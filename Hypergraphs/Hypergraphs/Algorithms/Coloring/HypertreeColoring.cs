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

    private GraphGreedyColoring _graphGreedyColoring;

    public HypertreeColoring()
    {
        _graphGreedyColoring = new GraphGreedyColoring();
    } 
    
    public override int[] ComputeColoring(Hypergraph h)
    {
        Graph hostTree = HypertreeHostTreeFactory.FromHypertree(h);
        _validColoring = _graphGreedyColoring.Apply(hostTree);
        return _validColoring;
    }
    
}