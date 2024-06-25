using Hypergraphs.Extensions;
using Hypergraphs.Graphs.Generators;
using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class HypertreeGenerator
{
    private static readonly Random _r = new Random();

    public Hypergraph Generate(int n, int m)
    {
        List<int> chosenVertices = new List<int>();
        List<int> verticesInNoEdge = new List<int>();
        for (int v = 0; v < n; v++)
            verticesInNoEdge.Add(v);
        TreeGenerator generator = new TreeGenerator();
        Graph tree = generator.Generate(n);
        List<HashSet<int>> subtrees = new List<HashSet<int>>();//GenerateSubtrees(tree);

        /*
         * reworked:
         * 1. choose a random vertex (if verticesInNoEdge is not empty then select from this set), and a random vertex from already visitred ones
         * 2. do bfs/dfs starting from the vertex, treat it like parent
         * 3. if no more vertices are available to visit OR edge size limit reached, return the edge
         * 4. this way we do not need to generate every single possible subtree
         */

        for (int e = 0; e < m; e++)
        {
            if (e == m - 1 && verticesInNoEdge.Count != 0)
            {
                // find an edge with all the remaining vertices
                HashSet<int> smallestSubtree = FindSmallestSubtree(tree, verticesInNoEdge.ToHashSet());
                foreach (int v in smallestSubtree)
                {
                    if (verticesInNoEdge.Contains(v))
                    {
                        verticesInNoEdge.Remove(v);
                        chosenVertices.Add(v);
                    }
                }
                subtrees.Add(smallestSubtree);
                break;
            }
            int startVertex;
            int endVertex;
            
            HashSet<int> subtree;
            do
            {
                if (verticesInNoEdge.Count != 0)
                    startVertex = verticesInNoEdge[_r.Next(verticesInNoEdge.Count)];
                else
                    startVertex = _r.Next(n);
                if (chosenVertices.Count != 0)
                    endVertex = chosenVertices[_r.Next(chosenVertices.Count)];
                else
                    do
                    {
                        endVertex = _r.Next(n);
                    } while (endVertex == startVertex);
                subtree = RandomDfs(endVertex, startVertex, -1, tree);
            } while (
                subtrees.Any(st => st.SetEquals(subtree)) || subtree.Count < 2
            );

            subtrees.Add(subtree);
            foreach (int v in subtree)
            {
                if (verticesInNoEdge.Contains(v))
                {
                    verticesInNoEdge.Remove(v);
                    chosenVertices.Add(v);
                }
            }
        }

        return HypergraphFactory.FromHyperEdgesList(n, subtrees.Select(st => st.ToList()).ToList());
    }

    private HashSet<int> RandomDfs(int targetVertex, int vertex, int parent, Graph tree)
    {
        HashSet<int> subtree = new HashSet<int>() { vertex };

        // If the current vertex is the target vertex, return the subtree
        if (vertex == targetVertex)
            return subtree;

        // Get the neighbors of the current vertex in random order
        List<int> neighbors = tree.Neighbours(vertex).Where(v => v != parent).ToList();
        neighbors.Shuffle();

        foreach (int neighbor in neighbors)
        {
            HashSet<int> randomSubtree = RandomDfs(targetVertex, neighbor, vertex, tree);
            subtree.UnionWith(randomSubtree);
            if (subtree.Contains(targetVertex)) return subtree;
        }

        return subtree;
    }
    
    private HashSet<int> FindSmallestSubtree(Graph tree, HashSet<int> targetVertices)
    {
        HashSet<int> smallestSubtree = null;
        int minSubtreeSize = int.MaxValue;

        foreach (int vertex in targetVertices)
        {
            HashSet<int> subtree = DFS(vertex, -1, tree, targetVertices);
            if (subtree.Count < minSubtreeSize)
            {
                minSubtreeSize = subtree.Count;
                smallestSubtree = subtree;
            }
        }

        return smallestSubtree;
    }
    
    private HashSet<int> DFS(int vertex, int parent, Graph tree, HashSet<int> targetVertices)
    {
        HashSet<int> subtree = new HashSet<int>() { vertex };

        // If the current vertex is one of the target vertices, remove it from the set
        targetVertices.Remove(vertex);

        // If all target vertices are found, return the subtree
        if (targetVertices.Count == 0)
            return subtree;

        foreach (int neighbor in tree.Neighbours(vertex).Where(v => v != parent))
        {
            HashSet<int> subtreeFromNeighbor = DFS(neighbor, vertex, tree, new HashSet<int>(targetVertices));
            subtree.UnionWith(subtreeFromNeighbor);
        }

        return subtree;
    }
    
}