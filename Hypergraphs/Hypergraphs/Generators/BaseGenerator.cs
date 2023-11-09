namespace Hypergraphs.Generators;

public abstract class BaseGenerator
{
    private int _count;
    
    protected List<List<T>> GenerateNCombinations<T>(List<T> input, int count, int k)
    {
        _count = count;
        List<List<T>> result = new List<List<T>>();
        List<T> currentCombination = new List<T>();
        GenerateSpecificCombinations(input, k, 0, currentCombination, result);
        return result;
    }

    protected void GenerateSpecificCombinations<T>(List<T> input, int k, int startIndex, List<T> currentCombination, List<List<T>> result)
    {
        if (_count == 0)
        {
            return; // Stop generating more combinations once we've reached the desired count.
        }

        if (k == 0)
        {
            result.Add(new List<T>(currentCombination));
            _count--;
            return;
        }

        for (int i = startIndex; i <= input.Count - k; i++)
        {
            currentCombination.Add(input[i]);
            GenerateSpecificCombinations(input, k - 1, i + 1, currentCombination, result);
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }
    }
    
}