using Hypergraphs.Extensions;
using Hypergraphs.Generators.Model;
using Hypergraphs.Model;
using MathNet.Numerics.Random;

namespace Hypergraphs.Generators;

public class HyperpathGenerator
{
    private static Random _r = new Random();

    public HyperpathGenerator()
    {
    }

    public Hypergraph Generate(int n, int m)
    {
        int[,] matrix = GenerateConsecutiveOnesMatrix(n, m);

        List<int> vertices = new List<int>();
        for (int i = 0; i < n; i++)
            vertices.Add(i);
        vertices.Shuffle();
        int row = 0;
        int[,] finalMatrix = new int[n, m];
        foreach (int vertex in vertices)
        {
            for (int e = 0; e < m; e++)
                finalMatrix[row, e] = matrix[vertex, e];
            row++;
        }
        return new Hypergraph()
        {
            N = n,
            M = m,
            Matrix = finalMatrix
        };
    }

    public int[,] GenerateConsecutiveOnesMatrix(int n, int m)
    {
        int[,] matrix = new int[n, m];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            matrix[i, j] = 0;

        List<ConsecutiveOnesColumn> possibleEdges = GeneratePossibleColumns(n);
        List<ConsecutiveOnesColumn> chosenEdges = new List<ConsecutiveOnesColumn>();

        HashSet<int> verticesInNoEdge = new HashSet<int>();
        for (int v = 0; v < n; v++)
            verticesInNoEdge.Add(v);

        for (int e = 0; e < m; e++)
        {
            // if its the first iteration, choose a random column
            List<ConsecutiveOnesColumn> overlappingEdges;
            if (m == 0)
                overlappingEdges = possibleEdges;
            else
                overlappingEdges = possibleEdges
                    .Where(edge => chosenEdges.Any(chosen => chosen.Overlaps(edge)))
                    .ToList();
            // filter out the columns, so that there are only those left, which overlap with already existing columns are left

            // if verticesInNoEdge().Size != 0, then choose only the columns with such vertices
            if (verticesInNoEdge.Count != 0)
                overlappingEdges = overlappingEdges
                    .Where(edge => verticesInNoEdge.Any(edge.ContainsVertex))
                    .ToList();

            // if it is the last iteration and some vertices are left unassigned to any edge somehow, then
            // the last edge should contain every such vertex and must overlap with a random column
            ConsecutiveOnesColumn chosenEdge;
            if (e == m - 1 && verticesInNoEdge.Count != 0)
            {
                int maxVertex = verticesInNoEdge.Max();
                int minVertex = verticesInNoEdge.Min();
                chosenEdge = possibleEdges
                    .Where(edge => edge.ContainsVertex(minVertex) && edge.ContainsVertex(maxVertex))
                    .First();
            }
            else if (e != 0)
            {
                chosenEdge = overlappingEdges[_r.Next(overlappingEdges.Count)];
            }
            else
            {
                chosenEdge = possibleEdges[_r.Next(overlappingEdges.Count())];
            }

            for (int v = chosenEdge.StartIndex; v < chosenEdge.EndIndex; v++)
                matrix[v, e] = 1;

            possibleEdges.Remove(chosenEdge);
            chosenEdges.Add(chosenEdge);
            for (int v = chosenEdge.StartIndex; v <= chosenEdge.EndIndex; v++)
            {
                if (verticesInNoEdge.Contains(v)) verticesInNoEdge.Remove(v);
                matrix[v, e] = 1;
            }
        }

        return matrix;
    }

    private List<ConsecutiveOnesColumn> GeneratePossibleColumns(int n)
    {
        List<ConsecutiveOnesColumn> possibleEdges = new List<ConsecutiveOnesColumn>();
        int edgeSize = 2;
        while (edgeSize <= n)
        {
            for (int i = 0; i <= n - edgeSize; i++)
            {
                possibleEdges.Add(new ConsecutiveOnesColumn()
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