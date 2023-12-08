using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class HyperpathCheck : PropertyCheck<Hypergraph>
{
    public bool Apply(Hypergraph h)
    {
        // check is simple connected ( no edge duplicates, no 1-vertex edges, no empty edges )
        
        // check consecutive ones
        
        return true;
    }
}