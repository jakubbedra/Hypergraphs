using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class ClassicGreedy : BaseGreedy
{
    private static Random _r = new Random();

    public override int[] ComputeColoring(Hypergraph h)
    {
        int[] coloring = new int[h.N];
        // color all vertices with -1
        for (var i = 0; i < coloring.Length; i++)
            coloring[i] = -1;

        _vertexOrder = new int[h.N];
        for (var i = 0; i < _vertexOrder.Length; i++) _vertexOrder[i] = i;
        double[] x = new double[h.N];
        for (var i = 0; i < x.Length; i++) x[i] = _r.NextDouble();
        Array.Sort(x, _vertexOrder);
        
        for (var i = 0; i < h.N; i++)
        {
            coloring[_vertexOrder[i]] = GetMinNonConflictingColor2(h, _vertexOrder[i], coloring);
        }

        return coloring;
    }

}