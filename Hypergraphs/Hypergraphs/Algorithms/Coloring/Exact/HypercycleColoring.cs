using Hypergraphs.Algorithms;
using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Algorithms.PCTrees;
using Hypergraphs.Model;

public class HypercycleColoring : BaseColoring<Hypergraph>
{
    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        // remove only ones columns
        List<int> onlyOnes = new List<int>();
        for (int e = 0; e < hypergraph.M; e++)
        {
            bool isAllEdge = true;
            for (var v = 0; v < hypergraph.N; v++)
            {
                if (hypergraph.Matrix[v, e] == 1)
                {
                    isAllEdge = false;
                    break;
                }
            }

            if (isAllEdge) onlyOnes.Add(e);
        }

        int[,] newMatrix = new int[hypergraph.N, hypergraph.M - onlyOnes.Count];
        int currentColumn = 0;
        for (int e = 0; e < hypergraph.M; e++)
        {
            if (!onlyOnes.Contains(e))
            {
                for (var v = 0; v < hypergraph.N; v++)
                    newMatrix[v, currentColumn] = hypergraph.Matrix[v, e];
                currentColumn++;
            }
        }

        PCTree tree = new PCTree(newMatrix, hypergraph.N, hypergraph.M - onlyOnes.Count(), true);
        tree.Construct();
        int[]? permutation = tree.GetPermutation();
        if (permutation == null || !IsCycleHost(permutation, hypergraph))
            throw new ArgumentException("Given hypergraph is not a hypercycle.");
        
        int[] colors = new int[hypergraph.N];
        // if has subhypergraph C_n
        if (HasCycleGraph(hypergraph, permutation))
        {
            if (hypergraph.N % 2 == 1)
            {
                for (int v = 0; v < hypergraph.N - 1; v++)
                    colors[permutation[v]] = v % 2;
                colors[permutation[hypergraph.N - 1]] = 3;
            }
            else
                for (int v = 0; v < hypergraph.N; v++)
                    colors[permutation[v]] = v % 2;
            return colors;
        }
        
        for (int i = 0; i < colors.Length; i++)
            colors[i] = -1;

        // check if the hypergraph has odd number of vertices and contains 2-edges
        List<List<int>> twoEdges = TwoEdgesFinder.FindTwoEdgeSequences(hypergraph);
        HashSet<int> coloredVertices = new HashSet<int>();
        if (hypergraph.N % 2 == 1 && twoEdges.Count != 0)
        {
            // color 2-edges
            foreach (List<int> twoEdge in twoEdges)
            {
                int color = 0;
                foreach (int v in twoEdge)
                {
                    colors[v] = color;
                    color++;
                    color %= 2;
                    coloredVertices.Add(v);
                    // List<int> edgeVertices = hypergraph.GetEdgeVertices(e);
                    // if (colors[edgeVertices[0]] == -1 && colors[edgeVertices[1]] == -1)
                    // {
                    //     colors[edgeVertices[0]] = 0;
                    //     colors[edgeVertices[1]] = 1;
                    // }
                    // else if (colors[edgeVertices[1]] == -1) /////////////////////////////////////////////////
                    //     colors[edgeVertices[1]] = (colors[edgeVertices[0]] + 1) % 2;
                    // else
                    //     colors[edgeVertices[0]] = (colors[edgeVertices[1]] + 1) % 2;
                    //
                    // edgeVertices.ForEach(v => coloredVertices.Add(v));
                }
            }

            // color rest of the edges
            for (int v = 0; v < hypergraph.N; v++)
            {
                if (!coloredVertices.Contains(v))
                {
                    // assign lowest possible color 
                    // int currentColor = 0;
                    colors[v] = GetMinNonConflictingColor2(hypergraph, v, colors);
                    // while (IsAnyEdgeMonochromatic(hypergraph, v, colors))
                    // {
                    //     currentColor++;
                    //     colors[v] = currentColor;
                    // }
                }
            }

            return colors;
        }

        for (int i = 0; i < permutation.Length; i++)
            colors[permutation[i]] = i % 2;

        return colors;
    }

    private bool HasCycleGraph(Hypergraph hypergraph, int[] permutation)
    {
        int n = hypergraph.N;
        int m = hypergraph.M;
        List<HashSet<int>> twoEdges = new List<HashSet<int>>();
        for (int e = 0; e < m; e++)
        {
            List<int> edgeVertices = hypergraph.GetEdgeVertices(e);
            if (edgeVertices.Count == 2)
                twoEdges.Add(edgeVertices.ToHashSet());
        }
        return twoEdges.Count == n;
    }

    private bool IsAnyEdgeMonochromatic(Hypergraph hypergraph, int vertex, int[] colors)
    {
        List<int> vertexEdges = hypergraph.GetVertexEdges(vertex);
        foreach (int e in vertexEdges)
        {
            HashSet<int> edgeColors = new HashSet<int>(); 
            foreach (int v in hypergraph.GetEdgeVertices(e))
                edgeColors.Add(colors[v]);

            if (edgeColors.Count == 1) return true;
        }

        return false;
    }
    protected int GetMinNonConflictingColor2(Hypergraph h, int vertex, int[] coloring)
    {
        HashSet<int> conflictingColors = GetConflictingColors2(h, vertex, coloring);
        int currentMinColor = 0;

        while (conflictingColors.Contains(currentMinColor))
            currentMinColor++;
        return currentMinColor;
    }

    protected HashSet<int> GetConflictingColors2(Hypergraph h, int vertex, int[] coloring)
    {
        List<int> vertexEdges = h.GetVertexEdges(vertex);
        HashSet<int> conflictingColors = new HashSet<int>();

        foreach (int e in vertexEdges)
        {
            List<Tuple<int, int>> edgeColors = h.GetEdgeVertices(e)
                .Where(u => u != vertex)
                .Where(u => coloring[u] != -1)
                .Select(u => new Tuple<int, int>(u, coloring[u]))
                .ToList();
            if (edgeColors.Count == h.EdgeCardinality(e) - 1)
            {
                int color = edgeColors[0].Item2;
                if (edgeColors.All(c => c.Item2 == color))
                    conflictingColors.Add(color);
            }
        }

        return conflictingColors;
    }

    private bool IsCycleHost(int[] permutation, Hypergraph hypergraph)
    {
        for (var i = 0; i < permutation.Length; i++)
        {
            List<int> e1 = hypergraph.GetVertexEdges(permutation[i]);
            List<int> e2 = hypergraph.GetVertexEdges(permutation[(i + 1) % permutation.Length]);
            if (!e1.Intersect(e2).Any()) return false;
        }

        return true;
    }

    private bool TwoEdgeExists(Hypergraph hypergraph)
    {
        for (int e = 0; e < hypergraph.M; e++)
            if (hypergraph.EdgeCardinality(e) == 2)
                return true;

        return false;
    }

    private List<List<int>> GetTwoEdges(Hypergraph hypergraph)
    {
        List<int> twoEdges = new List<int>();
        for (int e = 0; e < hypergraph.M; e++)
            if (hypergraph.EdgeCardinality(e) == 2)
                twoEdges.Add(e);

        HashSet<int> checkedEdges = new HashSet<int>();
        List<List<int>> connectedTwoEdges = new List<List<int>>();
        for (var e1 = 0; e1 < twoEdges.Count; e1++)
        {
            if (checkedEdges.Contains(e1)) continue;
            List<int> tmp = new List<int>() { e1 };
            for (var e2 = e1 + 1; e2 < twoEdges.Count; e2++)
                if (hypergraph.EdgesIntersect(e1, e2))
                {
                    tmp.Add(e2);
                    checkedEdges.Add(e2);
                }

            checkedEdges.Add(e1);
            connectedTwoEdges.Add(tmp);
        }

        return connectedTwoEdges;
    }
}