using Hypergraphs.Generators;
using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Hypergraphs.Factory;
using Hypergraphs.Hypergraphs.Factory.Valdator;
using Hypergraphs.Model;

namespace HypergraphsTests.Model;

public class HypertreeHostTreeFactoryTest
{
    [Test]
    public void Create_Interval()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
        };
        int n = 5;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        TreeCheck check = new TreeCheck();

        Graph hostTree = HypertreeHostTreeFactory.FromHypertree(h);
        bool result = check.Apply(hostTree);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Create_SmallHypertree()
    {
        List<List<int>> edges = new List<List<int>>()
        {
            new List<int>() { 0, 1 },
            new List<int>() { 1, 2, 3, 4 },
            new List<int>() { 4, 3, 8, 9 },
            new List<int>() { 4, 6, 7 },
            new List<int>() { 2, 4, 5 },
        };
        int n = 10;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        TreeCheck check = new TreeCheck();

        Graph hostTree = HypertreeHostTreeFactory.FromHypertree(h);
        bool result = check.Apply(hostTree);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Create_SmallHypertree2()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 6, 7 },
            new List<int> { 8, 9, 10 },
            new List<int>() { 8, 7, 6 },
            new List<int> { 1, 2, 3, 4, 5 },
        };
        int n = 11;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        TreeCheck check = new TreeCheck();

        Graph hostTree = HypertreeHostTreeFactory.FromHypertree(h);
        bool result = check.Apply(hostTree);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Create_LargerHostTree()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 1, 2, 3 },
            new List<int> { 2, 3, 4 },
            new List<int> { 3, 5, 6, 7 },
            new List<int> { 5, 6 },
            new List<int> { 8, 9 },
            new List<int> { 6, 7, 8, 9 },
            new List<int> { 10, 12 },
            new List<int> { 11, 10, 12, 9 },
        };
        int n = 13;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        TreeCheck check = new TreeCheck();

        Graph hostTree = HypertreeHostTreeFactory.FromHypertree(h);
        bool result = check.Apply(hostTree);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHostTreeValidityTest_10_10()
    {
        int iterations = 1000;
        int n = 10;
        int m = 10;
        HypertreeGenerator generator = new HypertreeGenerator();
        HostGraphValidator validator = new HostGraphValidator();
        for (int i = 0; i < iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            Graph hostTree = HypertreeHostTreeFactory.FromHypertree(hypergraph);
            bool result = validator.IsValid(hypergraph, hostTree);
            Assert.That(result, Is.True);
        }
    }
    
    [Test]
    public void IsHostTreeValidityTest_10_50()
    {
        int iterations = 1000;
        int n = 10;
        int m = 50;
        HypertreeGenerator generator = new HypertreeGenerator();
        HostGraphValidator validator = new HostGraphValidator();
        for (int i = 0; i < iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            Graph hostTree = HypertreeHostTreeFactory.FromHypertree(hypergraph);
            bool result = validator.IsValid(hypergraph, hostTree);
            Assert.That(result, Is.True);
        }
    }

    [Test]
    public void IsHostTreeValidityTest_100_100()
    {
        int iterations = 1000;
        int n = 100;
        int m = 100;
        HypertreeGenerator generator = new HypertreeGenerator();
        HostGraphValidator validator = new HostGraphValidator();
        for (int i = 0; i < iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            Graph hostTree = HypertreeHostTreeFactory.FromHypertree(hypergraph);
            bool result = validator.IsValid(hypergraph, hostTree);
            Assert.That(result, Is.True);
        }
    }
    
    [Test]
    public void IsHostTreeValidityTest_1000_100()
    {
        int iterations = 1000;
        int n = 1000;
        int m = 100;
        HypertreeGenerator generator = new HypertreeGenerator();
        HostGraphValidator validator = new HostGraphValidator();
        for (int i = 0; i < iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            Graph hostTree = HypertreeHostTreeFactory.FromHypertree(hypergraph);
            bool result = validator.IsValid(hypergraph, hostTree);
            Assert.That(result, Is.True);
        }
    }
    
    [Test]
    public void IsHostTreeValidityTest_100_1000()
    {
        int iterations = 1000;
        int n = 100;
        int m = 1000;
        HypertreeGenerator generator = new HypertreeGenerator();
        HostGraphValidator validator = new HostGraphValidator();
        for (int i = 0; i < iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            Graph hostTree = HypertreeHostTreeFactory.FromHypertree(hypergraph);
            bool result = validator.IsValid(hypergraph, hostTree);
            Assert.That(result, Is.True);
        }
    }
    
}