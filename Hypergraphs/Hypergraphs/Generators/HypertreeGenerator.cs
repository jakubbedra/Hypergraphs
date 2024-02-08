using Hypergraphs.Graphs.Generators;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class HypertreeGenerator
{
    private static readonly Random _r = new Random();
    
    public Hypergraph Generate(int n, int m)
    {
        List<int> chosenVertices = new List<int>();
        List<int> verticesInNoEdge = new List<int>();
        for (int v = 0; v < n; v++)
            verticesInNoEdge.Add(v);
        TreeGenerator generator = new TreeGenerator();
        Graph tree = generator.Generate(n);

        for (int e = 0; e < m; e++)
        {
            if (verticesInNoEdge.Count != 0)
            {
                int startVertex = verticesInNoEdge[_r.Next(verticesInNoEdge.Count)];
                // do a random walk on the subtree
            }

        }
        
        return null;
    }  
    
}