using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class LargestFirstEdgeSizes : BaseGreedy
{
    public override int[] ComputeColoring(Hypergraph h)
    {
        int[] coloring = new int[h.N];
        // color all vertices with 0
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = -1;

        _vertexOrder = new int[h.N];
        for (var i = 0; i < _vertexOrder.Length; i++)
            _vertexOrder[i] = i;

        // calculate edge sizes and weights
        Dictionary<int, double> edgeWeights = new Dictionary<int, double>();
        for (int e = 0; e < h.M; e++)
        {
            int edgeSize = h.EdgeCardinality(e);
            edgeWeights.Add(e, 1.0 / ((double)edgeSize));
        }
        
        _vertexOrder = _vertexOrder.OrderByDescending(v => h.GetVertexEdges(v).Sum(e => edgeWeights[e])).ToArray();
        
        for (var i = 0; i < h.N; i++)
        {
            coloring[_vertexOrder[i]] = GetMinNonConflictingColor2(h, _vertexOrder[i], coloring);
        }

        return coloring;
    }

}