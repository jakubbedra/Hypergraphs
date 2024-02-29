using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class HyperpathColoringTest
{
    [Test]
    public void ApplyColoring_SmallHyperpath()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
            new List<int> { 1, 2, 3 },
        };
        int n = 5;
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HyperpathColoring coloring = new HyperpathColoring();
        int[] validColoring = coloring.ComputeColoring(h);
        bool result = validator.IsValid(h, validColoring);
        int usedColors = validColoring.Distinct().Count();

        Assert.That(result, Is.True);
        Assert.That(usedColors, Is.EqualTo(expectedChromaticNumber));
    }
    
    [Test]
    public void ApplyColoring_MediumHyperpath()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 4, 3, 5, 2, 11 },
            new List<int> { 5, 2, 11 },
            new List<int> { 2, 11 },
            new List<int> { 2, 11, 6, 10, 1, 7, 9 },
            new List<int> { 1, 7, 8, 0 },
            new List<int> { 6, 10, 1, 7 },
        };
        int n = 12;
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HyperpathColoring coloring = new HyperpathColoring();
        int[] validColoring = coloring.ComputeColoring(h);
        bool result = validator.IsValid(h, validColoring);
        int usedColors = validColoring.Distinct().Count();

        Assert.That(result, Is.True);
        Assert.That(usedColors, Is.EqualTo(expectedChromaticNumber));
    }
}