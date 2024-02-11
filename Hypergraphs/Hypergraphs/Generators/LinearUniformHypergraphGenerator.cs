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

        HashSet<int> verticesInNoEdge = new HashSet<int>();
        for (int v = 0; v < n; v++)
            verticesInNoEdge.Add(v);
        List<int> chosenVertices = new List<int>();

        for (int e = 0; e < m; e++)
        {
            HashSet<int> edge = GenerateRandomEdge(e, r, matrix, chosenVertices, verticesInNoEdge);
            foreach (int v in edge)
                matrix[v, e] = 1;
        }

        return new Hypergraph()
        {
            N = n,
            M = m,
            Matrix = matrix
        };
    }

    private HashSet<int> GenerateRandomEdge(int e, int r, int[,] matrix, List<int> chosenVertices, HashSet<int> verticesInNoEdge)
    {
        // pick random vertex 
        int v1 = chosenVertices[_r.Next(chosenVertices.Count)];
        HashSet<int> edge = new HashSet<int> { v1 };
        for (int i = 0; i < r; i++)
        {
            int v2 = -1;
            do
            {
                v2 = chosenVertices[_r.Next(chosenVertices.Count)];
            } while (edge.Contains(v2) || Conflict(v2, e, matrix, edge)); // while chosen vertex conflicts

            edge.Add(v2);
            if (verticesInNoEdge.Contains(v2))
            {
                verticesInNoEdge.Remove(v2);
                chosenVertices.Add(v2);
            }
        }

        return edge;
    }

    private bool Conflict(int v, int currentEdge, int[,] matrix, HashSet<int> edge)
    {
        for (int e = 0; e < currentEdge; e++)
        {
            int verticesInEdgeCount = 0;
            if (matrix[v, e] == 1) verticesInEdgeCount++;

            foreach (int vertex in edge)
                if (matrix[vertex, e] == 1)
                    verticesInEdgeCount++;

            if (verticesInEdgeCount > 1) return true;
        }

        return false;
    }
    
}
