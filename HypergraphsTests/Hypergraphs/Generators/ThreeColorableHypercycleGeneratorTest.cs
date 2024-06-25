using Hypergraphs.Generators;
using Hypergraphs.Generators.Model;

namespace HypergraphsTests.Generators;

public class ThreeColorableHypercycleGeneratorTest
{
    [Test]
    public void Generate_SmallHypercycle()
    {
        HypercycleGenerator generator = new HypercycleGenerator(true);
        int n = 21;
        int m = 37;

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

        ValidateAllTwoEdgesExist(n, m, hypergraph);
    }

    private void ValidateAllTwoEdgesExist(int n, int m, int[,] hypergraph)
    {
        for (int e = 0; e < n; e++)
        {
            int edgeSize = 0;
            for (int v = 0; v < n; v++)
                edgeSize += hypergraph[v, e];
            Assert.That(edgeSize, Is.EqualTo(2));
        }
    }
    
}