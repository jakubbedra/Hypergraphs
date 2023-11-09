using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Generators;

public class UniformHypergraphGeneratorTest
{
    [Test]
    public void GenerateSimpleTest()
    {
        UniformHypergraphGenerator generator = new UniformHypergraphGenerator();
        int n = 10;
        int m = 6;
        int r = 3;
        
        Hypergraph h = generator.GenerateSimple(n,m,r);

        Assert.That(h.N, Is.EqualTo(n));
        Assert.That(h.M, Is.EqualTo(m));
        for (int i = 0; i < m; i++)
        {
            Assert.That(h.EdgeCardinality(i), Is.EqualTo(r));
        }
    }
}