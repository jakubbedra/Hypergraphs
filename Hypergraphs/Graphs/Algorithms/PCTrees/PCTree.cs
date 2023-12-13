namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class PCTree
{
    private List<PCNode> _leaves;
    public List<PCNode> Leaves
    {
        get { return _leaves;  }
    }
  
    private int _rows;
    private int _columns;

    private int[,] _matrix;
    private int _currentRow;

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

    public int[]? GetPermutation()
    {
        return null;
    }
    
    // creates initial tree with 1 P-Node
    private void InitTree()
    {
        PCNode center = new PCNode() { Type = NodeType.P };
        // add leaves
    }

    public void LabelNodes()
    {
        
    }

    public int GetValueInCurrentRow(int column)
    {
        return _matrix[_currentRow, column];
    }

}