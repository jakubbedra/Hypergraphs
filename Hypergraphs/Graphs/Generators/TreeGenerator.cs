using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Generators;

public class TreeGenerator
{
    private readonly Random _r;

    public TreeGenerator()
    {
        _r = new Random();
    }
    
    public Graph Generate(int n)
    {
        Graph g = new Graph(n);
        for (int v = 1; v < n; v++)
        {
            int u = _r.Next(v);
            g.AddEdge(v, u);
        }

        return g;
    }
    
}