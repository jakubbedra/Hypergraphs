using Hypergraphs.Common.Algorithms;
using Hypergraphs.Graphs.Algorithms;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Hypergraphs.Factory;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HypertreeCheck : PropertyCheck<Hypergraph>
{
    public bool Apply(Hypergraph h)
    {
        HellyCheck hellyCheck = new HellyCheck();
        ChordalityCheck chordalityCheck = new ChordalityCheck();
        
        Graph lineGraph = LineGraphFactory.FromHypergraph(h);

        return hellyCheck.IsHelly(h) && chordalityCheck.Apply(lineGraph);
    }
}