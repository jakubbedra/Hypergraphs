using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class SimplifiedDSaturTest : GreedyTestBase
{
    
    [SetUp]
    public void Setup()
    {
        _coloringAlgorithm = new SimplifiedDSatur();
    }

}