using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Hypergraphs.Algorithms;

public class CUDAColorVariationColoringTest
{
    
    static void GenerateNumbers(int[] counter, int c, int n)
    {
        int totalCombinations = 1;
        for (int i = 0; i < n; i++)
        {
            totalCombinations *= c;
        }

        for (int i = 0; i < totalCombinations; i++)//while search space not exhausted
        {
            IncrementCounter(counter, c);
            PrintNumber(counter);
        }
    }
    
    static void PrintNumber(int[] number)
    {
        foreach (var digit in number)
        {
            Console.Write(digit + " ");
        }
        Console.WriteLine();
    }
    
    static void IncrementCounter(int[] counter, int c)
    {
        int index = counter.Length - 1;
        while (index >= 0)///startVertex
        {
            counter[index]++;
            if (counter[index] < c)
                break;
            counter[index] = 0;
            index--;
        }
    }

    [Test]
    public void test2137()
    {
        int n = 10;
        int maxColors = 4;
        int[] dupa = new int[n];
        for (var i = 0; i < n; i++)
            dupa[i] = 0;
        GenerateNumbers(dupa, maxColors, n);
    }
    
    [Test]
    public void ComputeColoring_SmallHyperpath()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 2, 3, 4 },
        };
        int n = 5;
        Hypergraph h = HypergraphFactory.FromHyperEdgesList(n, edges);
        CUDAColorVariationColoring coloring = new CUDAColorVariationColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloring.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
        Assert.That(colors.Distinct().Count(), Is.EqualTo(2));
    }
    
    [Test]
    public void ComputeColoring_SmallHypertree()
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
        CUDAColorVariationColoring coloring = new CUDAColorVariationColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloring.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
        Assert.That(colors.Distinct().Count(), Is.EqualTo(2));
    }
  
    [Test]
    public void ComputeColoring_SmallRandomUniformHypergraph()
    {
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        // ConnectedHypergraphGenerator generator = new ConnectedHypergraphGenerator();
        int n = 10;
        int m = 40;
        int r = 3;
        Hypergraph h = generator.GenerateConnected(n, m, r);
        CUDAColorVariationColoring coloringAlgorithm = new CUDAColorVariationColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloringAlgorithm.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
    [Test]
    public void ComputeColoring_LargeRandomUniformHypergraph()
    {
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        // reasonable limit 
        int n = 20;
        int m = 200;
        int r = 3;
        Hypergraph h = generator.GenerateConnected(n, m, r);
        CUDAColorVariationColoring coloringAlgorithm = new CUDAColorVariationColoring();
        HypergraphColoringValidator validator = new HypergraphColoringValidator();

        int[] colors = coloringAlgorithm.ComputeColoring(h);

        Assert.True(validator.IsValid(h, colors));
    }
    
}