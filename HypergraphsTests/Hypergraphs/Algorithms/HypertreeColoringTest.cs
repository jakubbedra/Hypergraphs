using Hypergraphs.Algorithms;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class HypertreeColoringTest
{
    [Test]
    public void ApplyColoring_Interval()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
        };
        int n = 5;
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HypertreeColoring coloring = new HypertreeColoring();
        int[] validColoring = coloring.Apply(h);
        bool result = validator.IsValid(h, validColoring);
        int chromaticNumber = coloring.ChromaticNumber;

        Assert.That(result, Is.True);
        Assert.That(chromaticNumber, Is.EqualTo(expectedChromaticNumber));
    }

    [Test]
    public void ApplyColoring_SmallHypertree()
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
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HypertreeColoring coloring = new HypertreeColoring();
        int[] validColoring = coloring.Apply(h);
        bool result = validator.IsValid(h, validColoring);
        int chromaticNumber = coloring.ChromaticNumber;

        Assert.That(result, Is.True);
        Assert.That(chromaticNumber, Is.EqualTo(expectedChromaticNumber));
    }

    [Test]
    public void ApplyColoring_SmallHypertree2()
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
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HypertreeColoring coloring = new HypertreeColoring();
        int[] validColoring = coloring.Apply(h);
        bool result = validator.IsValid(h, validColoring);
        int chromaticNumber = coloring.ChromaticNumber;

        Assert.That(result, Is.True);
        Assert.That(chromaticNumber, Is.EqualTo(expectedChromaticNumber));
    }

    [Test]
    public void ApplyColoring_LargerHostTree()
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
        int expectedChromaticNumber = 2;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        
        HypertreeColoring coloring = new HypertreeColoring();
        int[] validColoring = coloring.Apply(h);
        bool result = validator.IsValid(h, validColoring);
        int chromaticNumber = coloring.ChromaticNumber;

        Assert.That(result, Is.True);
        Assert.That(chromaticNumber, Is.EqualTo(expectedChromaticNumber));
    }
    
}
