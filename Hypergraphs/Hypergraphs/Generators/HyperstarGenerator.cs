using Hypergraphs.Common.Comparator;
using Hypergraphs.Extensions;
using Hypergraphs.Model;
using MathNet.Numerics.Random;

namespace Hypergraphs.Generators;

public class HyperstarGenerator
{
    private static Random _r = new Random();

    public Hypergraph Generate(int n, int m, int verticesInCenter = 1)
    {
        int[,] matrix = new int[n, m];
        for (int v = 0; v < n; v++)
        for (int e = 0; e < m; e++)
            matrix[v, e] = 0;

        // todo: if a vertex is in no edge, then add it in last iteration/edge
        HashSet<int> verticesInNoEdge = new HashSet<int>();
        for (int v = 0; v < n; v++)
            verticesInNoEdge.Add(v);
        
        // 1. fill in the verticesInCenter first rows
        for (int v = 0; v < verticesInCenter; v++)
        for (int e = 0; e < m; e++)
            matrix[v, e] = 1;
        
        var comparer = new HashSetEqualityComparer<int>();
        HashSet<HashSet<int>> edges = new HashSet<HashSet<int>>(comparer);
        while (edges.Count != m)
        {
            HashSet<int> edge = new HashSet<int>();
            for (int v = verticesInCenter; v < n; v++)
                if (_r.NextBoolean())
                    edge.Add(v);
            edges.Add(edge);
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
        
        // check if any new vertices were added to the center
        List<int> additionalVerticesInCenter = new List<int>();
        for (int v = verticesInCenter; v < n; v++)
            if (RowIsOnlyOnes(m, v, matrix))
                additionalVerticesInCenter.Add(v);

        int selectedEdge = _r.Next(m);
        foreach (int v in additionalVerticesInCenter)
            matrix[v, selectedEdge] = 0;
        
        // check for isolated vertices
        if (verticesInNoEdge.Count != 0)
        {
            selectedEdge = _r.Next(m);
            foreach (int v in verticesInNoEdge)
                matrix[v, selectedEdge] = 1;
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
            Matrix = matrix//todo: finalMatrix
        };
    }

    private bool RowIsOnlyOnes(int m, int row, int[,] matrix)
    {
        for (int e = 0; e < m; e++)
            if (matrix[row, e] == 0)
                return false;
        return true;
    }
    
}