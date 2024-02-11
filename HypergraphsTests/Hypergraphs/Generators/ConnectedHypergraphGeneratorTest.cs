using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Generators;

public class ConnectedHypergraphGeneratorTest
{
     private static readonly int Iterations = 100;

    [Test]
    public void GenerateTest_SmallHypergraph()
    {
        int n = 10;
        int m = 5;
        int verticesInCenter = 1;
        ConnectedHypergraphGenerator generator = new ConnectedHypergraphGenerator();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(isConnected, Is.True);
        }
    }
    
    [Test]
    public void GenerateTest_MediumHypergraph()
    {
        int n = 21;
        int m = 37;
        int verticesInCenter = 1;
        ConnectedHypergraphGenerator generator = new ConnectedHypergraphGenerator();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(isConnected, Is.True);
        }
    }
    
    [Test]
    public void GenerateTest_BigHypergraph()
    {
        int n = 1000;
        int m = 109;
        ConnectedHypergraphGenerator generator = new ConnectedHypergraphGenerator();
        HypergraphConnectivityCheck connectivityCheck = new HypergraphConnectivityCheck();
        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph h = generator.Generate(n, m);
            bool isConnected = connectivityCheck.Apply(h);
            Assert.That(isConnected, Is.True);
        }
    }
}