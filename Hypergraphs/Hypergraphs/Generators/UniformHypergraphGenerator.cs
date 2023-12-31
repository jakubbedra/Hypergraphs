using Hypergraphs.Extensions;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class UniformHypergraphGenerator : BaseGenerator
{
    private static Random _random = new Random();

    public UniformHypergraphGenerator()
    {
    }

    public Hypergraph GenerateSimple(int n, int m, int r)
    {
        List<int> vertices = new List<int>();
        for (int i = 0; i < n; i++)
        {
            vertices.Add(i);
        }

        vertices.Shuffle();
        List<List<int>> edges = GenerateNCombinations(vertices, m, r);
        return HypergraphFactory.FromHyperEdgesList(n, edges);
    }


    public Hypergraph GenerateConnected(int n, int m, int r)
    {
        if (m * (r - 1) < n)
        {
            throw new Exception("Invalid m and r in relation to n.");
        }

        Stack<int> vertices = new Stack<int>();
        for (int i = 0; i < n; i++)
            vertices.Push(i);

        List<List<int>> hyperedges = new List<List<int>>();

        // construct random uniform hyperpath

        List<int> previousEdge = new List<int>();
        for (int i = 0; i < r; i++)
            previousEdge.Add(vertices.Pop());

        hyperedges.Add(previousEdge);
        m--;

        while (vertices.Count >= r - 1)
        {
            List<int> currentEdge = new List<int>();
            for (int i = 0; i < r - 1; i++)
            {
                currentEdge.Add(vertices.Pop());
            }

            currentEdge.Add(previousEdge[_random.Next(r)]);
            previousEdge = currentEdge;
            hyperedges.Add(previousEdge);
            m--;
        }

        if (vertices.Count > 0)
        {
            List<int> currentEdge = new List<int>();
            int verticesCount = vertices.Count;
            for (int i = 0; i < verticesCount; i++)
            {
                currentEdge.Add(vertices.Pop());
            }

            List<int> previousEdgeCopy = new List<int>(previousEdge);
            for (int i = 0; i < r - verticesCount; i++)
            {
                int choice = previousEdgeCopy[_random.Next(previousEdgeCopy.Count)];
                currentEdge.Add(choice);
                previousEdgeCopy.Remove(choice);
            }

            previousEdge = currentEdge;
            hyperedges.Add(previousEdge);
            m--;
        }

        // add more edges
        int[] verticesDegree = new int[n];
        for (var i = 0; i < verticesDegree.Length; i++)
            verticesDegree[i] = 0;
        foreach (List<int> hyperedge in hyperedges)
        foreach (int v in hyperedge)
            verticesDegree[v]++;

        Hypergraph hypergraph = HypergraphFactory.FromHyperEdgesList(n, hyperedges);
        List<int> incorrectEdges2 = new List<int>();
        for (int e = 0; e < hypergraph.M; e++)
        {
            int edgeCardinality = hypergraph.EdgeCardinality(e);
            if (edgeCardinality != r)
                incorrectEdges2.Add(e);// todo: to 4 tworzy sie przed whilem juz 0_0
        }
        // choose a random vertex
        // connect those edges, which are no longer of size r, with a random vertex, that is not included in any of them
        while (m > 0)
        {
            // delete random vertex
            hypergraph.WeakDeleteVertex(_random.Next(n));

            // search edges which are no longer of size r
            List<int> incorrectEdges = new List<int>();
            for (int e = 0; e < hypergraph.M; e++)
            {
                int edgeCardinality = hypergraph.EdgeCardinality(e);
                if (edgeCardinality != r)
                    incorrectEdges.Add(e);// todo: to 4 tworzy sie przed whilem juz 0_0
            }
            List<int> canBeInEdges = new List<int>();
            for (int v = 0; v < n; v++)
                if (incorrectEdges.All(e => !hypergraph.VertexInEdge(v, e)))
                    canBeInEdges.Add(v);
            int chosenVertex = canBeInEdges[_random.Next(canBeInEdges.Count)];
            foreach (int e in incorrectEdges)
                hypergraph.AddVertexToEdge(chosenVertex, e);

            // form a new edge for the deleted vertex
            List<int> allVertices = new List<int>();
            for (int v = 0; v < hypergraph.N; v++)
                allVertices.Add(v);

            List<int> newEdge = new List<int>();
            for (int i = 0; i < r - 1; i++)
            {
                int choice = allVertices[_random.Next(allVertices.Count)];
                newEdge.Add(choice);
                allVertices.Remove(choice);
            }

            hypergraph.AddVertex();
            hypergraph.AddEdge();
            newEdge.Add(hypergraph.N - 1);
            newEdge.ForEach(v => hypergraph.AddVertexToEdge(v, hypergraph.M - 1));
            m--;
        }
        
        return hypergraph;
    }
    
}