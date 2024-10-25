using Hypergraphs.Model;

namespace Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;

public class ThreeUniformHypergraphColoringVVVV
{
    private static Random _r = new Random();

    private int _delta;

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
        _delta = _hypergraph.Delta();
        _possibleVertexColors = new Dictionary<int, List<int>>();

        FirstPhase();

        return _colors;
    }

    private void FirstPhase()
    {
        // initialize Lv
        int sqrtDelta = (int)Math.Floor(Math.Sqrt((double)_delta));
        for (int v = 0; v < _hypergraph.N; v++)
        {
            List<int> possibleColors = new List<int>();
            for (int c = 0; c < 10 * sqrtDelta; c++)
                possibleColors.Add(c);
            _possibleVertexColors.Add(v, possibleColors);
        }

        for (int v = 0; v < _hypergraph.N; v++)
        {
            if (!_frozenVertices.Contains(v))
            {
                // a)
                List<int> possibleColors = _possibleVertexColors[v];
                int color = possibleColors[_r.Next(possibleColors.Count)];

                // b)
                List<List<int>> neighbouringEdges = _hypergraph.GetVertexEdges(v)
                    .Select(e => _hypergraph.GetEdgeVertices(e))
                    .ToList();
                neighbouringEdges.ForEach(edge => ColorEdge(edge, v, color));
            }
        }
    }

    private void ThirdPhase()
    {
        // find connected components of not completely colored edges

        // brute-force the coloring of leftover components (check all of the combinations from list of possible colors)
    }

    private void SecondPhase()
    {
        // find connected components of not completely colored edges
        List<HashSet<int>> connectedComponents = FindUncoloredConnectedComponents();

        // continue the coloring for each component
        
    }
/**
 * 


FAZA I:
1. L[v] = {1, ... , 10*sqrt(delta)}
2. for v = 1 to n if v is not frozen then:
	a) colors[v] = random c from L[v]
	b) for each edge {v,u,w} where color[u] == c and color[w] == -1
		i. L[w].Remove(c)
		ii. if L[w].Count == 9*sqrt(delta):
			1. frozenVertices.Add(w)
			2. hypergraph.Neighbours(w).Foreach(frozenVertices.Add)

FAZA II:
1. find not completely colored connected components, then for each of them do 2.
2. for v = 1 to n if v is not frozen then:
	a) colors[v] = random c from L[v]
	b) for each edge {v,u,w} where color[w] == c and color[u] == -1
		i. L[u].Remove(c)
	c) if L[v].Count == 8*sqrt(delta):
		i. frozenVertices.Add(v)
		ii. hypergraph.Neighbours(v).Foreach(frozenVertices.Add)


 */
    private void ColorConnectedComponent(HashSet<int> connectedComponent)
    {
        foreach (int e in connectedComponent)
        {
            List<int> edgeVertices = _hypergraph.GetEdgeVertices(e);
            
            // if c is the same color as w, remove c from Lu
            foreach (int v in edgeVertices)
            {
                if (!_frozenVertices.Contains(v))
                {
                    // a)
                    List<int> possibleColors = _possibleVertexColors[v];
                    int color = possibleColors[_r.Next(possibleColors.Count)];

                    // b)
                    List<List<int>> neighbouringEdges = _hypergraph.GetVertexEdges(v)
                        .Select(e => _hypergraph.GetEdgeVertices(e))
                        .ToList();
                    neighbouringEdges.ForEach(edge => ColorEdgePhase2(edge, v, color));
                }
            }
        }
    }
    
    // testy moge zrobic tak, ze po prostu sprawdzam czy pokolorowanie jest ok

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

    private void ColorEdge(List<int> edge, int coloredVertex, int assignedColor)
    {
        // check if monochromatic
        _colors[coloredVertex] = assignedColor;
        HashSet<int> uncoloredVertices = new HashSet<int>();
        HashSet<int> edgeColors = new HashSet<int>();
        foreach (int vertex in edge)
            if (_colors[vertex] != -1)
                edgeColors.Add(_colors[vertex]);
            else if (_colors[vertex] == -1)
                uncoloredVertices.Add(vertex);
        if (uncoloredVertices.Count == 1 && edgeColors.Count == 1)
        {
            int sqrtDelta = (int)Math.Floor(Math.Sqrt((double)_delta));
            foreach (int uncoloredVertex in uncoloredVertices)
            {
                _possibleVertexColors[uncoloredVertex].Remove(assignedColor);
                if (_possibleVertexColors[uncoloredVertex].Count == 9 * sqrtDelta)
                {
                    HashSet<int> verticesToFreeze = _hypergraph.GetVertexEdges(uncoloredVertex)
                        .SelectMany(e => _hypergraph.GetEdgeVertices(e).Where(u => _colors[u] == -1))
                        .ToHashSet();

                    foreach (int u in verticesToFreeze)
                        _frozenVertices.Add(u);
                }
            }
        }
    }
    
    private void ColorEdgePhase2(List<int> edge, int coloredVertex, int assignedColor)
    {
        // check if monochromatic
        _colors[coloredVertex] = assignedColor;
        HashSet<int> uncoloredVertices = new HashSet<int>();
        HashSet<int> edgeColors = new HashSet<int>();
        foreach (int vertex in edge)
            if (_colors[vertex] != -1)
                edgeColors.Add(_colors[vertex]);
            else if (_colors[vertex] == -1)
                uncoloredVertices.Add(vertex);
        if (uncoloredVertices.Count == 1 && edgeColors.Count == 1)
        {
            int sqrtDelta = (int)Math.Floor(Math.Sqrt((double)_delta));
            foreach (int uncoloredVertex in uncoloredVertices)
            {
                _possibleVertexColors[uncoloredVertex].Remove(assignedColor);
                
                for (int e = 0; e < _hypergraph.M; e++)
                    RemoveColorIfOneVertexUncoloredRestIsMonochromatic(e, assignedColor);
                
                if (_possibleVertexColors[uncoloredVertex].Count == 8 * sqrtDelta)
                {
                    HashSet<int> verticesToFreeze = _hypergraph.GetVertexEdges(uncoloredVertex)
                        .SelectMany(e => _hypergraph.GetEdgeVertices(e).Where(u => _colors[u] == -1))
                        .ToHashSet();

                    foreach (int u in verticesToFreeze)
                        _frozenVertices.Add(u);
                }
            }
        }
    }

    private void RemoveColorIfOneVertexUncoloredRestIsMonochromatic(int e, int assignedColor)
    {
        List<int> edgeVertices = _hypergraph.GetEdgeVertices(e);
        bool doRemove = edgeVertices.Count(v => _colors[v] == -1) == 1 &&
               edgeVertices.Count(v => _colors[v] == assignedColor) == edgeVertices.Count - 1;
        if (doRemove)
        {
            int uncoloredVertex = edgeVertices.First(v => _colors[v] == -1);
            _possibleVertexColors[uncoloredVertex].Remove(assignedColor);
        }
    }
    
}