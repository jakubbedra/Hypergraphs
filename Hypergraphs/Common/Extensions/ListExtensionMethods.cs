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
    
    public static void RotateLeft<T>(this List<T> list, int positions)
    {
        int count = list.Count;

        // Calculate the effective rotation index
        int rotationIndex = positions % count;

        // Handle the case where no rotation is needed
        if (rotationIndex == 0) return;

        // Perform cyclic rotation
        List<T> rotatedPart = list.GetRange(0, count - rotationIndex);
        rotatedPart.Reverse();
        list.RemoveRange(0, count - rotationIndex);
        rotatedPart.ForEach(item => list.Add(item));
    }
    
    public static void RotateRight<T>(this List<T> list, int positions)
    {
        int count = list.Count;

        // Calculate the effective rotation index
        int rotationIndex = positions % count;
        if (rotationIndex == 0) return;
        // Perform right cyclic rotation
        List<T> rotatedPart = list.GetRange(count - rotationIndex, rotationIndex);
        list.RemoveRange(count - rotationIndex, rotationIndex);
        list.InsertRange(0, rotatedPart);
    }
    
}
