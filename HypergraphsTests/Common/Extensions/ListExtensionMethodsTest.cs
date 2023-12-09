using Hypergraphs.Extensions;

namespace HypergraphsTests.Common.Extensions;

public class ListExtensionMethodsTest
{
    [Test]
    public void RotateLeftTest()
    {
        List<int> list = new List<int>() { 2,1,3,7,6,9 };
        List<int> expected = new List<int>() { 3,7,6,9,2,1 };
        int rotateBy = 2;
        
        list.RotateLeft(rotateBy);
        
        for (var i = 0; i < list.Count; i++)
            Assert.True(list[i] == expected[i]);
    }
}