using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Generators;

public class HypertreeGeneratorTest
{
    [Test]
    public void GenerateTest()
    {
        int n = 10;
        int m = 8;
        HypertreeGenerator generator = new HypertreeGenerator();

        Hypergraph hypergraph = generator.Generate(n, m);
        
        Assert.IsNull(hypergraph);
    }
}