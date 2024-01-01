using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class MonteCarloHypergraphColoring : BaseColoring<Hypergraph>
{
    private BaseGreedy _greedyAlgorithm;
    private BaseMonteCarloMethod _monteCarloMethod;

    public MonteCarloHypergraphColoring(BaseGreedy greedyAlgorithm, BaseMonteCarloMethod monteCarloMethod)
    {
        _greedyAlgorithm = greedyAlgorithm;
        _monteCarloMethod = monteCarloMethod;
    }

    public override int[] ComputeColoring(Hypergraph h)
    {
        _greedyAlgorithm.ComputeColoring(h);
        int[] vertexOrder = _greedyAlgorithm.GetVertexOrder();
        
        //todo
        
        return _monteCarloMethod.ComputeColoring();
    }
    
}