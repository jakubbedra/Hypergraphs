using Hypergraphs.Model;

namespace Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;

public class ThreeUniformHypergraphColoring
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
            /**
             * The input is a hypergraph H with vertices Vt, ... , Vn·
0. We initialize Lv = {1, ... , IOJLi} for each v.
1. For i = 1 to n, if Vi is not frozen then:
a) Assign to Vi a random colour c chosen uniformly from Lvi.
b) For each edge {Vi, u, w} where u has colour c and w is uncoloured:
i. Remove c from Lw.
ii. If ILw I = 9J3 then w is bad and we freeze w and all uncoloured
vertices in those hyperedges containing w. 
             */
            if (!_frozenVertices.Contains(v))
            {
                List<int> possibleColors = _possibleVertexColors[v];
                int color = possibleColors[_r.Next(possibleColors.Count)];
                List<List<int>> neighbouringEdges = _hypergraph.GetVertexEdges(v)
                    .Select(e => _hypergraph.GetEdgeVertices(e))
                    .ToList();
                
            }
            
        }
    }

    private void CheckEdgeColors(List<int> edge, int assignedColor)
    {
        // check if monochromatic
        HashSet<int> uncoloredVertices = new HashSet<int>();
        HashSet<int> edgeColors = new HashSet<int>();
        foreach (int vertex in edge)
            if (_colors[vertex] != -1)
                edgeColors.Add(_colors[vertex]);
            else
                uncoloredVertices.Add(vertex);
        if (uncoloredVertices.Count == 1 && edgeColors.Count == 1)
        {
            int sqrtDelta = (int)Math.Floor(Math.Sqrt((double)_delta));
            foreach (int uncoloredVertex in uncoloredVertices)
            {
                _possibleVertexColors[uncoloredVertex].Remove(assignedColor);
                if (_possibleVertexColors[uncoloredVertex].Count == 9 * sqrtDelta)
                {
                    // todo: find all uncolored vertices that are neighbours of w
                    HashSet<int> verticesToFreeze = _hypergraph.GetVertexEdges(uncoloredVertex)
                        .SelectMany(e => _hypergraph.GetEdgeVertices(e).Where(u => _colors[u] == -1))
                        .ToHashSet();

                    foreach (int u in verticesToFreeze)
                        _frozenVertices.Add(u);
                }
            }
        }
    }
    
}