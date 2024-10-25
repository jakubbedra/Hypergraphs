using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class NestedRolloutPolicyAdaptation
{
    private static Random _random = new Random();

    private double _alpha = 0.1;
    
    private int _maxNumberOfColors;

    private Hypergraph _hypergraph;
    private double[,] _policy;

    public int NumberOfEpochs { get; set; }

    public NestedRolloutPolicyAdaptation(Hypergraph hypergraph, int maxNumberOfColors, int numberOfEpochs)
    {
        _hypergraph = hypergraph;
        _maxNumberOfColors = maxNumberOfColors; // at least 2 colors
        _policy = new double[hypergraph.N, _maxNumberOfColors];
        for (int i = 0; i < hypergraph.N; i++)
        for (int j = 0; j < maxNumberOfColors; j++)
            _policy[i, j] = 1.0;
        NumberOfEpochs = numberOfEpochs;
    }

    public int[]? ComputeColoring()
    {
        // for N times execute
        int epoch = 0;

        double score = 0.0;
        List<Move> moves = new List<Move>();
        do
        {
            Tuple<double, List<Move>> result = Execute(new List<Move>(), 3);
            score = result.Item1;
            moves = result.Item2;
            epoch++;
        } while (Math.Abs(score - _hypergraph.M) < 0.01 && epoch < NumberOfEpochs);

        if (!(Math.Abs(score - _hypergraph.M) < 0.01)) return null;

        int[] coloring = new int[_hypergraph.N];
        for (var v = 0; v < coloring.Length; v++)
            coloring[v] = moves.Where(m => m.Vertex == v).First().Color;

        return coloring;
    }

    public Tuple<double, List<Move>> Execute(List<Move> state, int level)
    {
        if (level == 0) return Playout(state);

        int N = 100;
        double bestScore = double.MinValue;
        List<Move> seq = new List<Move>();
        Move bestMove = null;
        
        List<Move> possibleMoves = GetPossibleMoves(state);
        foreach (Move move in possibleMoves)
        {
            state.Add(move);
            Tuple<double, List<Move>> result = Execute(state, level - 1);
            state.Remove(move);
            
            if (result.Item1 >= bestScore)
            {
                bestScore = result.Item1;
                seq = result.Item2;
                bestMove = move;
            }

            AdaptPolicy(seq);
        }
        state.Add(bestMove);
        return new Tuple<double, List<Move>>(Score(seq), seq);
    }

    private void AdaptPolicy(List<Move> seq)
    {
        double[,] polp = new double[_policy.GetLength(0), _policy.GetLength(1)];
        for (int i = 0; i < _policy.GetLength(0); i++)
            for (int j = 0; j < _policy.GetLength(1); j++)
                polp[i, j] = _policy[i, j];

        List<Move> state = new List<Move>();
        foreach (Move move in seq)
        {
            polp[move.Vertex, move.Color] += _alpha;
            double z = 0.0;
            List<Move> possibleMoves = GetPossibleMoves(state);

            foreach (var m in possibleMoves)
                z += Math.Exp(_policy[m.Vertex, m.Color]);

            foreach (var m in possibleMoves)
            {
                polp[m.Vertex, m.Color] -= _alpha * (Math.Exp(_policy[m.Vertex, m.Color])/z);
            }

            state.Add(move);
        }

        _policy = polp;
    }

    private Tuple<double, List<Move>> Playout(List<Move> state)
    {
        List<Move> sequence = state.Count != 0 ? new List<Move> { state.Last() } : new List<Move>();
        List<Move> possibleMoves = GetPossibleMoves(state);
        while (true)
        {
            if (state.Count == _hypergraph.N)
            {
                return new Tuple<double, List<Move>>(Score(state), sequence);
            }

            double z = 0.0;
            // for all possible moves
            Dictionary<Move, double> cumulativeProbabilities = new Dictionary<Move, double>();

            foreach (var move in possibleMoves)
            {
                z += Math.Exp(_policy[move.Vertex, move.Color]);
                cumulativeProbabilities[move] = z;
            }

            double choice = _random.NextDouble() * z;

            Move? chosenMove = null;
            foreach (var kvp in cumulativeProbabilities)
            {
                if (choice <= kvp.Value)
                {
                    chosenMove = kvp.Key;
                    break;
                }
            }

            state.Add(chosenMove);
            sequence.Add(chosenMove);

            possibleMoves.Remove(chosenMove);
            possibleMoves = possibleMoves.Where(move => move.Vertex != chosenMove.Vertex).ToList();
        }
    }

    private List<Move> GetPossibleMoves(List<Move> state)
    {
        List<Move> possibleMoves = new List<Move>();
        for (int v = 0; v < _hypergraph.N; v++)
            if (state.Count == 0 || state.All(s => s.Vertex != v))
                for (int c = 0; c < _maxNumberOfColors; c++)
                    possibleMoves.Add(new Move(v, c));
        return possibleMoves;
    }

    private double Score(List<Move> state)
    {
        // return number of monochromatic edges, if there are none at the end - we found the k-coloring
        int[] colors = new int [_hypergraph.N];
        foreach (Move move in state)
            colors[move.Vertex] = move.Color;

        double totalScore = _hypergraph.M;

        for (int e = 0; e < _hypergraph.M; e++)
            if (IsMonochromatic(e, colors))
                totalScore -= 1.0;

        return totalScore;
    }

    private bool IsMonochromatic(int e, int[] colors)
    {
        HashSet<int> edgeColors = new HashSet<int>();
        for (int v = 0; v < _hypergraph.N; v++)
            if (_hypergraph.Matrix[v, e] != 0)
                edgeColors.Add(colors[v]);

        return edgeColors.Count == 1;
    }
}