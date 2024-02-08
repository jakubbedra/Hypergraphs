using Hypergraphs.Graphs.Algorithms;

namespace HypergraphsTests.Graphs.Algorithms;

public class ConsecutiveOnesTest
{
    [Test]
    public void GetPermutation_ConsecutiveOnes_SingleConnectedComponent()
    {
        int[,] matrix =
        {
            { 1, 0, 0, 1, 1, 0 }, // 0
            { 0, 0, 1, 0, 0, 1 }, // 1
            { 1, 1, 1, 1, 0, 1 }, // 2
            { 0, 0, 0, 0, 1, 0 }, // 3
            { 1, 1, 1, 1, 1, 0 }, // 4
            { 1, 0, 0, 0, 1, 0 }, // 5
        };
        int n = 6;
        int m = 6;
        int[] expectedPermutation = { 3, 5, 0, 4, 2, 1 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.EqualTo(expectedPermutation));
    }
    
    [Test]
    public void GetPermutation_BioLeft()
    {
        int[,] matrix =
        {
            { 0, 0, 0, 0, 1, 0, 0, 1 }, // 0
            { 1, 1, 1, 0, 0, 0, 0, 0 }, // 1
            { 1, 1, 0, 0, 0, 0, 0, 0 }, // 2
            { 1, 0, 1, 0, 0, 0, 1, 0 }, // 3
            { 0, 1, 0, 0, 1, 0, 0, 1 }, // 4
            { 0, 1, 0, 0, 1, 0, 0, 1 }, // 5
            { 0, 0, 0, 0, 0, 1, 1, 1 }, // 6
            { 0, 0, 0, 0, 1, 0, 1, 1 }, // 7
            { 0, 0, 0, 1, 0, 0, 1, 1 }, // 8
        };
        int n = 9;
        int m = 8;
        int[] expectedPermutation = { 3, 5, 0, 4, 2, 1 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.Null);
    }
    
    [Test]
    public void GetPermutation_BioRight()
    {
        int[,] matrix =
        {
            { 0, 0, 0, 1, 1, 0, 0, 1 }, // 0
            { 1, 0, 1, 1, 0, 0, 0, 0 }, // 1
            { 1, 0, 0, 1, 0, 0, 0, 0 }, // 2
            { 1, 0, 1, 1, 0, 0, 0, 0 }, // 3
            { 0, 1, 0, 1, 1, 0, 0, 1 }, // 4
            { 0, 1, 0, 1, 1, 0, 0, 1 }, // 5
            { 0, 0, 0, 1, 0, 1, 1, 1 }, // 6
            { 0, 0, 0, 1, 1, 0, 0, 1 }, // 7
            { 0, 0, 0, 0, 0, 0, 1, 1 }, // 8
        };
        int n = 9;
        int m = 8;
        int[] expectedPermutation = { 3, 5, 0, 4, 2, 1 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.Null);
    }
    
    [Test]
    public void GetPermutation_Bio3()
    {
        int[,] matrix =
        {
            { 0, 1, 0, 0, 0, 1, 0 }, // 0
            { 0, 1, 0, 1, 0, 1, 1 }, // 1
            { 0, 1, 0, 1, 1, 0, 0 }, // 2
            { 1, 1, 0, 1, 0, 1, 1 }, // 3
            { 0, 1, 1, 1, 1, 0, 0 }, // 4
            { 1, 1, 0, 1, 0, 1, 1 }, // 5
            { 0, 0, 0, 1, 1, 0, 0 }, // 6
            { 0, 1, 0, 0, 0, 1, 0 }, // 7
        };
        int n = 8;
        int m = 7;
        int[] expectedPermutation = { 3, 5, 0, 4, 2, 1 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.Null);
    }

    [Test]
    public void GetPermutation_ConsecutiveOnes_TwoConnectedComponents()
    {
        int[,] matrix =
        {
            { 1, 0, 1, 1, 1, 1 }, // 0
            { 1, 1, 1, 0, 1, 0 }, // 1
            { 1, 1, 1, 0, 0, 0 }, // 2
            { 1, 0, 1, 0, 1, 1 }, // 3
            { 1, 0, 0, 1, 0, 1 }, // 4
        };
        int n = 5;
        int m = 6;
        int[] expectedPermutation = { 2, 1, 3, 0, 4 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.EqualTo(expectedPermutation));
    }

    [Test]
    public void GetPermutation_ConsecutiveOnes_FourConnectedComponents()
    {
        int[,] matrix =
        {
            { 1, 0, 0, 0, 0, 0, 0, 0 }, // 0
            { 1, 1, 1, 0, 0, 0, 1, 0 }, // 1
            { 0, 1, 0, 1, 1, 0, 0, 0 }, // 2
            { 1, 1, 1, 0, 0, 1, 0, 1 }, // 3
            { 1, 1, 1, 0, 0, 0, 0, 1 }, // 4
            
            { 0, 1, 0, 0, 1, 0, 0, 0 }, // 5
            { 1, 1, 1, 0, 0, 1, 1, 0 }, // 6
            { 0, 1, 0, 1, 0, 0, 0, 0 }, // 7
            { 1, 1, 1, 0, 0, 0, 0, 1 }, // 8
        };
        int n = 9;
        int m = 8;
        int[] expectedPermutation = { 0, 1, 6, 3, 4, 8, 7, 2, 5 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.EqualTo(expectedPermutation));
    }
    
    [Test]
    public void GetPermutation_ConsecutiveOnes_FiveConnectedComponents()
    {
        int[,] matrix =
        {
            { 1, 1, 1, 0, 0, 0, 1, 1, 0, 1 }, // 3
            { 0, 1, 0, 1, 0, 1, 0, 0, 0, 0 }, // 7
            { 0, 1, 0, 0, 1, 1, 0, 0, 0, 0 }, // 9
            { 1, 1, 1, 0, 0, 0, 1, 0, 0, 1 }, // 1
            { 0, 1, 0, 1, 1, 1, 0, 0, 0, 0 }, // 8
            { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1 }, // 5
            { 1, 1, 1, 0, 0, 0, 0, 0, 1, 0 }, // 6
            { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0 }, // 10
            { 1, 1, 1, 0, 0, 0, 1, 1, 0, 1 }, // 2
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 0
            { 1, 1, 1, 0, 0, 0, 0, 1, 0, 1 }, // 4
        };
        int n = 11;
        int m = 10;
        int[] expectedPermutation = { 9, 6, 5, 3, 0, 8, 10, 1, 4, 2, 7 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.EqualTo(expectedPermutation));
    }

    [Test] // TODO
    public void GetPermutation_ConsecutiveOnes_DuplicatedColumns()
    {
        int[,] matrix =
        {
            { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 0
            { 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0 }, // 1
            { 0, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0 }, // 2
            { 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 1 }, // 3
            { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 }, // 4
            
            { 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 }, // 5
            { 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 0 }, // 6
            { 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0 }, // 7
            { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 }, // 8
        };
        int n = 9;
        int m = 11;
        int[] expectedPermutation = { 0, 1, 6, 3, 4, 8, 7, 2, 5 };
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();

        Assert.That(permutation, Is.EqualTo(expectedPermutation));
    }

    [Test]
    public void GetPermutation_NotConsecutiveOnes()
    {
        int[,] matrix =
        {   //0  1  2  3  4  5  6  7  8
            { 1, 0, 0, 0, 0, 0, 0, 0, 0 }, // 0
            { 1, 1, 0, 0, 0, 0, 0, 0, 0 }, // 1
            { 1, 1, 1, 0, 0, 0, 0, 0, 0 }, // 2
            { 0, 1, 1, 1, 0, 0, 0, 0, 0 }, // 3
            { 0, 0, 1, 0, 0, 0, 0, 0, 0 }, // 4

            { 0, 0, 0, 1, 1, 0, 0, 0, 0 }, // 5
            { 0, 0, 0, 1, 1, 0, 1, 0, 0 }, // 6
            { 0, 0, 0, 1, 0, 0, 1, 0, 0 }, // 7
            { 0, 0, 0, 0, 0, 1, 1, 0, 0 }, // 8
            { 0, 0, 0, 0, 0, 1, 1, 1, 0 }, // 9

            { 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // 10
            { 0, 0, 0, 0, 0, 0, 0, 1, 1 }, // 11
            { 0, 0, 0, 0, 0, 0, 0, 1, 1 }, // 12
        };
        int n = 13;
        int m = 9;
        ConsecutiveOnes c1p = new ConsecutiveOnes(matrix, n, m);
        
        int[]? permutation = c1p.GetPermutation();
        
        Assert.That(permutation, Is.Null); 
    }

}