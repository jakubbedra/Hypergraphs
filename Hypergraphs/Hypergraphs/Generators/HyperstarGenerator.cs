using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class HyperstarGenerator
{
    private static Random _r = new Random();

    public Hypergraph Generate(int n, int m, int verticesInCenter = -1)
    {
        // 1. fill in the verticesInCenter first rows
        
        // 2. Select r (or a random value from 1 to n-verticesInCenter) vertices. 
        
        // 3. If there are any vertices left in the verticesInNoEdge set, then we need to make sure, at least one of them is picked in this iteration
        
        // 4. if it's the last iteration -> make sure verticesInNoEdge set is empty, if needed the last edge should contain all the vertices

        // randomize the matrix
        
        return null;
    }

    public Hypergraph Generate2(int n, int m, int verticesInCenter = -1)
    {
        // choose center vertex/vertices
        // 2^(n-|C|) - 1/0 = max_m => |C| = n-log_{2}(max_m + 1/0)
        if (verticesInCenter == -1)
        {
            verticesInCenter = _r.Next(n - (int)Math.Ceiling(Math.Log2(m))) + 1;
        }

        int verticesOutsideCenter = n - verticesInCenter;
        long maxNumberOfDifferentEdges = (long)Math.Pow(2, verticesOutsideCenter);
        maxNumberOfDifferentEdges -= verticesInCenter == 1 ? 1 : 0;

        if (m > maxNumberOfDifferentEdges)
        {
            throw new ArgumentOutOfRangeException(
                "m",
                $"Number of edges should not exceed {maxNumberOfDifferentEdges} with {verticesInCenter} {(verticesInCenter == 1 ? "vertex" : "vertices")} in center."
            );
        }

        int[] verticesArray = GenerateRandomVerticesPermutation(n);
        int[] centerVertices = new int[verticesInCenter];
        int[] permutableVertices = new int[verticesOutsideCenter];

        for (int v = 0; v < verticesInCenter; v++)
        {
            centerVertices[v] = verticesArray[v];
        }

        for (int v = verticesInCenter; v < n; v++)
        {
            permutableVertices[v - verticesInCenter] = verticesArray[v];
        }

        List<List<int>> edges = GenerateEdges(centerVertices, permutableVertices, m);

        return HypergraphFactory.FromHyperEdgesList(n, edges);
    }

    private List<List<int>> GenerateEdges(int[] centerVertices, int[] permutableVertices, int m)
    {
        HashSet<List<int>> edges = new HashSet<List<int>>();
        int addedEdges = 0;
        int i = 0;
        bool[] selectedVertices = new bool[centerVertices.Length + permutableVertices.Length];
        for (var v = 0; v < centerVertices.Length; v++)
            selectedVertices[centerVertices[v]] = true;
        for (var v = 0; v < permutableVertices.Length; v++)
            selectedVertices[permutableVertices[v]] = false;
        while (addedEdges < m)
        {
            List<int> edge = new List<int>();
            foreach (int vertex in centerVertices)
                edge.Add(vertex);

            // add vertices to edge
            //int additionalVerticesInEdge = _r.Next(permutableVertices.Length) + 1;
            int additionalVerticesInEdge = RandomGaussian(permutableVertices.Length) + 1;
            for (int j = i; j < i + additionalVerticesInEdge; j++)
            {
                int vertex = permutableVertices[j % permutableVertices.Length];
                selectedVertices[vertex] = true;
                edge.Add(vertex);
            }

            i += additionalVerticesInEdge;

            if (!edges.Contains(edge))
            {
                edges.Add(edge);
                addedEdges++;
            }
        }

        List<List<int>> edgesList = edges.ToList();
        // choose edges at random to which a vertex will be assigned
        for (int v = 0; v < selectedVertices.Length; v++)
        {
            if (!selectedVertices[v])
            {
                int e = _r.Next(m);
                edgesList[e].Add(v);
                selectedVertices[v] = true;
            }
        }

        return edgesList;
    }

    private int RandomGaussian(int n)
    {
        var normal = new MathNet.Numerics.Distributions.Normal(Math.Sqrt(n - 1), 1.0);
        double z = Math.Floor(normal.Sample());
        return Math.Min((int)z, n);
    }

    private int[] GenerateRandomVerticesPermutation(int n)
    {
        int[] permutation = new int[n];

        for (int i = 0; i < n; i++)
        {
            permutation[i] = i;
        }

        return Shuffle(permutation);
    }

    private int[] Shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int chosen = _r.Next(array.Length - i) + i;
            (array[i], array[chosen]) = (array[chosen], array[i]);
        }

        return array;
    }
}