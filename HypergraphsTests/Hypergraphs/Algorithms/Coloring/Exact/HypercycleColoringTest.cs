using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;
using Newtonsoft.Json;

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
    /*
0 0 1 1 1 0 0 1 0 0     
1 1 0 0 0 0 0 1 1 0
1 1 1 0 1 1 0 1 1 0
1 1 0 0 0 1 1 1 1 1 
0 0 0 1 0 0 0 0 0 1     
     */


    /*
3 1 . . . 1
2 1 . 1 . . 
4 1 . 1 . .
7 1 1 1 1 .
8 . 1 1 1 .
1 . 1 1 1 .
0 . 1 1 1 .
5 . . 1 1 .
6 . . . 1 .
9 . . . 1 1 

     */
    [Test]
    public void ApplyColoring_SimpleHypercycleWith5EdgesAnd10Vertices()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 2, 3, 4, 7 },
            new List<int> { 0, 1, 7, 8 },
            new List<int> { 0, 1, 2, 4, 5, 7, 8 },
            new List<int> { 0, 1, 5, 6, 7, 8, 9 },
            new List<int> { 3, 9 },
        };
        int n = 10;
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
    public void ApplyColoring_Simple3ColorableHypercycleWith5EdgesAnd10Vertices()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 1, 2, 3 },
            new List<int> { 2, 3, 4, 5 },
            new List<int> { 7, 8, 9 },
            new List<int> { 0, 1 },
            new List<int> { 1, 2 },
            new List<int> { 2, 3 },
            new List<int> { 3, 4 },
            new List<int> { 4, 5 },
            new List<int> { 5, 6 },
            new List<int> { 6, 7 },
            new List<int> { 7, 8 },
            new List<int> { 8, 9 },
            new List<int> { 9, 10 },
            new List<int> { 10, 0 },
        };
        int n = 11;
        int expectedChromaticNumber = 3;
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
    public void ApplyColoring_SimpleHypercycleWith10EdgesAnd20Vertices()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 5,6,7,11,12,17 },
            new List<int> { 4,5,6,7,8,11,12,14,15,16,17 },
            new List<int> { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,17,18,19 },
            new List<int> { 0,1,2,3,8,9,10,13,15,18,19 },
            new List<int> { 0,1,2,3,4,6,8,9,10,13,14,15,16,18,19 },
            
            new List<int> { 0,1,2,3,8,9,10,15,16,18,19 },
            new List<int> { 0,1,2,3,4,5,6,7,8,10,11,12,13,14,15,16,17 },
            new List<int> { 1,3,4,7,9,12,13,14,17,18,19 },
            new List<int> { 1,3,4,5,6,7,9,10,11,12,13,14,17,18,19 },
            new List<int> { 4,9,18,19 },
        };
        int n = 20;
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
    public void ApplyColoring_SimpleHypercycleWith10EdgesAnd10Vertices()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 2 },
            new List<int> { 0,2,3,4,6,8,9 },
            new List<int> { 1,3,4,7,8,9 },
            new List<int> { 0,1,2,3,4,5,6,8,9 },
            new List<int> { 0,2,3,8,9 },
            new List<int> { 2,6 },
            new List<int> { 1,4,7 },
            new List<int> { 4,7 },
            new List<int> { 0,1,2,3,4,6,7,8,9 },
            new List<int> { 0,3,4,8,9 },
        };
        int n = 10;
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
    public void ApplyColoring_SimpleHypercycleWith10EdgesAnd10Vertices_Case2()
    {
        List<List<int>> edges = new List<List<int>>
        {
            new List<int> { 0, 2, 6 },
            new List<int> { 0,1,2,5,6,7,8,9 },
            new List<int> { 4,7 },
            new List<int> { 1,2,3,4,5,7,9 },
            new List<int> { 0,1,2,3,4,6,7,8,9 },
            new List<int> { 1,3,4,5,9 },
            new List<int> { 1,3,5,9 },
            new List<int> { 0,1,2,3,4,5,6,8,9 },
            new List<int> { 1,3,4,5,8,9 },
            new List<int> { 5,9 },
        };
        int n = 10;
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
  
    private static string path = @"C:\Users\theKonfyrm\Desktop\hypercycle\";

    [Test]
    public void ApplyColoring_RandomHypercycle()
    {
        List<Tuple<int, int>> sizes = new List<Tuple<int, int>>(){
            Tuple.Create(50,10),
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size.Item1;
            int m = size.Item2;
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    int expectedChromaticNumber = 2;
                    HypercycleGenerator generator = new HypercycleGenerator();
                    Hypergraph h = generator.Generate(n, m);
                    HypergraphColoringValidator validator = new HypergraphColoringValidator();

                    HypercycleColoring coloring = new HypercycleColoring();
                    int[] validColoring = coloring.ComputeColoring(h); 
                    bool result = validator.IsValid(h, validColoring);
                    int usedColors = validColoring.Distinct().Count();

                    hypergraphs.Add(h);
                    // Assert.That(result, Is.True);
                    // Assert.That(usedColors, Is.EqualTo(expectedChromaticNumber));
                }
                catch (Exception)
                {
                    i--;
                }
            }

            // Console.WriteLine(JsonConvert.SerializeObject(hypergraphs));
            File.WriteAllText($"{path}hypercycles_{n}_{m}.json", JsonConvert.SerializeObject(hypergraphs));
        }
    }
    
    private static string path2 = @"C:\Users\theKonfyrm\Desktop\hypercycle3col\";

    [Test]
    public void ApplyColoring_Random3ColorableHypercycle()
    {
        List<Tuple<int, int>> sizes = new List<Tuple<int, int>>(){
            Tuple.Create(21,31),
            Tuple.Create(49,49),
            Tuple.Create(109,209),
            // Tuple.Create(21,49),
            // Tuple.Create(109,249),
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size.Item1;
            int m = size.Item2;
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    int expectedChromaticNumber = 2;
                    // var generator = new ThreeColorableHypercycleGenerator();
                    var generator = new HypercycleGenerator();
                    Hypergraph h = generator.Generate(n, m);
                    HypergraphColoringValidator validator = new HypergraphColoringValidator();

                    HypercycleColoring coloring = new HypercycleColoring();
                    int[] validColoring = coloring.ComputeColoring(h); 
                    bool result = validator.IsValid(h, validColoring);
                    int usedColors = validColoring.Distinct().Count();

                    hypergraphs.Add(h);
                    // Assert.That(result, Is.True);
                    // Assert.That(usedColors, Is.EqualTo(expectedChromaticNumber));
                }
                catch (Exception)
                {
                    i--;
                }
            }

            // Console.WriteLine(JsonConvert.SerializeObject(hypergraphs));
            File.WriteAllText($"{path2}3_colorable_hypercycles_{n}_{m}.json", JsonConvert.SerializeObject(hypergraphs));
        }
    }
    /*
     
0 0 1 1 1 0 0 1 0 0     
1 1 0 0 0 0 0 1 1 0
1 1 1 0 1 1 0 1 1 0
1 1 0 0 0 1 1 1 1 1 
0 0 0 1 0 0 0 0 0 1     


3 2 4 7 8 0 1 5 6 9  
1 1 1 1 . . . . . .       
. . . 1 1 1 1 . . .  
. 1 1 1 1 1 1 1 . .  
. . . 1 1 1 1 1 1 1   
1 . . . . . . . . 1       
     
     */
    /*
     fail:
1 1 1 1 1 . 1 1 1 1
1 1 1 1 1 1 1 . 1 1
. . 1 . . . . 1 . .
. . . 1 . . . . . 1
. 1 1 1 . 1 1 1 1 1

9 0 1 2 8 3 4 6 7 5  
. . 1 1 1 1 . . . .  
1 1 1 . . . . . 1 1  
. . . 1 1 1 1 1 1 .  
. . 1 1 1 . . . . .  
1 1 . 1 1 1 1 1 1 1  
     
     pass:
. . . . . . . 1 1 .  
1 1 1 1 . . . . 1 1  
. . 1 1 1 1 1 1 1 1  
. 1 1 1 1 1 1 1 . .  
. . . 1 1 1 1 1 . .  
     
3 6 4 5 1 7 8 9 0 2 
. . 1 1 1 . . . . . 
. 1 1 1 1 1 1 1 1 1 
1 . 1 1 1 1 1 1 1 1 
1 1 . . . 1 1 1 1 1 
1 1 1 1 . . 1 1 1 1 
     
     */
}