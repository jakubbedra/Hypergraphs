namespace Hypergraphs.Model;
//todo: rename to HypergraphBuilder? or dunno xd
public class HypergraphFactory
{
    public static Hypergraph FromHyperEdgesList(int n, List<List<int>> hyperEdges)
    {
        Hypergraph hypergraph = new Hypergraph(n, hyperEdges.Count);
        hypergraph.N = n;
        hypergraph.M = hyperEdges.Count;
        int e = 0;
        foreach (List<int> hyperEdge in hyperEdges)
        {
            foreach (var v in hyperEdge)
            {
                hypergraph.Matrix[v, e] = 1;
            }

            e++;
        }

        return hypergraph;
    }

}