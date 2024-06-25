using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class OtherGreedy : BaseGreedy
{
    private static Random _r = new Random();

    public override int[] ComputeColoring(Hypergraph h)
    {
        int[] coloring = new int[h.N];
        // color all vertices with 0
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = -1;

        // int[] dupa = { 1,2,3, 0 };
        // _vertexOrder = dupa;
        _vertexOrder = new int[h.N];
        for (var i = 0; i < _vertexOrder.Length; i++) _vertexOrder[i] = i;
        double[] x = new double[h.N];
        for (var i = 0; i < x.Length; i++) x[i] = _r.NextDouble();
        Array.Sort(x, _vertexOrder);
        
        for (var i = 0; i < h.N; i++)
        {
            coloring[_vertexOrder[i]] = GetMinNonConflictingColor(h, _vertexOrder[i], coloring);
        }

        return coloring;
    }

}