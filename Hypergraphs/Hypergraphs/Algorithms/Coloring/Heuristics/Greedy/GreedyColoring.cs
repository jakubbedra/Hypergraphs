using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class GreedyColoring : BaseGreedy
{
    private static Random _r = new Random();

    private readonly bool _randomizeVertices;
    private int[]? _startPermutation;

    public int[]? StartPermutation
    {
        set => _startPermutation = value;
    }

    public GreedyColoring(bool randomizeVertices = false)
    {
        _randomizeVertices = randomizeVertices;
        _startPermutation = null;
    }

    public GreedyColoring(int[] startPermutation)
    {
        _randomizeVertices = false;
        _startPermutation = startPermutation;
    }

    public override int[] ComputeColoring(Hypergraph h)
    {
        int[] coloring = new int[h.N];
        // color all vertices with 1
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = 0; //TODO: warning!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        _vertexOrder = new int[h.N];
        if (_startPermutation == null)
        {
            for (var i = 0; i < _vertexOrder.Length; i++)
                _vertexOrder[i] = i;
        }
        else
        {
            for (var i = 0; i < _vertexOrder.Length; i++)
                _vertexOrder[i] = _startPermutation[i];
        }

        // randomize vertices
        if (_randomizeVertices)
        {
            double[] x = new double[h.N];
            for (var i = 0; i < x.Length; i++)
                x[i] = _r.NextDouble();

            Array.Sort(x, _vertexOrder);
        }

        HashSet<int> visitedEdges = new HashSet<int>();

        for (var i = 0; i < _vertexOrder.Length; i++)
        {
            int currentMinColor = 0; //TODO: warning!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            for (int j = 0; j < h.M; j++)
            {
                if (h.Matrix[_vertexOrder[i], j] != 0 && !visitedEdges.Contains(j))
                {
                    visitedEdges.Add(j);
                    // recolor the vertex so that edge j is not monochromatic
                    currentMinColor = GetMinNonConflictingColor(h, _vertexOrder[i], coloring);
                }
            }

            coloring[_vertexOrder[i]] = currentMinColor;
        }

        return coloring;
    }

}