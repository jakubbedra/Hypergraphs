using Hypergraphs.Generators;

namespace HypergraphsTests.Generators;

public class HypercycleGeneratorTest
{
    [Test]
    public void Generate_SmallHypercycle()
    {
        HypercycleGenerator generator = new HypercycleGenerator();
        int n = 10;
        int m = 14;

        int[,] hypergraph = generator.GenerateHypercycleMatrix(n, m);

        // int[,] hypergraph =
        // {
        //     { 1, 1, 0, 0 },
        //     { 1, 1, 0, 1 },
        //     { 0, 1, 0, 1 },
        //     { 0, 0, 1, 0 },
        //     { 0, 0, 1, 0 },
        //     { 0, 0, 1, 0 },
        // };
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write($"{hypergraph[i, j]}; ");
            }

            Console.WriteLine();
        }

        // ValidateOverlap(n, m, hypergraph);
    } 
    // [Test]
    // public void Generate_MediumHyperpath()
    // {
    //     HyperpathGenerator generator = new HyperpathGenerator();
    //     int n = 21;
    //     int m = 37;
    //
    //     int[,] hypergraph = generator.GenerateConsecutiveOnesMatrix(n, m);
    //
    //     // for (int i = 0; i < n; i++)
    //     // {
    //     //     for (int j = 0; j < m; j++)
    //     //     {
    //     //         Console.Write($"{hypergraph[i, j]}; ");
    //     //     }
    //     //
    //     //     Console.WriteLine();
    //     // }
    //     
    //     ValidateOverlap(n, m, hypergraph);
    // }
    //
    // [Test]
    // public void Generate_BigHyperpath()
    // {
    //     HyperpathGenerator generator = new HyperpathGenerator();
    //     int n = 40;
    //     int m = 69;
    //
    //     int[,] hypergraph = generator.GenerateConsecutiveOnesMatrix(n, m);
    //
    //     // for (int i = 0; i < n; i++)
    //     // {
    //     //     for (int j = 0; j < m; j++)
    //     //     {
    //     //         Console.Write($"{hypergraph[i, j]}; ");
    //     //     }
    //     //
    //     //     Console.WriteLine();
    //     // }
    //     
    //     ValidateOverlap(n, m, hypergraph);
    // }
    //
    // [Test]
    // public void Generate_LargeHyperpath()
    // {
    //     HyperpathGenerator generator = new HyperpathGenerator();
    //     int n = 500;
    //     int m = 2137;
    //
    //     int[,] hypergraph = generator.GenerateConsecutiveOnesMatrix(n, m);
    //
    //     ValidateOverlap(n, m, hypergraph);
    // }
    //
    // private void ValidateOverlap(int n, int m, int[,] matrix)
    // {
    //     List<ConsecutiveOnesColumn> columns = new List<ConsecutiveOnesColumn>();
    //     for (int e = 0; e < m; e++)
    //     {
    //         int startIndex = -1;
    //         int size = 0;
    //         for (int v = 0; v < n; v++)
    //         {
    //             if (startIndex == -1 && matrix[v, e] == 1)
    //             {
    //                 startIndex = v;
    //             }
    //
    //             if (matrix[v, e] == 1) size++;
    //         }
    //
    //         columns.Add(new ConsecutiveOnesColumn() { Size = size, StartIndex = startIndex });
    //     }
    //
    //     foreach (ConsecutiveOnesColumn column in columns)
    //     {
    //         Assert.True(columns.Where(c => c != column).Any(c => c.Overlaps(column)));
    //     }
    // }
    
}