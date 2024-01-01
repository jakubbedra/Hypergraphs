using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class SimplifiedDSatur : BaseGreedy
{
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        int[] coloring = new int[h.N];
        // color all vertices with 0
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = 0;

        _vertexOrder = new int[h.N];
        for (var i = 0; i < _vertexOrder.Length; i++)
            _vertexOrder[i] = i;

        _vertexOrder = _vertexOrder.OrderByDescending(h.VertexDegree).ToArray();
        
        for (var i = 0; i < h.N; i++)
        {
            coloring[_vertexOrder[i]] = GetMinNonConflictingColor(h, _vertexOrder[i], coloring);
        }

        return coloring;
    }

}
