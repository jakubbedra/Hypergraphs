using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Hypergraphs.Factory;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HypertreeColoring : BaseColoring<Hypergraph>
{
    private int[] _validColoring;
    public int ChromaticNumber => _validColoring.Max() + 1;

    private GreedyColoring _greedyColoring;

    public HypertreeColoring()
    {
        _greedyColoring = new GreedyColoring();
    } 
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        Graph hostTree = HypertreeHostTreeFactory.FromHypertree(h);
        _validColoring = _greedyColoring.Apply(hostTree);
        return _validColoring;
    }
    
}