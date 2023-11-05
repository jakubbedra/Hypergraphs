using Hypergraphs.Model;

namespace Hypergraphs.Hypergraphs.Factory;

public class DualHypergraphFactory
{
    public static Hypergraph FromHypergraph(Hypergraph h)
    {
        int[,] matrixDual = new int[h.M, h.N];
        for (int i = 0; i < h.N; i++)
        for (int j = 0; j < h.N; j++)
            matrixDual[j, i] = h.Matrix[i, j];

        return new Hypergraph()
        {
            Matrix = matrixDual,
            N = h.M,
            M = h.N
        };
    }
}