using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class ThreeColorableHypercycleGenerator
{
    private HypercycleGenerator _hypercycleGenerator;

    public ThreeColorableHypercycleGenerator()
    {
        _hypercycleGenerator = new HypercycleGenerator(true);
    }

    public Hypergraph Generate(int n, int m)
    {
        if (n % 2 != 1) throw new ArgumentException($"Number of vertices should be odd, but was {n}.");
        if (m - n < 0) throw new ArgumentException();
        
        Hypergraph hypergraph = _hypercycleGenerator.Generate3Colorable(n, m);
        return hypergraph;
    }

}