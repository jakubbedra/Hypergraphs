using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class ColorVariationColoring : BaseColoring<Hypergraph>
{
    private Hypergraph _h;
    private HypergraphColoringValidator _validator;

    public ColorVariationColoring(Hypergraph h)
    {
        _h = h;
        _validator = new HypergraphColoringValidator();
    }

    public ColorVariationColoring()
    {
        _validator = new HypergraphColoringValidator();
    }
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        _h = h;
        int[] colors = new int[h.N];
        for (var i = 0; i < colors.Length; i++)
            colors[i] = -1;
        for (int i = 2; i < h.N; i++)
        {
            // List<int> coloring = VariationColoring(i, new List<int>());

            int[] coloring = VariationColoring2(i, colors, 0, h.N, h.M, h.Matrix);
            // if (coloring.Count > 0)
            if (coloring.Length > 0)
            {
                return coloring.ToArray();
            }
        }

        return new List<int>().ToArray();
    }

    List<int> VariationColoring(int maxNumberOfColors, List<int> vertexColors)
    {
        if (vertexColors.Count == _h.N)
        {
            if (_validator.IsValid(_h, vertexColors.ToArray())) 
                return vertexColors;
            return new List<int>();
        }
        else
        {//crobic rekurencje, ilosc zaglebien wyliczamy na poczatku, kazde zaglebienie - 1 wierzcholek
            
            for (int i = 0; i < maxNumberOfColors; i++)
            {
                vertexColors.Add(i);
                List<int> coloring = VariationColoring(maxNumberOfColors, vertexColors);
                if (coloring.Count > 0)
                {
                    return coloring;
                }

                vertexColors.Remove(i);
            }
        }

        return new List<int>();
    }
    
    private int[] VariationColoring2(int maxNumberOfColors, int[] vertexColors, int startVertex, int n, int m, int[,] matrix)
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(startVertex); // Start with the first vertex
    
        while (stack.Count > 0)
        {
            int currentVertex = stack.Count - 1;
            int currentColor = vertexColors[currentVertex];

            // If we have explored all colors for this vertex, backtrack
            if (currentColor == maxNumberOfColors)
            {
                vertexColors[currentVertex] = -1; // Reset color
                stack.Pop(); // Backtrack
                continue;
            }

            // Try assigning the next color to the current vertex
            vertexColors[currentVertex] = currentColor + 1;

            // If the coloring is valid, return it
            if (currentVertex == n - 1 && IsValidColoring(matrix, n, m, vertexColors))
                return vertexColors;

            // If there are more vertices to color, push the next vertex onto the stack
            if (currentVertex < n - 1)
                stack.Push(currentVertex + 1);
        }

        // If no valid coloring is found, return an empty array
        return new int[] { };
    }
    private bool IsValidColoring(int[,] matrix, int n, int m, int[] coloring)
    {
        for (int i = 0; i < m; i++)
        {
            bool isMonochromatic = false;
            // we only need 2 colors, because it is sufficient for telling that an edge is not monochromatic
            int color1 = -1;
            int color2 = -1;
            
            for (var j = 0; j < n; j++)
            {
                if (matrix[j, i] > 0)
                {
                    if (color1 == -1)
                    {
                        color1 = coloring[j];
                    }
                    else if (coloring[j] != color1)
                    {
                        color2 = coloring[j];
                        break;
                    }
                }
            }
            
            if (color2 == -1) return false;
        }

        return true;
    }
}
