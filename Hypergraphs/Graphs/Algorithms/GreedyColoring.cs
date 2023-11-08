using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class GreedyColoring
{
    private int[] _validColoring;
    private int _chromaticNumber;

    public int[] Apply(Graph g)
    {
        _chromaticNumber = int.MaxValue;
        _validColoring = new int[g.N];
        for (var i = 0; i < g.N; i++)
            _validColoring[i] = -1;

        int v = 0;
        _validColoring[v] = 0;
        Queue<int> q = new Queue<int>();
        q.Enqueue(v);
        while (q.Count != 0)
        {
            v = q.Dequeue();
            List<int> neighbours = g.Neighbours(v);
            foreach (int u in neighbours)
            {
                if (_validColoring[u] == -1)
                {
                    q.Enqueue(u);
                }
            }

            _validColoring[v] = SmallestAvailableColor(neighbours
                .Select(vertex => _validColoring[vertex])
                .ToHashSet()
            );
        }

        return _validColoring;
    }

    private int SmallestAvailableColor(HashSet<int> neighbouringColors)
    {
        int smallestAvailableColor = 0;
        while (neighbouringColors.Contains(smallestAvailableColor))
            smallestAvailableColor++;
        return smallestAvailableColor;
    }
}