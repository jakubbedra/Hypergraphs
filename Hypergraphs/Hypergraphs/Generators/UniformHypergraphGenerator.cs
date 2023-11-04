using Hypergraphs.Extensions;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class UniformHypergraphGenerator : BaseGenerator
{
    public UniformHypergraphGenerator() {}
    
    public Hypergraph GenerateSimple(int n, int m, int r)
    {
        List<int> vertices = new List<int>();
        for (int i = 0; i < n; i++)
        {
            vertices.Add(i);
        }

        vertices.Shuffle();
        List<List<int>> edges = GenerateNCombinations(vertices, m, r);
        return HypergraphFactory.FromHyperEdgesList(n, edges);
    }

    public Hypergraph GenerateConnected(int n, int m, int r)
    {
        throw new NotImplementedException();
    }

}
