using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class LargestFirstTest : GreedyTestBase
{
    
    [SetUp]
    public void Setup()
    {
        _coloringAlgorithm = new LargestFirst();
    }

}