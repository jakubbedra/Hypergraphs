using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

public class HyperstarGeneratorTest
{
    [Test]
    public void GenerateTest_SingleVertexInCenter()
    {
        int n = 10;
        int m = 5;
        int verticesInCenter = 1;
        HyperstarGenerator generator = new HyperstarGenerator();
        HyperstarCheck check = new HyperstarCheck();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        
        Hypergraph h = generator.Generate(n, m, verticesInCenter);
        bool result = check.Apply(h);
        bool isConnected = connectivityCheck.Apply(h);
        // 6/10 wierzcholkow, nie nalezy do zadnej krawedzi :<
        // todo: finish this shit
        Assert.That(result, Is.True);
        Assert.That(isConnected, Is.True);
    }
    
}