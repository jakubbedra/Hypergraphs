using Hypergraphs.Algorithms;
using Hypergraphs.Generators;
using Hypergraphs.Model;

namespace HypergraphsTests.Generators;

public class HypertreeGeneratorTest
{
    private static readonly int Iterations = 100;
    
    [Test]
    public void GenerateTest_SmallHypertree()
    {
        int n = 10;
        int m = 8;
        HypertreeGenerator generator = new HypertreeGenerator();
        HypertreeCheck check = new HypertreeCheck();

        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            bool result = check.Apply(hypergraph);

            Assert.IsTrue(result);            
        }
    } 
    
    [Test]
    public void GenerateTest_MediumHypertree()
    {
        int n = 21;
        int m = 37;
        HypertreeGenerator generator = new HypertreeGenerator();
        HypertreeCheck check = new HypertreeCheck();

        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            bool result = check.Apply(hypergraph);

            Assert.IsTrue(result);            
        }
    }
    
    [Test]
    public void GenerateTest_BigHypertree()
    {
        int n = 100;
        int m = 50;
        HypertreeGenerator generator = new HypertreeGenerator();
        HypertreeCheck check = new HypertreeCheck();

        for (int i = 0; i < Iterations; i++)
        {
            Hypergraph hypergraph = generator.Generate(n, m);
            bool result = check.Apply(hypergraph);

            Assert.IsTrue(result);            
        }
    }
    
}