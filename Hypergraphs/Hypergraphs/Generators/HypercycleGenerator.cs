using Hypergraphs.Extensions;
using Hypergraphs.Generators.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class HypercycleGenerator
{
    private static Random _r = new Random();

    public HypercycleGenerator()
    {
    }

    // todo: wygemerowac z n-krawedziami
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
            for (int e = 0; e < m; e++)
                finalMatrix[col, e] = matrix[vertex, e];

        return new Hypergraph()
        {
            N = n,
            M = m,
            Matrix = finalMatrix
        };
    }

// todo: rember about handling n-edges case!
    public int[,] GenerateHypercycleMatrix(int n, int m)
    {
        int[,] matrix = new int[n, m];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            matrix[i, j] = 0;

        List<CircularOnesColumn> possibleEdges = GeneratePossibleColumns(n);
        List<CircularOnesColumn> chosenEdges = new List<CircularOnesColumn>();

        HashSet<UndirectedEdge> unsatisfiedEdges = new HashSet<UndirectedEdge>();//todo: filter based on this instead of just vertices
        for (int v = 0; v < n; v++)
        {
            unsatisfiedEdges.Add(new UndirectedEdge() { V = v, U = (v + 1) % n });
        }

        UndirectedEdge specialEdge = new UndirectedEdge() { U = -1, V = -1 };
        for (int e = 0; e < m; e++)
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
                    .Where(edge => unsatisfiedEdges.Any(ue => edge.ContainsVertex(ue.U,n) && edge.ContainsVertex(ue.V,n)))
                    .ToList();
            }

            // if it is the last iteration and some vertices are left unassigned to any edge somehow, then
            // the last edge should contain every such vertex and must overlap with a random column
            CircularOnesColumn chosenEdge;
            if (e == m - 1 && unsatisfiedEdges.Count != 0 && !unsatisfiedEdges.Contains(specialEdge))//todo
            {
                chosenEdge = possibleEdges
                    .Where(edge => unsatisfiedEdges.All(uv => edge.ContainsVertex(uv.U, n) && edge.ContainsVertex(uv.V, n)))
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
            // todo: analogicznie dla hipergwiazd moge zrobic :)

            for (int v = chosenEdge.StartIndex; v < chosenEdge.EndIndex; v++)
            {
                matrix[v % n, e] = 1;
            }

            possibleEdges.Remove(chosenEdge);
            chosenEdges.Add(chosenEdge);
            for (int v = chosenEdge.StartIndex; v <= chosenEdge.EndIndex; v++) //todo: moze byc jakis fuckup z tymi indeksami 0_0
            {
                if (v != chosenEdge.EndIndex)
                {
                    UndirectedEdge? undirectedEdge = unsatisfiedEdges
                        .FirstOrDefault(ue => ue.U == v && ue.V == (v + 1) % n || ue.V == v && ue.U == (v + 1) % n);
                    if (undirectedEdge is not null) unsatisfiedEdges.Remove(undirectedEdge);
                }
                matrix[v % n, e] = 1;
            }

            if (unsatisfiedEdges.Count == 0)// TODO: KURWA JEGO MAC NIE USUWAJA SIE UNSATISFIED EDGES WGL.................
            {
                unsatisfiedEdges.Add(specialEdge);
                possibleEdges.Add(new CircularOnesColumn() { StartIndex = 0, Size = n });
            }
        }

        return matrix;
    }

    private List<CircularOnesColumn> GeneratePossibleColumns(int n)
    {
        List<CircularOnesColumn> possibleEdges = new List<CircularOnesColumn>();
        int edgeSize = 2;
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