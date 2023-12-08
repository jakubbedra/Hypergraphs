namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class PCTree
{
    private List<PCNode> _leaves;
    private int _rows;
    private int _columns;

    private int[,] _matrix;

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
    }
    
    public void Construct()
    {
        InitTree();
        for (int i = 1; i < _rows; i++)
        {
            
        }
    }

    public int[] GetPermutation()
    {
        throw new NotImplementedException();
    }
    
    // creates initial tree with 1 P-Node
    private void InitTree()
    {
        PCNode center = new PCNode() { Type = NodeType.P };
        PCEdge? first = null;
        PCEdge? last = null;
        
        for (int i = 0; i < _columns; i++)
        {
            PCNode leaf = new PCNode()
            {
                Type = NodeType.Leaf,
                Column = i,
            };
            PCEdge edge = new PCEdge()
            {
                NodeA = center,
                NodeB = leaf,
                IsParentA = true,
            };
            if (first == null)
            {
                first = edge;
                last = edge;
                edge.Left = edge;
                edge.Right = edge;
            }
            
            if (_matrix[0, i] == 1)
            {
                edge.Right = last!.Right;
                edge.Left = last;
                last.Right = edge;
                edge.Right.Left = edge;
                last = edge;
            }
            else
            {
                // add on bottom (append left)
                // todo: extract to prepend/append
                edge.Right = first;
                edge.Left = first.Left;
                first.Left = edge;
                edge.Left.Right = edge;
            }
        }
    }
    
}