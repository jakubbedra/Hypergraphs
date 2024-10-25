﻿using Hypergraphs.Model;

namespace Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;

public abstract class BaseUniformHypergraphColoring
{
    private static Random _r = new Random();

    protected int _sqrtDelta;

    protected int[] _colors;

    protected HashSet<int> _frozenVertices;

    protected Hypergraph _hypergraph;

    protected Dictionary<int, List<int>> _possibleVertexColors;

    public abstract int[] ComputeColoring(Hypergraph hypergraph);

    protected void InitializePossibleVertexColors(int startColor = 0)
    {
        _possibleVertexColors.Clear();
        for (int v = 0; v < _hypergraph.N; v++)
        {
            List<int> possibleColors = new List<int>();
            for (int c = 0; c < 10 * _sqrtDelta; c++)
                possibleColors.Add(c + startColor);
            _possibleVertexColors.Add(v, possibleColors);
        }
    }
    
    protected void PhaseI()
    {
        for (int v = 0; v < _hypergraph.N; v++)
        {
            if (!_frozenVertices.Contains(v))
            {
                List<int> possibleColors = _possibleVertexColors[v];
                int c = possibleColors[_r.Next(possibleColors.Count)];
                _colors[v] = c;
                // for each edge {v,u,w} where color[u] == c and color[w] == -1
                List<List<int>> edges = _hypergraph.GetVertexEdges(v)
                    .Select(e => _hypergraph.GetEdgeVertices(e))
                    .Where(edge => OneVertexUncoloredRestIsMonochromatic(edge, v, c))
                    .ToList();
                foreach (List<int> edge in edges)
                {
                    int w = edge.First(vertex => _colors[vertex] == -1);
                    _possibleVertexColors[w].Remove(c);
                    if (_possibleVertexColors[w].Count == 9 * _sqrtDelta)
                        FreezeVertexAndItsUncoloredNeighbours(w);
                }
            }
        }
    }
    
    protected void PhaseII()
    {
        List<HashSet<int>> connectedComponents = FindUncoloredConnectedComponents(); // set of edges for each component
        // for those vertices that are only in the found components
        connectedComponents.ForEach(RandomlyColorComponent);
    }

    protected void PhaseIII()
    {
        // complete the coloring by brute-force
        List<HashSet<int>> connectedComponents = FindUncoloredConnectedComponents();
        connectedComponents.ForEach(BruteForceColorComponent);
    }

    private void BruteForceColorComponent(HashSet<int> componentEdges)
    {
        // find uncolored vertices
        List<int> uncoloredVertices = componentEdges.ToList()
            .SelectMany(e => _hypergraph.GetEdgeVertices(e))
            .Where(v => _colors[v] == -1)
            .ToList();
        // brute-force using the vertices' lists
        BruteForceColoring(componentEdges, uncoloredVertices, 0);
    }

    private bool BruteForceColoring(HashSet<int> componentEdges, List<int> vertices, int currentVertex)
    {
        if (currentVertex == vertices.Count)
        {
            return !componentEdges.Any(IsEdgeMonochromatic);
        }
        foreach (int c in _possibleVertexColors[currentVertex])
        {
            _colors[currentVertex] = c;
            if (BruteForceColoring(componentEdges, vertices, currentVertex + 1))
                return true;
            _colors[currentVertex] = -1;
        }
        return false;
    }
    
    private bool IsEdgeMonochromatic(int e)
    {
        HashSet<int> edgeColors = new HashSet<int>();
        _hypergraph.GetEdgeVertices(e).ForEach(v => edgeColors.Add(_colors[v]));
        return edgeColors.Count > 1;
    }
    
    private void RandomlyColorComponent(HashSet<int> componentEdges)
    {
        HashSet<int> componentVertices = componentEdges
            .SelectMany(e => _hypergraph.GetEdgeVertices(e))
            .ToHashSet();
        foreach (int vertex in componentVertices)
        {
            if (!_frozenVertices.Contains(vertex))
            {
                List<int> vertexColors = _possibleVertexColors[vertex];
                int c = vertexColors[_r.Next(vertexColors.Count)];
                _colors[vertex] = c;
                List<List<int>> edges = _hypergraph.GetVertexEdges(vertex)
                    .Select(e => _hypergraph.GetEdgeVertices(e))
                    .Where(edge => OneVertexUncoloredRestIsMonochromatic(edge, vertex, c))
                    .ToList();
                edges.ForEach(edge => edge.Where(u =>_colors[u] == -1).ToList().ForEach(u => _possibleVertexColors[u].Remove(c)));
                if (vertexColors.Count == 8 * _sqrtDelta)
                {
                    _frozenVertices.Add(vertex);
                    foreach (int u in _hypergraph.Neighbours(vertex))
                        _frozenVertices.Add(u);
                }
            }
        }
    }
    
    private bool OneVertexUncoloredRestIsMonochromatic(List<int> edgeVertices, int recentlyColoredVertex, int chosenColor)
    {
        int uncoloredVerticesCount = edgeVertices.Count(v => _colors[v] == -1);
        int verticesWithChosenColor = edgeVertices.Count(v => _colors[v] == chosenColor);
        return uncoloredVerticesCount == 1 && verticesWithChosenColor == edgeVertices.Count - 1;
    }
    
    private List<HashSet<int>> FindUncoloredConnectedComponents()
    {
        List<HashSet<int>> connectedComponents = new List<HashSet<int>>();

        for (int e = 0; e < _hypergraph.M; e++)
        {
            if (_hypergraph.GetEdgeVertices(e).Any(v => _colors[v] == -1))
            {
                // if any set contains an edge that is adjacent to the current one, add the current one to the set
                bool setFound = false;
                foreach (HashSet<int> connectedComponent in connectedComponents)
                {
                    if (connectedComponent.Any(e2 => _hypergraph.EdgeIntersection(e, e2).Count != 0))
                    {
                        connectedComponent.Add(e);
                        setFound = true;
                        break;
                    }
                }

                // if not, create a new set
                if (!setFound)
                {
                    connectedComponents.Add(new HashSet<int>() { e });
                }
            }
        }

        return connectedComponents;
    }

    private void FreezeVertexAndItsUncoloredNeighbours(int v)
    {
        _frozenVertices.Add(v);
        HashSet<int> neighbours = _hypergraph.Neighbours(v);
        foreach (int u in neighbours)
        {
            if (_colors[u] == -1)
                _frozenVertices.Add(u);
        }
    }
    
}
