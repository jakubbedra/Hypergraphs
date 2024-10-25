using Hypergraphs.Extensions;
using Hypergraphs.Generators.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class HypercycleGenerator
{
    private static Random _r = new Random();

    private bool _forceCycleSubhypergraph;

    public HypercycleGenerator(bool forceCycleSubhypergraph = false)
    {
        _forceCycleSubhypergraph = forceCycleSubhypergraph;
    }

    public Hypergraph Generate(int n, int m)
    {
        int[,] matrix = GenerateHypercycleMatrix(n, m);

        List<int> vertices = new List<int>();
        for (int i = 0; i < n; i++)
            vertices.Add(i);
        vertices.Shuffle();
        int col = 0;
        int[,] finalMatrix = new int[n, m];
        foreach (int vertex in vertices)
        {
            for (int e = 0; e < m; e++)
                finalMatrix[col, e] = matrix[vertex, e];
            col++;
        }

        return new Hypergraph()
        {
            N = n,
            M = m,
            Matrix = finalMatrix
        };
    }

    public Hypergraph Generate3Colorable(int n, int m)
    {
        int[,] matrix1 = GenerateHypercycleMatrix(n, m - n);
        int[,] matrix = new int[n, m];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            matrix[i, j] = 0;
        for (int i = 0; i < n; i++)
        for (int j = 0; j < m - n; j++)
            matrix[i, j] = matrix1[i, j];

        for (int i = 0; i < n; i++)
        {
            matrix[i, i + m - n] = matrix[(i+1)%n, i + m - n] = 1;
        }

        List<int> vertices = new List<int>();
        for (int i = 0; i < n; i++)
            vertices.Add(i);
        vertices.Shuffle();
        int col = 0;
        int[,] finalMatrix = new int[n, m];
        foreach (int vertex in vertices)
        {
            for (int e = 0; e < m; e++)
                finalMatrix[col, e] = matrix[vertex, e];
            col++;
        }

        return new Hypergraph()
        {
            N = n,
            M = m,
            Matrix = finalMatrix
        };
    }

    public int[,] GenerateHypercycleMatrix(int n, int m, bool skip2Edges = false)
    {
        int[,] matrix = new int[n, m];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            matrix[i, j] = 0;

        int startEdge = 0;
        List<CircularOnesColumn> possibleEdges = GeneratePossibleColumns(n, skip2Edges);
        List<CircularOnesColumn> chosenEdges = new List<CircularOnesColumn>();
        HashSet<UndirectedEdge>
            unsatisfiedEdges = new HashSet<UndirectedEdge>();

        // if (!_forceCycleSubhypergraph)
        for (int v = 0; v < n; v++)
            unsatisfiedEdges.Add(new UndirectedEdge() { V = v, U = (v + 1) % n });

        // if (_forceCycleSubhypergraph)
        // {
        //     List<CircularOnesColumn> twoEdges = possibleEdges.Where(column => column.Size == 2).ToList();
        //     foreach (CircularOnesColumn edge in twoEdges)
        //     {
        //         for (int v = 0; v < edge.Size; v++)
        //             matrix[(v + edge.StartIndex) % n, startEdge] = 1;
        //         startEdge++;
        //     }
        //
        //     twoEdges.ForEach(e => possibleEdges.Remove(e));
        //     twoEdges.ForEach(e => chosenEdges.Add(e));
        // }


        UndirectedEdge specialEdge = new UndirectedEdge() { U = -1, V = -1 };
        for (int e = startEdge; e < m; e++)
        {
            // if its the first iteration, choose a random column
            List<CircularOnesColumn> overlappingEdges;
            if (m == 0)
            {
                overlappingEdges = possibleEdges;
            }
            else
            {
                overlappingEdges = possibleEdges
                    .Where(edge => chosenEdges.Any(chosen => chosen.Overlaps(edge, n)))
                    .ToList();
            }
            // filter out the columns, so that there are only those left, which overlap with already existing columns are left

            // if verticesInNoEdge().Size != 0, then choose only the columns with such vertices
            if (unsatisfiedEdges.Count != 0 && !unsatisfiedEdges.Contains(specialEdge))
            {
                overlappingEdges = overlappingEdges
                    .Where(edge =>
                        unsatisfiedEdges.Any(ue => edge.ContainsVertex(ue.U, n) && edge.ContainsVertex(ue.V, n)))
                    .ToList();
            }

            // if it is the last iteration and some vertices are left unassigned to any edge somehow, then
            // the last edge should contain every such vertex and must overlap with a random column
            CircularOnesColumn chosenEdge;
            if (e == m - 1 && unsatisfiedEdges.Count != 0 && !unsatisfiedEdges.Contains(specialEdge))
            {
                chosenEdge = possibleEdges
                    .Where(edge =>
                        unsatisfiedEdges.All(uv => edge.ContainsVertex(uv.U, n) && edge.ContainsVertex(uv.V, n)))
                    .First();
            }
            else if (e != 0 && !unsatisfiedEdges.Contains(specialEdge))
            {
                chosenEdge = overlappingEdges[_r.Next(overlappingEdges.Count)];
            }
            else
            {
                chosenEdge = possibleEdges[_r.Next(possibleEdges.Count())];
            }

            for (int v = chosenEdge.StartIndex; v < chosenEdge.EndIndex; v++)
            {
                matrix[v % n, e] = 1;
            }

            possibleEdges.Remove(chosenEdge);
            chosenEdges.Add(chosenEdge);
            for (int v = chosenEdge.StartIndex;
                 v <= chosenEdge.EndIndex;
                 v++) 
            {
                if (v != chosenEdge.EndIndex)
                {
                    UndirectedEdge? undirectedEdge = unsatisfiedEdges
                        .FirstOrDefault(ue => ue.U == v && ue.V == (v + 1) % n || ue.V == v && ue.U == (v + 1) % n);
                    if (undirectedEdge is not null) unsatisfiedEdges.Remove(undirectedEdge);
                }

                matrix[v % n, e] = 1;
            }

            if (unsatisfiedEdges.Count == 0) 
            {
                unsatisfiedEdges.Add(specialEdge);
                possibleEdges.Add(new CircularOnesColumn() { StartIndex = 0, Size = n });
            }
        }

        return matrix;
    }

    private List<CircularOnesColumn> GeneratePossibleColumns(int n, bool skip2Edges)
    {
        List<CircularOnesColumn> possibleEdges = new List<CircularOnesColumn>();
        int edgeSize = skip2Edges ? 3 : 2;
        while (edgeSize < n)
        {
            for (int i = 0; i < n; i++)
            {
                possibleEdges.Add(new CircularOnesColumn()
                {
                    StartIndex = i,
                    Size = edgeSize
                });
            }

            edgeSize++;
        }

        return possibleEdges;
    }
}