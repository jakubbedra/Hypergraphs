using Hypergraphs.Model;

namespace Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;

public class ThreeUniformHypergraphColoring
{
    private static Random _r = new Random();

    private int _sqrtDelta;

    private int[] _colors;

    private HashSet<int> _frozenVertices;

    private Hypergraph _hypergraph;

    private Dictionary<int, List<int>> _possibleVertexColors;
    
    public int[] ComputeColoring(Hypergraph hypergraph)
    {
        _colors = new int[hypergraph.N];
        for (var v = 0; v < _colors.Length; v++)
        {
            _colors[v] = -1; // uncolored
        }

        _frozenVertices = new HashSet<int>();
        _hypergraph = hypergraph;
        _sqrtDelta = (int)Math.Floor(Math.Sqrt((double)_hypergraph.Delta()));
        _possibleVertexColors = new Dictionary<int, List<int>>();
        InitializePossibleVertexColors();
        
        PhaseI();
        PhaseII();
        PhaseIII();

        return _colors;
    }
    
    private void InitializePossibleVertexColors()
    {
        for (int v = 0; v < _hypergraph.N; v++)
        {
            List<int> possibleColors = new List<int>();
            for (int c = 0; c < 10 * _sqrtDelta; c++)
                possibleColors.Add(c);
            _possibleVertexColors.Add(v, possibleColors);
        }
    }
    
    private void PhaseI()
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
    
    private void PhaseII()
    {
        List<HashSet<int>> connectedComponents = FindUncoloredConnectedComponents(); // set of edges for each component
        // for those vertices that are only in the found components
    }

    private void PhaseIII()
    {
        // complete the coloring by brute-force
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