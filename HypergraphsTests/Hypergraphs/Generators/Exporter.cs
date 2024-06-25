using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;
using Newtonsoft.Json;

namespace HypergraphsTests.Generators;

public class Exporter
{
    private static string path2 = @"C:\Users\theKonfyrm\Desktop\generators\";

    [Test]
    public void ExportHyperstars()
    {
        List<List<int>> sizes = new List<List<int>>()
        {
            new List<int> { 10, 10, 1 },
            new List<int> { 25, 25, 5 },
            new List<int> { 50, 100, 10 },
            new List<int> { 100, 50, 10 },
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size[0];
            int m = size[1];
            int c = size[2];
            for (int i = 0; i < 1000; i++)
            {
                var generator = new HyperstarGenerator();
                Hypergraph h = generator.Generate(n, m, c);
                hypergraphs.Add(h);
            }

            File.WriteAllText($"{path2}\\hyperstars\\hyperstars_{n}_{m}_{c}.json",
                JsonConvert.SerializeObject(hypergraphs));
        }
    }

    [Test]
    public void ExportHypertrees()
    {
        List<List<int>> sizes = new List<List<int>>()
        {
            new List<int> { 10, 10 },
            new List<int> { 10, 20 },
            new List<int> { 20, 10 },
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size[0];
            int m = size[1];
            for (int i = 0; i < 1000; i++)
            {
                var generator = new HypertreeGenerator();
                Hypergraph h = generator.Generate(n, m);
                hypergraphs.Add(h);
            }

            File.WriteAllText($"{path2}\\hypertrees\\hypertrees_{n}_{m}.json",
                JsonConvert.SerializeObject(hypergraphs));
        }
    }
    
    [Test]
    public void ExportHyperpaths()
    {
        List<List<int>> sizes = new List<List<int>>()
        {
            new List<int> { 10, 10 },
            new List<int> { 10, 20 },
            new List<int> { 20, 10 },
            new List<int> { 25, 25 },
            new List<int> { 50, 20 },
            new List<int> { 20, 50 },
            new List<int> { 100, 100 },
            new List<int> { 100, 200 },
            new List<int> { 200, 100 },
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size[0];
            int m = size[1];
            for (int i = 0; i < 1000; i++)
            {
                var generator = new HyperpathGenerator();
                Hypergraph h = generator.Generate(n, m);
                hypergraphs.Add(h);
            }

            File.WriteAllText($"{path2}\\hyperpaths\\hyperpaths_{n}_{m}.json",
                JsonConvert.SerializeObject(hypergraphs));
        }
    }
    
    [Test]
    public void ExportRandom()
    {
        List<List<int>> sizes = new List<List<int>>()
        {
            new List<int> { 20, 10 },
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size[0];
            int m = size[1];
            for (int i = 0; i < 1000; i++)
            {
                var generator = new ConnectedHypergraphGenerator();
                Hypergraph h = generator.Generate(n, m);
                hypergraphs.Add(h);
            }

            File.WriteAllText($"{path2}\\random\\random_{n}_{m}.json",
                JsonConvert.SerializeObject(hypergraphs));
        }
    }
    
    [Test]
    public void Export3uniform()
    {
        List<List<int>> sizes = new List<List<int>>()
        {
            new List<int> { 10, 10, 3 },
            new List<int> { 10, 20, 3 },
            new List<int> { 15, 100, 3 },
            new List<int> { 15, 200, 3 },
            new List<int> { 20, 100, 3 },
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size[0];
            int m = size[1];
            int r = size[2];
            for (int i = 0; i < 1000; i++)
            {
                var generator = new UniformHypergraphGenerator();
                Hypergraph h = generator.GenerateConnected(n, m, r);
                hypergraphs.Add(h);
            }

            File.WriteAllText($"{path2}\\3uniform\\3uniform_{n}_{m}.json",
                JsonConvert.SerializeObject(hypergraphs));
        }
    }
    
    [Test]
    public void Export4uniform()
    {
        List<List<int>> sizes = new List<List<int>>()
        {
            new List<int> { 10, 10, 4 },
            new List<int> { 10, 20, 4 },
            new List<int> { 15, 100, 4 },
            new List<int> { 15, 200, 4 },
            new List<int> { 20, 200, 4 },
        };
        foreach (var size in sizes)
        {
            List<Hypergraph> hypergraphs = new List<Hypergraph>();
            int n = size[0];
            int m = size[1];
            int r = size[2];
            for (int i = 0; i < 1000; i++)
            {
                var generator = new UniformHypergraphGenerator();
                Hypergraph h = generator.GenerateConnected(n, m, r);
                hypergraphs.Add(h);
            }

            File.WriteAllText($"{path2}\\uniform\\uniform_{n}_{m}_{r}.json",
                JsonConvert.SerializeObject(hypergraphs));
        }
    }
}