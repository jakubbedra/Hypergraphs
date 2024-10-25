﻿using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class GreedyTestBase
{
    protected BaseGreedy _coloringAlgorithm;

    [Test]
    public void ComputeColoring_2ColorableHypergraph()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 6, 7 },
            new List<int> { 8, 9, 10 },
            new List<int> { 8, 7, 6 },
            new List<int> { 1, 2, 3, 4, 5 },
        };
        int n = 11;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = _coloringAlgorithm.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
    [Test]
    public void ComputeColoring_Graph()
    {
        List<List<int>> hyperedges = new List<List<int>>
        {
            new List<int> { 0, 1 },
            new List<int> { 0, 2 },
            new List<int> { 0, 3 },
            new List<int> { 0, 4 },
            new List<int> { 1, 2 },
            new List<int> { 1, 3 },
            new List<int> { 1, 4 },
            new List<int> { 2, 3 },
            new List<int> { 2, 4 },
            new List<int> { 3, 4 },
        };
        int n = 5;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = _coloringAlgorithm.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
    [Test]
    public void ComputeColoring_RandomUniformHypergraph()
    {
        int n = 40;
        int m = 4200;
        int r = 4;
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        Hypergraph h = generator.GenerateSimple(n, m, r);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = _coloringAlgorithm.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
}