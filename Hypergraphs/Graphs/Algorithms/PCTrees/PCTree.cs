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

    public static PCTree TestInstance(List<PCNode> leaves, int[,] matrix, int rows, int columns)
    {
        return new PCTree()
        {
            _leaves = leaves,
            _matrix = matrix,
            _rows =  rows,
            _columns = columns
        };
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

    public bool Construct()
    {
        InitTree();

        TerminalPathFinder finder = new TerminalPathFinder(this);

        for (int i = 0; i < _rows; i++)
        {
            finder.LabelNodes();
            List<PCNode>? terminalPath = finder.FindTerminalPath();
            if (terminalPath == null)
                return false;
            if(!TerminalPathRearrangementUtils.RearrangePath(terminalPath))
                return false;
            
            TerminalPathRearrangementUtils.SplitAndMergePathV2(terminalPath);
            finder.ClearNodeLabels();
            _currentRow++;
        }

        _currentRow--;
        
        return true;
    }

    private void Dfs(PCNode node, List<int> columnOrder, HashSet<PCNode> visited)
    {
        if (node.Type == NodeType.P)
        {
            TerminalPathRearrangementUtils.OrderPNode(node, node.Neighbours.FirstOrDefault(n => n.Type is NodeType.C or NodeType.P), null);
        }

        if (node.Type == NodeType.C)
        {
            TerminalPathRearrangementUtils.OrderCNode(node, node.Neighbours.FirstOrDefault(n => n.Type is NodeType.C or NodeType.P), null);
        }
        visited.Add(node);
        if (node.Type == NodeType.Leaf)
            columnOrder.Add((int)node.Column!);
        foreach (PCNode neighbour in node.Neighbours.Where(n => !visited.Contains(n)))
            Dfs(neighbour, columnOrder, visited);
    }

    private void TraverseNode(PCNode node, List<PCNode> terminalPath, int i, List<int> columnOrder, HashSet<PCNode> visited)
    {
        visited.Add(node);
        
        if (terminalPath.Count > 1)
        {
            if (i == 0)
            {
                node.Neighbours.RotateLeft(node.Neighbours.IndexOf(terminalPath[1])+1);
                foreach (PCNode neighbour in node.Neighbours)
                {
                    if (neighbour == terminalPath[1]) break;
                    Dfs(neighbour, columnOrder, visited);
                }
                TraverseNode(terminalPath[1], terminalPath, 1, columnOrder, visited);
            }
            else if (i == terminalPath.Count - 1)
            {
                node.Neighbours.RotateLeft(node.Neighbours.IndexOf(terminalPath[i-1])+1);
                foreach (PCNode neighbour in node.Neighbours)
                {
                    if (neighbour == terminalPath[i-1]) break;
                    Dfs(neighbour, columnOrder, visited);
                }
            }
            else
            {
                foreach (PCNode neighbour in node.Neighbours)
                {
                    if (neighbour == terminalPath[i+1])
                        TraverseNode(neighbour, terminalPath, i+1, columnOrder, visited);
                    else if (neighbour != terminalPath[i-1])
                        Dfs(neighbour, columnOrder, visited);
                }
                
            }
        }
        else if (terminalPath.Count == 1)
        {
            foreach (PCNode neighbour in node.Neighbours)
            {
                Dfs(neighbour, columnOrder, visited);
            }
        }
    }

    public int[]? GetPermutation()
    {
        HashSet<PCNode> visited = new HashSet<PCNode>();
        List<int> columnOrder = new List<int>();

        TerminalPathFinder finder = new TerminalPathFinder(this);
        finder.LabelNodes();
        List<PCNode>? terminalPath = finder.FindTerminalPath();
        if (terminalPath == null)
            return null;

        TerminalPathRearrangementUtils.RearrangePath(terminalPath);
        TraverseNode(terminalPath[0], terminalPath, 0, columnOrder, visited);

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
            _leaves.Add(leaf);
            center.AppendNeighbour(leaf);
        }
    }

    public int GetValueInCurrentRow(int column)
    {
        return _matrix[_currentRow, column];
    }
}