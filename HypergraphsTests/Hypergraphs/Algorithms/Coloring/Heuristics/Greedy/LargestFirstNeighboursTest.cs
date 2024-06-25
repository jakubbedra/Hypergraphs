using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class LargestFirstNeighboursTest : GreedyTestBase
{
    
    [SetUp]
    public void Setup()
    {
        _coloringAlgorithm = new LargestFirstNeighbours();
    }

}