using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class NMCS : BaseMonteCarloMethod
{

    public NMCS(Hypergraph hypergraph, int maxNumberOfColors, int numberOfEpochs, int maxDepth, int[] vertexOrder) :
        base(hypergraph, maxNumberOfColors, numberOfEpochs, maxDepth, vertexOrder)
    { }
    
    public NMCS() : base() {}

    public override double Execute(int vertex, int[] colors, int level)
    {
        if (level == 0) return Playout(vertex, colors);

        while (vertex < _hypergraph.N)
        {
            double bestScore = double.MinValue;
            int bestColor = -1;
            List<int> possibleMoves = GetPossibleMoves(vertex, colors);

            foreach (int color in possibleMoves)
            {
                colors[_vertexOrder[vertex]] = color;
                // colors[vertex] = color;
                double result = Execute(vertex + 1, colors, level - 1);
                if (result >= bestScore)
                {
                    bestScore = result;
                    bestColor = color;
                }
            }

            for (var i = vertex; i < colors.Length; i++)
                colors[_vertexOrder[i]] = -1;
            colors[_vertexOrder[vertex]] = bestColor;
            // colors[vertex] = bestColor;
            vertex++;
        }

        return Score(colors);
    }

}