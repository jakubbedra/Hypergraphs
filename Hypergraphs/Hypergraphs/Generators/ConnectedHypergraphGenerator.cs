using Hypergraphs.Common.Comparator;
using Hypergraphs.Extensions;
using Hypergraphs.Model;
using MathNet.Numerics.Random;

namespace Hypergraphs.Generators;

public class ConnectedHypergraphGenerator
{
    private static Random _r = new Random();

    public Hypergraph Generate(int n, int m)
    {
        int[,] matrix = new int[n, m];
        for (int v = 0; v < n; v++)
        for (int e = 0; e < m; e++)
            matrix[v, e] = 0;

        HashSet<int> verticesInNoEdge = new HashSet<int>();
        for (int v = 0; v < n; v++)
            verticesInNoEdge.Add(v);
        List<int> chosenVertices = new List<int>();
        
        var comparer = new HashSetEqualityComparer<int>();
        HashSet<HashSet<int>> edges = new HashSet<HashSet<int>>(comparer);
        while (edges.Count != m)
        {
            int startVertex;
            if (edges.Count == 0) startVertex = _r.Next(n);
            else startVertex = chosenVertices[_r.Next(chosenVertices.Count)];
            
            HashSet<int> edge = new HashSet<int>() { startVertex };
            for (int v = 0; v < n; v++)
                if (_r.NextBoolean())
                    edge.Add(v);
            if (edge.Count > 1)
            {
                edges.Add(edge);
                foreach (int v in edge)
                {
                    if (verticesInNoEdge.Contains(v))
                    {
                        verticesInNoEdge.Remove(v);
                        chosenVertices.Add(v);
                    }
                }
            }
            
        }
        
        int e1 = 0;
        foreach (HashSet<int> edge in edges)
        {
            foreach (int v in edge)
            {
                matrix[v, e1] = 1;
                if (verticesInNoEdge.Contains(v))
                    verticesInNoEdge.Remove(v);
            }
            e1++;
        }
        
        // shuffle the matrix rows
        List<int> shuffledVertices = new List<int>(n);
        for (int v = 0; v < n; v++)
            shuffledVertices.Add(v);
        
        shuffledVertices.Shuffle();
        int[,] finalMatrix = new int[n,m];
        
        for (var v = 0; v < n; v++)
        for (int e = 0; e < m; e++)
            finalMatrix[shuffledVertices[v], e] = matrix[v, e];
        return new Hypergraph()
        {
            N = n,
            M = m,
            Matrix = finalMatrix
        };
    }
    
}