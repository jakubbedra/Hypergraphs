using Hypergraphs.Extensions;

namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class PCTree
{
    private List<PCNode> _leaves;

    public List<PCNode> Leaves
    {
        get { return _leaves; }
    }

    private int _rows;
    private int _columns;

    private int[,] _matrix;
    private int _currentRow;

    public static PCTree TestInstance(List<PCNode> leaves)
    {
        return new PCTree() { _leaves = leaves };
    }

    private PCTree()
    {
    }

    public PCTree(int[,] matrix, int rows, int columns, bool transpose = false)
    {
        _leaves = new List<PCNode>();
        if (transpose)
        {
            _rows = columns;
            _columns = rows;
            _matrix = new int[columns, rows];
            for (int i = 0; i < _rows; i++)
            for (int j = 0; j < _columns; j++)
                _matrix[i, j] = matrix[j, i];
        }
        else
        {
            _rows = rows;
            _columns = columns;
            _matrix = new int[rows, columns];
            for (int i = 0; i < _rows; i++)
            for (int j = 0; j < _columns; j++)
                _matrix[i, j] = matrix[i, j];
        }

        _currentRow = 0;
    }

    public void Construct()
    {
        InitTree();
        for (int i = 1; i < _rows; i++)
        {
        }
    }

    private void Dfs(PCNode node, List<int> columnOrder, HashSet<PCNode> visited)
    {
        visited.Add(node);
        if (node.Type == NodeType.Leaf)
            columnOrder.Add((int)node.Column!);
        foreach (PCNode neighbour in node.Neighbours.Where(n => !visited.Contains(n)))
            Dfs(neighbour, columnOrder, visited);
    }

    private void TraverseNode(PCNode currentNode, List<PCNode> terminalPath)
    {
        // todo: traverse single node : top, right (recursive), bottom
    }
    
    public int[]? GetPermutation()
    {
        // todo
        HashSet<PCNode> visited = new HashSet<PCNode>();
        List<int> columnOrder = new List<int>();
        int currentSlot = 0;

        TerminalPathFinder finder = new TerminalPathFinder(this);
        finder.LabelNodes(); // todo: Implement cleaning node labels
        List<PCNode>? terminalPath = finder.FindTerminalPath();
        if (terminalPath == null)
            return null;

        PCNode? previous = null;
        for (var i = 0; i < terminalPath.Count; i++)
        {
            // todo: make sure the i-1th node is first and i+1th node is second
            // todo: ej wsm, to moge przerotwowac p1 na pozycje 0 i po prostu zrobic dfs'a na tym nodzie
            List<PCNode> neighbours = terminalPath[0].Neighbours;
            if (terminalPath.Count != 1)
            {
                if (i == 0)
                {
                    // if previous is null
                    neighbours.RotateLeft(neighbours.IndexOf(terminalPath[i+1])+1);
                }
                else if (i == terminalPath.Count - 1)
                {
                    // if next is null
                    neighbours.RotateLeft(neighbours.IndexOf(terminalPath[i-1]));
                }
                else
                {
                    
                }
            }
            // get upper
            // go next
            // get lower
            
        }
        
        // while there are still unvisited leaves
        // foreach (PCNode leaf in _leaves)
        // {
            // if (!visited.Contains(leaf))
            // {
                // Dfs(leaf, columnOrder, visited);
            // }
        // }
        
        return columnOrder.ToArray();
    }

    // creates initial tree with 1 P-Node
    private void InitTree()
    {
        PCNode center = new PCNode() { Type = NodeType.P };
        // add leaves
        for (int i = 0; i < _columns; i++)
        {
            PCNode leaf = new PCNode()
            {
                Type = NodeType.Leaf,
                Column = i,
                Parent = center,
                Neighbours = new List<PCNode>() { center }
            };
            center.AppendNeighbour(leaf);
        }
    }

    public int GetValueInCurrentRow(int column) // todo might have circular dependency -> eliminate it
    {
        return _matrix[_currentRow, column];
    }
}