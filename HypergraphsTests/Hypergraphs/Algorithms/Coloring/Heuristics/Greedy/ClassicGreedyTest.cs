using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class ClassicGreedyTest : GreedyTestBase
{
    [SetUp]
    public void Setup()
    {
        _coloringAlgorithm = new ClassicGreedy();
    }

    [Test]
    public void HyperstarTest()
    {
        int expectedColors = 2;
        for (int i = 0; i < 1000; i++)
        {
            int n = 10;
            int m = 10;
            int c = 1;
            HyperstarGenerator generator = new HyperstarGenerator();
            Hypergraph hypergraph = generator.Generate(n, m, c);
            int[] coloring = _coloringAlgorithm.ComputeColoring(hypergraph);
            int numberOfColors = coloring.Distinct().Count();
            Assert.AreEqual(expectedColors, numberOfColors);
        }
    }
    
}