using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class NRPA : BaseMonteCarloMethod
{

    private double _alpha = 0.1;

    public NRPA(Hypergraph hypergraph, int maxNumberOfColors, int numberOfEpochs, int maxDepth, int[] vertexOrder,
        double alpha) :
        base(hypergraph, maxNumberOfColors, numberOfEpochs, maxDepth, vertexOrder)
    {
        _alpha = alpha;
    }
    
    public NRPA(Hypergraph hypergraph, int maxNumberOfColors, int numberOfEpochs, int maxDepth, int[] vertexOrder) :
        base(hypergraph, maxNumberOfColors, numberOfEpochs, maxDepth, vertexOrder)
    { }
    
    public NRPA() : base() {}

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
                double result = Execute(vertex+1, colors, level - 1);
                
                if (result >= bestScore)
                {
                    bestScore = result;
                    bestColor = color;
                }

                AdaptPolicy(vertex, colors);
            }

            for (var i = vertex; i < colors.Length; i++)
                colors[_vertexOrder[i]] = -1;
            colors[_vertexOrder[vertex]] = bestColor;
            vertex++;
        }

        return Score(colors);
    }

    private void AdaptPolicy(int vertex, int[] colors)
    {
        double[,] polp = new double[_policy.GetLength(0), _policy.GetLength(1)];
        for (int i = 0; i < _policy.GetLength(0); i++)
            for (int j = 0; j < _policy.GetLength(1); j++)
                polp[i, j] = _policy[i, j];

        for (int v = 0; v<=vertex; v++)
        {
            polp[v, colors[v]] += _alpha;
            double z = 0.0;
            for (int c = 0; c < _maxNumberOfColors; c++)
                z += Math.Exp(_policy[v, c]);

            for (int c = 0; c < _maxNumberOfColors; c++)
                polp[v, c] -= _alpha * (Math.Exp(_policy[v, c])/z);
        }

        _policy = polp;
    }
    
}
