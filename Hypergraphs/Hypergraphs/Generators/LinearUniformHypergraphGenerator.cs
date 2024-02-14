using Hypergraphs.Extensions;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class LinearUniformHypergraphGenerator
{
    private static Random _r = new Random();

    public Hypergraph Generate(int n, int m, int r)
    {
        int[,] matrix = new int[n, m];
        for (int v = 0; v < n; v++)
        for (int e = 0; e < m; e++)
            matrix[v, e] = 0;

        for (int e = 0; e < m; e++)
        {
            HashSet<int> edge = GenerateRandomEdge(n, m, e, r, matrix);
            if (edge.Count == r)
            {
                foreach (int v in edge)
                {
                    matrix[v, e] = 1;
                }
            }
        }
        
        Hypergraph hypergraph = new Hypergraph()
        {
            N = n,
            M = m,
            Matrix = matrix
        };
        for (int e = m-1; e >= 0; e--)
            if (hypergraph.GetEdgeVertices(e).Count != r)
                hypergraph.StrongDeleteEdge(e);
        for (int v = n-1; v >= 0; v--)
            if (hypergraph.VertexDegree(v) == 0)
                hypergraph.StrongDeleteVertex(v);
        return hypergraph;
    }

    private HashSet<int> GenerateRandomEdge(int n, int m, int e, int r, int[,] matrix)
    {
        HashSet<int> edge = new HashSet<int>();
        
        List<int> randomVertices = new List<int>();
        for (int v = 0; v < n; v++)
            randomVertices.Add(v);
        randomVertices.Shuffle();

        while (randomVertices.Count > 0 && edge.Count != r)
        {
            int v = randomVertices.First();
            randomVertices.RemoveAt(0);
            edge.Add(v);
            // find all edges that contain v and then all vertices in those edges
            HashSet<int> neighbours = FindNeighbours(n, m, v, e, r, matrix);
            
            // remove neighbours from possible vertices
            foreach (int neighbour in neighbours)
                randomVertices.Remove(neighbour);
        }
        
        return edge;
    }

    private HashSet<int> FindNeighbours(int n, int m, int v, int e, int r, int[,] matrix)
    {
        // find edges that contain v
        HashSet<int> edges = new HashSet<int>();
        for (int e1 = 0; e1 < e; e1++)
            if (matrix[v, e1] == 1)
                edges.Add(e1);
        
        // return all vertices in those edges
        HashSet<int> neighbours = new HashSet<int>();
        foreach (int edge in edges)
            for (int vertex = 0; vertex < n; vertex++)
                if (matrix[vertex, edge] == 1)
                    neighbours.Add(vertex);

        return neighbours;
    }
    
}
