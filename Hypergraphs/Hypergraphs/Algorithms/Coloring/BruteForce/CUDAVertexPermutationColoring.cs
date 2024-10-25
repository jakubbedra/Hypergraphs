// using Hypergraphs.Common.Algorithms;
// using Hypergraphs.Model;
// using ILGPU;
// using ILGPU.Runtime;
// using ILGPU.Runtime.Cuda;
//
// namespace Hypergraphs.Algorithms;
//
// public class CUDAVertexPermutationColoring : BaseColoring<Hypergraph>
// {
//     private Hypergraph _h;
//     private HypergraphColoringValidator _validator;
//
//     public CUDAVertexPermutationColoring(Hypergraph h)
//     {
//         _h = h;
//         _validator = new HypergraphColoringValidator();
//     }
//
//     public CUDAVertexPermutationColoring()
//     {
//         _validator = new HypergraphColoringValidator();
//     }
//
//     public override int[] ComputeColoring(Hypergraph h)
//     {
//         _h = h;
//         List<int> colors = new List<int>();
//         for (int i = 2; i < h.N; i++)
//         {
//             List<int> coloring = VariationColoringFirstStep(i, colors, h);
//             if (coloring.Count > 0)
//             {
//                 return coloring.ToArray();
//             }
//         }
//
//         return colors.ToArray();
//     }
//
//     List<int> VariationColoringFirstStep(int maxNumberOfColors, List<int> vertexColors, Hypergraph hypergraph)
//     {
//         int maxParallelExecutions = 1000;
//         if (vertexColors.Count == _h.N)
//         {
//             if (_validator.IsValid(_h, vertexColors.ToArray()))
//                 return vertexColors;
//             return new List<int>();
//         }
//
//         int startVertices = 1;
//         while (Math.Pow(maxNumberOfColors, startVertices) < maxParallelExecutions)
//             startVertices++;
//
//         startVertices = Math.Min(startVertices, hypergraph.N);
//         // 1. check how much memory is available
//         // 2. check how many cases we can fit there
//
//         // 3. split into [c1,c2,c3,c3, ...], [c1,c2,c3,c3, ...], ...
//         // for each of these initial states do VariationColoring
//
//         List<int[]> cases = new List<int[]>();
//         GetStartPermutations(startVertices, 0, maxNumberOfColors, new List<int>(), cases);
//         int[,] casesMatrix = new int[cases.Count, hypergraph.N];
//         
//         for (int i = 0; i < cases.Count; i++)
//         for (int j = 0; j < hypergraph.N; j++)
//             casesMatrix[i, j] = 0;
//
//         for (int i = 0; i < cases.Count; i++)
//         for (int j = 0; j < cases[i].Length; j++)
//             casesMatrix[i, j] = cases[i][j];
//         // parallelize each case
//
//         
//         Context context = Context.Create(builder => builder.Cuda());
//         // Context context = Context.Create(builder => builder.CPU());
//         Accelerator accelerator = context.GetPreferredDevice(preferCPU: false).CreateAccelerator(context);
//         // Accelerator accelerator = context.GetPreferredDevice(preferCPU: true).CreateAccelerator(context);
//
//         
//         // Load the data.
//         var hypergraphData = accelerator.Allocate2DDenseY<int>(new Index2D(hypergraph.N, hypergraph.M)); 
//         var initialColorings = accelerator.Allocate2DDenseY<int>(new Index2D(cases.Count, hypergraph.N));
//         MemoryBuffer1D<int, Stride1D.Dense> deviceOutput = accelerator.Allocate1D<int>(1);
//
//         hypergraphData.CopyFromCPU(hypergraph.Matrix);
//         initialColorings.CopyFromCPU(casesMatrix);
//         deviceOutput.CopyFromCPU(new[]{-1});
//
//         // load / precompile the kernel
//         Action<Index1D, ArrayView2D<int, Stride2D.DenseY>, ArrayView2D<int, Stride2D.DenseY>, ArrayView<int>, int, int> loadedKernel = 
//             accelerator.LoadAutoGroupedStreamKernel<Index1D, ArrayView2D<int, Stride2D.DenseY>, ArrayView2D<int, Stride2D.DenseY>, ArrayView<int>, int, int>(VariationColoringGPU);
//
//         // finish compiling and tell the accelerator to start computing the kernel
//         loadedKernel(cases.Count, hypergraphData.View, initialColorings.View, deviceOutput.View, maxNumberOfColors, startVertices);
//
//         // wait for the accelerator to be finished with whatever it's doing in this case it just waits for the kernel to finish.
//         accelerator.Synchronize();
//
//         // moved output data from the GPU to the CPU for output to console
//
//         
//         int[] hostOutput = deviceOutput.GetAsArray1D();
//         
//         List<int> colors = new List<int>();
//         if (hostOutput[0] != -1)
//         {
//             int[,] finalResults = initialColorings.GetAsArray2D();
//             int[] output = new int[hypergraph.N];
//             for (int i = 0; i < hypergraph.N; i++)
//                 output[i] = finalResults[hostOutput[0], i];
//             colors = output.ToList();
//         }
//
//         accelerator.Dispose();
//         context.Dispose();
//
//         return colors;
//     }
//
//     private void GetStartPermutations(int maxDepth, int currentDepth, int n, List<int> lastPermutation, List<int[]> permutations)
//     {
//         if (currentDepth >= maxDepth)
//         {
//             permutations.Add(lastPermutation.ToArray());
//             return;
//         }
//
//         for (int i = 0; i < n; i++)
//         {
//             if (!lastPermutation.Contains(i))
//             {
//                 lastPermutation.Add(i);
//                 GetStartPermutations(maxDepth, currentDepth + 1, n, lastPermutation, permutations);
//                 lastPermutation.Remove(i);
//             }
//         }
//     }
//
//     private static void VariationColoringGPU(
//         Index1D index,
//         ArrayView2D<int, Stride2D.DenseY> hypergraphMatrix,
//         ArrayView2D<int, Stride2D.DenseY> colorings,
//         ArrayView<int> output,
//         int maxNumberOfColors,
//         int startVertex
//     )
//     {
//         int n = hypergraphMatrix.IntExtent.X;
//         int m = hypergraphMatrix.IntExtent.Y;
//         int currentColoring = index.X;
//         
//         do
//         {
//             if (output[0] != -1) return;
//             if (IsValidColoring(hypergraphMatrix, n, m, colorings, currentColoring))
//             {
//                 output[0] = currentColoring;
//                 return;
//             }
//             IncrementCounter(colorings, currentColoring, startVertex, n);
//         } while (!SearchSpaceExhausted(startVertex, n, maxNumberOfColors, colorings, currentColoring));
//     }
//     
//     private static void IncrementCounter(ArrayView2D<int, Stride2D.DenseY> colorings, int currentColoring, int startVertex, int n)
//     {
//         int index = n - 1;
//         while (index >= startVertex)
//         {
//             int v = colorings[new Index2D(currentColoring, index)];
//             while (Includes(colorings, v))
//             {
//                 v++;
//             }
//             colorings[new Index2D(currentColoring, index)] = v;
//             if (colorings[new Index2D(currentColoring, index)] < n)
//                 break;
//             colorings[new Index2D(currentColoring, index)] = -1;
//             index--;
//         }
//     }
//     
//     private static bool SearchSpaceExhausted(int startVertex, int n, int maxColors, ArrayView2D<int, Stride2D.DenseY> colorings, int currentColoring)
//     {
//         for (int v = startVertex; v < n; v++)
//             if (colorings[new Index2D(currentColoring, startVertex)] != maxColors - 1)
//                 return false;
//         return true;
//     }
//
//     private static bool IsValidColoring(ArrayView2D<int, Stride2D.DenseY> hypergraphMatrix, int n, int m, ArrayView2D<int, Stride2D.DenseY> colorings, int currentColoring)
//     {
//         for (int i = 0; i < m; i++)
//         {
//             int color1 = -1;
//             int color2 = -1;
//
//             for (var j = 0; j < n; j++)
//             {
//                 if (hypergraphMatrix[new Index2D(j, i)] > 0)
//                 {
//                     if (color1 == -1)
//                     {
//                         color1 = colorings[new Index2D(currentColoring, j)];
//                     }
//                     else if (colorings[new Index2D(currentColoring, j)] != color1)
//                     {
//                         color2 = colorings[new Index2D(currentColoring, j)];
//                         break;
//                     }
//                 }
//             }
//
//             if (color2 == -1) return false;
//         }
//
//         return true;
//     }
//     
// }