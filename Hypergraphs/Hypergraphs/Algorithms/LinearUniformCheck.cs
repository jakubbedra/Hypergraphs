using Hypergraphs.Common.Algorithms;
using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class LinearUniformCheck : PropertyCheck<Hypergraph>
{
    private UniformCheck _uniformCheck;
    private LinearCheck _linearCheck;
    
    public LinearUniformCheck()
    {
        _uniformCheck = new UniformCheck();
        _linearCheck = new LinearCheck();
    }
    
    public bool Apply(Hypergraph hypergraph)
    {
        return _uniformCheck.Apply(hypergraph) && _linearCheck.Apply(hypergraph);
    }
    
}