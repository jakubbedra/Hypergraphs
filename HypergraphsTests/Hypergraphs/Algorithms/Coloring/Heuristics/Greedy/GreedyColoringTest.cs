using Hypergraphs.Algorithms;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class GreedyColoringTest : GreedyTestBase
{
      
    [SetUp]
    public void Setup()
    {
        _coloringAlgorithm = new GreedyColoring(true);
    }
    
}