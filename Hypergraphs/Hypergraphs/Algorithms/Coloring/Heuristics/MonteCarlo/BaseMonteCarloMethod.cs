using Hypergraphs.Model;

namespace Hypergraphs.Algorithms;

public abstract class BaseMonteCarloMethod
{
    protected static Random _random = new Random();

    protected int _maxDepth;
    protected int _maxNumberOfColors;

    protected int[] _vertexOrder;
    
    protected Hypergraph _hypergraph;
    protected double[,] _policy;

    public int NumberOfEpochs { get; set; }
    
    public BaseMonteCarloMethod() {}

    public BaseMonteCarloMethod(Hypergraph hypergraph, int maxNumberOfColors, int numberOfEpochs, int maxDepth, int[] vertexOrder)
    {
        _hypergraph = hypergraph;
        _maxNumberOfColors = maxNumberOfColors; // at least 2 colors
        _policy = new double[hypergraph.N, _maxNumberOfColors];
        for (int i = 0; i < hypergraph.N; i++)
        for (int j = 0; j < maxNumberOfColors; j++)
            _policy[i, j] = 1.0;
        NumberOfEpochs = numberOfEpochs;
        _maxDepth = maxDepth;
        _vertexOrder = vertexOrder;
    }

    public void UpdateParams(Hypergraph hypergraph, int maxNumberOfColors, int numberOfEpochs, int maxDepth, int[] vertexOrder)
    {
        _hypergraph = hypergraph;
        _maxNumberOfColors = maxNumberOfColors; // at least 2 colors
        _policy = new double[hypergraph.N, _maxNumberOfColors];
        for (int i = 0; i < hypergraph.N; i++)
        for (int j = 0; j < maxNumberOfColors; j++)
            _policy[i, j] = 1.0;
        NumberOfEpochs = numberOfEpochs;
        _maxDepth = maxDepth;
        _vertexOrder = vertexOrder;
    }
    
    public int[]? ComputeColoring()
    {
        // for N times execute
        int epoch = 0;

        double score = 0.0;
        int[]? colors = null;
        do
        {
            colors = new int[_hypergraph.N];
            for (var i = 0; i < colors.Length; i++)
                colors[i] = -1;
            score = Execute(0, colors, _maxDepth);
            epoch++;
        } while (!(Math.Abs(score - _hypergraph.M) < 0.01) && epoch < NumberOfEpochs);

        if (!(Math.Abs(score - _hypergraph.M) < 0.01)) return null;

        return colors;
    }
    
    public abstract double Execute(int vertex, int[] colors, int level);
    
    protected double Playout(int vertex, int[] colors)
    {
        while (vertex < _hypergraph.N)
        {
            List<int> possibleMoves = GetPossibleMoves(vertex, colors);

            double z = 0.0;
            // for all possible moves
            Dictionary<int, double> cumulativeProbabilities = new Dictionary<int, double>();

            foreach (var move in possibleMoves)
            {
                z += Math.Exp(_policy[_vertexOrder[vertex], move]);
                cumulativeProbabilities[move] = z;
            }

            double choice = _random.NextDouble() * z;

            int chosenMove = -1;
            foreach (var kvp in cumulativeProbabilities)
            {
                if (choice <= kvp.Value)
                {
                    chosenMove = kvp.Key;
                    break;
                }
            }

            colors[_vertexOrder[vertex]] = chosenMove;
            // colors[vertex] = chosenMove;
            vertex++;
        }
        return Score(colors);
    }

    protected List<int> GetPossibleMoves(int vertex, int[] colors)
    {
        int next = vertex;
        List<int> possibleMoves = new List<int>();
        for (int c = 0; c < _maxNumberOfColors; c++)
        {
            colors[_vertexOrder[next]] = c;
            // colors[next] = c;
            if (_hypergraph.GetVertexEdges(next).All(e => !IsMonochromatic(e, colors)))
            {
                possibleMoves.Add(c);
            }
        }

        if (possibleMoves.Count == 0)
            possibleMoves.Add(_random.Next(_maxNumberOfColors));

        return possibleMoves;
    }

    protected double Score(int[] colors)
    {
        double totalScore = _hypergraph.M;

        for (int e = 0; e < _hypergraph.M; e++)
            if (IsMonochromatic(e, colors))
                totalScore -= 1.0;

        return totalScore;
    }

    protected bool IsMonochromatic(int e, int[] colors)
    {
        HashSet<int> edgeColors = new HashSet<int>();
        for (int v = 0; v < _hypergraph.N; v++)
            if (_hypergraph.Matrix[_vertexOrder[v], e] != 0)
                edgeColors.Add(colors[_vertexOrder[v]]);
        return edgeColors.Count == 1;
    }
    
}