using Hypergraphs.Model;

namespace Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;

public class ThreeUniformHypergraphColoring : BaseUniformHypergraphColoring
{
    
    public override int[] ComputeColoring(Hypergraph hypergraph)
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
    
}