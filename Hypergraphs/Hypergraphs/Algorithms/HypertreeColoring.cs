using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Hypergraphs.Factory;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HypertreeColoring
{
    private int[] _validColoring;
    public int ChromaticNumber => _validColoring.Max() + 1;

    private GreedyColoring _greedyColoring;

    public HypertreeColoring()
    {
        _greedyColoring = new GreedyColoring();
    } 
    
    public int[] Apply(Hypergraph h)
    {
        Graph hostTree = HypertreeHostTreeFactory.FromHypertree(h);
        _validColoring = _greedyColoring.Apply(hostTree);
        return _validColoring;
    }
    
}
