using Hypergraphs.Model;

namespace Hypergraphs.Algorithms.Heuristics.LovaszLocalLemma;

public class UniformHypergraphColoring : BaseUniformHypergraphColoring
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
        // todo: zmienic sqrtDelta !!!! i bedzie git wtedy
        
        // _sqrtDelta = (int)Math.Floor(Math.Sqrt((double)_hypergraph.Delta()));
        int r = hypergraph.EdgeCardinality(0);
        _sqrtDelta = (int)Math.Floor(Math.Pow((double)_hypergraph.Delta(), 1.0/ ((double)r - 1.0)));
        
        _possibleVertexColors = new Dictionary<int, List<int>>();

        InitializePossibleVertexColors();
        PhaseI();
        InitializePossibleVertexColors(10*_sqrtDelta);
        PhaseII();
        InitializePossibleVertexColors(20*_sqrtDelta);
        PhaseIII();

        return _colors;
    }
    
}