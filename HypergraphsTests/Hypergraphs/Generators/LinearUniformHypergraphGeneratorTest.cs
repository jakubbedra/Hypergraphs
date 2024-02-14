using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Generators;

public class LinearUniformHypergraphGeneratorTest
{
    private static readonly int Iterations = 100;
    
    [Test]
    public void GenerateTest_SmallHypergraph()
    {
        int n = 9;
        int m = 4;
        int r = 4;
        LinearUniformHypergraphGenerator generator = new LinearUniformHypergraphGenerator();
        LinearUniformCheck check = new LinearUniformCheck();

        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m, r);
            bool result = check.Apply(hypergraph);

            Assert.IsTrue(result);            
        }
    } 
    
    [Test]
    public void GenerateTest_MediumHypergraph()
    {
        int n = 21;
        int m = 37;
        int r = 10;
        LinearUniformHypergraphGenerator generator = new LinearUniformHypergraphGenerator();
        LinearUniformCheck check = new LinearUniformCheck();

        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m, r);
            bool result = check.Apply(hypergraph);

            Assert.IsTrue(result);            
        }
    }
    
    [Test]
    public void opGenerateTest_BigHypergraph()
    {
        int n = 100;
        int m = 50;
        int r = 10;
        LinearUniformHypergraphGenerator generator = new LinearUniformHypergraphGenerator();
        LinearUniformCheck check = new LinearUniformCheck();

        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m, r);
            bool result = check.Apply(hypergraph);

            Assert.IsTrue(result);            
        }
    }
    
}
