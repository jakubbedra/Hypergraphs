namespace Hypergraphs.Extensions;

public static class ListExtensionMethods
{
    
    public static void Shuffle<T>(this List<T> list)
    {
        Random random = new Random();
        int n = list.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    
}