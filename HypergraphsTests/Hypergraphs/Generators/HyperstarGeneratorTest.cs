using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

public class HyperstarGeneratorTest
{

    private static readonly int Iterations = 100;

    [Test]
    public void Export()
    {
        
    }
    
    
    [Test]
    public void GenerateTest_SmallHyperstar()
    {
        int n = 10;
        int m = 5;
        int verticesInCenter = 1;
        HyperstarGenerator generator = new HyperstarGenerator();
        HyperstarCheck check = new HyperstarCheck();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m, verticesInCenter);
            bool result = check.Apply(h);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(result, Is.True);
            Assert.That(isConnected, Is.True);
        }
    }
    
    [Test]
    public void GenerateTest_MediumHyperstar()
    {
        int n = 21;
        int m = 37;
        int verticesInCenter = 1;
        HyperstarGenerator generator = new HyperstarGenerator();
        HyperstarCheck check = new HyperstarCheck();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m, verticesInCenter);
            bool result = check.Apply(h);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(result, Is.True);
            Assert.That(isConnected, Is.True);
        }
    }
    
    [Test]
    public void GenerateTest_BigHyperstar()
    {
        int n = 1000;
        int m = 109;
        int verticesInCenter = 1;
        HyperstarGenerator generator = new HyperstarGenerator();
        HyperstarCheck check = new HyperstarCheck();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m, verticesInCenter);
            bool result = check.Apply(h);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(result, Is.True);
            Assert.That(isConnected, Is.True);
        }
    }
    
    [Test]
    public void GenerateTest_SmallHyperstar_MoreVerticesInCenter()
    {
        int n = 10;
        int m = 5;
        int verticesInCenter = 6;
        HyperstarGenerator generator = new HyperstarGenerator();
        HyperstarCheck check = new HyperstarCheck();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m, verticesInCenter);
            bool result = check.Apply(h);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(result, Is.True);
            Assert.That(isConnected, Is.True);
        }
    }
    
    [Test]
    public void GenerateTest_MediumHyperstar_MoreVerticesInCenter()
    {
        int n = 21;
        int m = 37;
        int verticesInCenter = 10;
        HyperstarGenerator generator = new HyperstarGenerator();
        HyperstarCheck check = new HyperstarCheck();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m, verticesInCenter);
            bool result = check.Apply(h);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(result, Is.True);
            Assert.That(isConnected, Is.True);
        }
    }
    
    [Test]
    public void GenerateTest_BigHyperstar_MoreVerticesInCenter()
    {
        int n = 1000;
        int m = 109;
        int verticesInCenter = 69;
        HyperstarGenerator generator = new HyperstarGenerator();
        HyperstarCheck check = new HyperstarCheck();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m, verticesInCenter);
            bool result = check.Apply(h);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(result, Is.True);
            Assert.That(isConnected, Is.True);
        }
    }
    
}