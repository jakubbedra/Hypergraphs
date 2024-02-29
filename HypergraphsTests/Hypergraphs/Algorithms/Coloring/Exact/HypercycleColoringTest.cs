using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class HypercycleColoringTest
{
    
    [Test]
    public void ApplyColoring_SimpleHypercycleNo2Edges()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
            new List<int> { 3, 4, 0 },
        };
        int n = 5;
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HypercycleColoring coloring = new HypercycleColoring();
        int[] validColoring = coloring.ComputeColoring(h);
        bool result = validator.IsValid(h, validColoring);
        int usedColors = validColoring.Distinct().Count();

        Assert.That(result, Is.True);
        Assert.That(usedColors, Is.EqualTo(expectedChromaticNumber));
    }
    
    [Test]
    public void ApplyColoring_SimpleHypercycleWith2Edges()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1 },
            new List<int> { 1, 2 },
            new List<int> { 2, 3 },
            new List<int> { 3, 4 },
            new List<int> { 4, 5 },
            new List<int> { 5, 6, 0 },
        };
        int n = 7;
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HypercycleColoring coloring = new HypercycleColoring();
        int[] validColoring = coloring.ComputeColoring(h);
        bool result = validator.IsValid(h, validColoring);
        int usedColors = validColoring.Distinct().Count();

        Assert.That(result, Is.True);
        Assert.That(usedColors, Is.EqualTo(expectedChromaticNumber));
    }
    
    
}