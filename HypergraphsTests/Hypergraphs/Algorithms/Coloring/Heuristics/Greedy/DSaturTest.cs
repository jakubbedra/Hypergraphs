using Hypergraphs.Algorithms;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class DSaturTest : GreedyTestBase
{
    
    [SetUp]
    public void Setup()
    {
        _coloringAlgorithm = new DSatur();
    }
    
}