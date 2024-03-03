using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class NRPAColoring : BaseColoring<Hypergraph>
{
    public override int[] ComputeColoring(Hypergraph hypergraph)
    {
        NRPA nrpa = new NRPA(hypergraph, 2, 10, );
    }
}