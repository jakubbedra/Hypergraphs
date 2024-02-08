using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class MonteCarloHypergraphColoring : BaseColoring<Hypergraph>
{
    private BaseGreedy? _greedyAlgorithm;
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
        int colors = 2;
        int numberOfEpochs = 10;
        int maxDepth = 3;
        HypergraphColoringValidator validator = new HypergraphColoringValidator();
        _monteCarloMethod.UpdateParams(h, colors, numberOfEpochs, maxDepth, vertexOrder);
        int[]? coloring = _monteCarloMethod.ComputeColoring();

        while (!validator.IsValid(h, coloring))
        {
            colors++;
            _monteCarloMethod.UpdateParams(h, colors, numberOfEpochs, maxDepth, vertexOrder);
            coloring = _monteCarloMethod.ComputeColoring();
        }
        
        return coloring;
    }
    
}