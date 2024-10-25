using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public class NestedMonteCarloSearch
{
    private static Random _random = new Random();
    
    private int _maxNumberOfColors;
    
    private Hypergraph _hypergraph;
    private double[,] _policy;

    public int NumberOfEpochs { get; set; }
    
    public NestedMonteCarloSearch(Hypergraph hypergraph, int maxNumberOfColors, int numberOfEpochs)
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
            Tuple<double,List<Move>> result = Execute(new List<Move>(), 3);
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

        List<Move> bestSequence = new List<Move>();
        List<Move> bestMoves = new List<Move>();
        List<Move> possibleMoves = GetPossibleMoves(state);

        while (state.Count != _hypergraph.N)
        {
            bestSequence.Clear();
            double bestSequenceScore = double.MinValue;
            Move? bestMove = null;
            foreach (Move move in possibleMoves)
            {
                List<Move> s = new List<Move>(state) { move };
                Tuple<double,List<Move>> result = Execute(s, level - 1);
                if (result.Item1 > bestSequenceScore)
                {
                    bestSequenceScore = result.Item1;
                    bestSequence = result.Item2;
                    bestMove = move;
                }
            }
            state.Add(bestMove);
            bestMoves.Add(bestMove);

            possibleMoves.Remove(bestMove);
            possibleMoves = possibleMoves.Where(m => m.Vertex != bestMove.Vertex).ToList();
        }

        return new Tuple<double, List<Move>>(Score(state), bestMoves);
    }
    
    private Tuple<double, List<Move>> Playout(List<Move> state)
    {
        List<Move> sequence = new List<Move> { state.Last() };
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
            if (state.All(s => s.Vertex != v))
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
