using Hypergraphs.Graphs.Model;

namespace Hypergraphs.Graphs.Algorithms;

public class ConsecutiveOnes
{
    private int[,] _matrix;
    private int _n;
    private int _m;

    private int[] _columnLengths;
    private int[,] _columnDotProducts;

    public ConsecutiveOnes(int[,] matrix, int n, int m)
    {
        if (m < 3) throw new ArgumentOutOfRangeException();
        _matrix = matrix;
        _n = n;
        _m = m;
        _columnLengths = new int[m];
        _columnDotProducts = new int [m, m];
        for (int i = 0; i < m; i++)
        {
            _columnLengths[i] = -1;
            for (int j = 0; j < m; j++)
            {
                _columnDotProducts[j, i] = -1;
            }
        }
    }

    public void RemoveDuplicateColumns()
    {
        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);

        // Use HashSet to track unique columns
        HashSet<string> uniqueColumns = new HashSet<string>();

        // List to store unique columns as arrays
        List<int[]> uniqueColumnList = new List<int[]>();

        // Check each column for uniqueness
        for (int col = 0; col < cols; col++)
        {
            // Extract the column as an array
            int[] currentColumn = new int[rows];
            for (int row = 0; row < rows; row++)
            {
                currentColumn[row] = _matrix[row, col];
            }

            // Convert the column array to a string for HashSet comparison
            string columnString = string.Join(",", currentColumn);

            // If the column is unique, add it to the HashSet and the list
            if (uniqueColumns.Add(columnString))
            {
                uniqueColumnList.Add(currentColumn);
            }
        }

        // Create a new matrix from the unique columns
        int[,] uniqueMatrix = new int[rows, uniqueColumnList.Count];
        for (int col = 0; col < uniqueColumnList.Count; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                uniqueMatrix[row, col] = uniqueColumnList[col][row];
            }
        }

        _m = uniqueColumnList.Count;
        _matrix = uniqueMatrix;
    }

    public int[]? GetPermutation()
    {
        RemoveDuplicateColumns();

        Graph overlapGraph = new Graph(_m); // vertices representing columns

        for (int i = 0; i < _m; i++)
        for (int j = i + 1; j < _m; j++)
            if (ColumnsOverlap(i, j))
                overlapGraph.AddEdge(i, j);

        List<HashSet<int>> connectedComponents = new List<HashSet<int>>();
        bool[] visitedVertices = new bool[_m];
        for (int i = 0; i < _m; i++)
            visitedVertices[i] = false;

        List<Column?[]> submatrices = new List<Column?[]>();

        BreadthFirstSearch bfs = new BreadthFirstSearch();
        while (visitedVertices.Contains(false))
        {
            int startVertex = Array.FindIndex(visitedVertices, v => v == false);
            List<int> bfsOrder = bfs.GetOrder(overlapGraph, startVertex);
            connectedComponents.Add(bfsOrder.ToHashSet());
            bfsOrder.ForEach(v => visitedVertices[v] = true);
            Column?[]? permutedRows = GetPermutedRows(bfsOrder, overlapGraph);
            if (permutedRows == null) return null;
            submatrices.Add(permutedRows!);
        }

        int[,] containmentDigraph = new int[_m, _m];
        for (int i = 0; i < _m; i++)
        for (int j = 0; j < _m; j++)
            if (ColumnAContainsB(i, j))
                containmentDigraph[i, j] = 1;

        int connectedComponentsCount = connectedComponents.Count;
        int[,] componentDigraph = new int[connectedComponentsCount, connectedComponentsCount];

        for (var i = 0; i < connectedComponentsCount; i++)
        for (int j = 0; j < connectedComponentsCount; j++)
            if (connectedComponents[i].Any(u => connectedComponents[j].Any(v => ColumnAContainsB(u, v))))
                componentDigraph[i, j] = 1;
            else
                componentDigraph[i, j] = 0;

        HashSet<int> unvisitedComponents = new HashSet<int>();
        for (var i = 0; i < connectedComponentsCount; i++)
            unvisitedComponents.Add(i);
        List<int> visitedColumns = new List<int>();
        Column?[] finalMatrix = new Column?[_m];
        for (var i = 0; i < finalMatrix.Length; i++)
            finalMatrix[i] = null;
        while (unvisitedComponents.Count != 0)
        {
            int currentComponent =
                unvisitedComponents.MinBy(c => GetInEdgesCount(componentDigraph, c, unvisitedComponents));

            Column?[] submatrix = submatrices[currentComponent];
            HashSet<int> currentColumns = connectedComponents[currentComponent];
          
            int beginningIndex = CalculateBeginningIndex(visitedColumns, currentColumns, containmentDigraph, finalMatrix);

            if (beginningIndex == -1) return null;
            
            foreach (int column in currentColumns)
            {
                // reposition the 1s
                submatrix[column]!.StartIndex += beginningIndex;
                // merge
                finalMatrix[column] = submatrix[column];
            }

            foreach (int currentColumn in currentColumns)
                visitedColumns.Add(currentColumn);
            unvisitedComponents.Remove(currentComponent);
        }
        // compare the matrix rows with the og (trimmed) matrix

        int[] rowsPermutation = new int[_n];
        for (var i = 0; i < _n; i++)
            rowsPermutation[i] = -1;
        for (int i = 0; i < _n; i++)
        {
            for (int j = 0; j < _n; j++)
            {
                bool rowsEqual = true;
                for (int k = 0; k < _m && rowsEqual; k++)
                    if (_matrix[i, k] != ColumnValueAtIndex(finalMatrix[k]!, j))
                        rowsEqual = false;
                if (rowsEqual && !rowsPermutation.Contains(i) && rowsPermutation[j] == -1)
                {
                    rowsPermutation[j] = i;
                    break;
                }
            }
        }

        return rowsPermutation.Contains(-1) ? null : rowsPermutation;
    }

    private int CalculateBeginningIndex(List<int> visitedColumns, HashSet<int> currentColumns, int[,] containmentDigraph, Column?[] finalMatrix)
    {
        for (int i = 0; i < _n; i++)
        {
            bool correctRow = true;
            foreach (int column in visitedColumns)
            {
                if (currentColumns.Any(c => containmentDigraph[column, c] != ColumnValueAtIndex(finalMatrix[column]!, i)))
                {
                    correctRow = false;
                    break;
                }
            }
            if (correctRow) return i;
        }

        return -1;
    }

    private int ColumnValueAtIndex(Column column, int index)
    {
        if (index >= column.StartIndex && index < column.StartIndex + column.Length)
            return 1;
        return 0;
    }

    private int GetInEdgesCount(int[,] d, int v, HashSet<int> verticesToCheck)
    {
        return verticesToCheck.Count(u => d[u, v] != 0);
    }

    // some columns might be null
    private Column?[]? GetPermutedRows(List<int> bfsOrder, Graph overlapGraph)
    {
        Column?[] columns = new Column?[_m];
        for (int i = 0; i < _m; i++)
            columns[i] = null;
        // handle the case for matrix of length 1 and 2

        int a = bfsOrder[0];

        if (bfsOrder.Count == 1)
        {
            columns[bfsOrder[0]] = new Column(0, GetOrFillLenght(bfsOrder[0]));
            return columns;
        }

        int b = bfsOrder[1];

        if (bfsOrder.Count == 2)
        {
            columns[a] = new Column(0, GetOrFillLenght(bfsOrder[0]));
            columns[b] = new Column(columns[a]!.StartIndex + columns[a]!.Length - GetOrFillDotProduct(a, b),
                GetOrFillLenght(b));

            if (columns[b]!.StartIndex + columns[b]!.Length > _n) // max depth exceeded - algorithm terminated
                return null;

            return columns;
        }


        // place the first column
        int c = bfsOrder[2];

        // we check overlapping by checking if edge exists
        if (!overlapGraph.EdgeExists(b, c))
            (a, b) = (b, a);

        int sizeA = GetOrFillLenght(a);
        int sizeB = GetOrFillLenght(b);

        // we determine if b or c will be on bottom

        // check if all the 1s in the matrix need to be moved one deeper
        // if the depth would exceed the maximum allowed (_n), return null

        columns[a] = new Column(0, sizeA);
        int ab = GetOrFillDotProduct(a, b);
        columns[b] = new Column(columns[a]!.StartIndex + columns[a]!.Length - ab, sizeB);

        if (columns[b]!.StartIndex + columns[b]!.Length > _n) // max depth exceeded - algorithm terminated
            return null;

        Column? newColumn = AddNewColumnOrNull(a, b, c, columns);
        if (newColumn == null) return null;

        columns[c] = newColumn;

        HashSet<int> checkedColumns = new HashSet<int> { a, b, c };

        for (int i = 3; i < bfsOrder.Count; i++)
        {
            c = bfsOrder[i];
            // find a,b such that c overlaps b and b overlaps c 
            b = overlapGraph.Neighbours(c).First(u => checkedColumns.Contains(u));
            a = overlapGraph.Neighbours(b).First(u => checkedColumns.Contains(u) && u != b);

            newColumn = AddNewColumnOrNull(a, b, c, columns);
            if (newColumn == null || !checkedColumns.All(u => ColumnDotProduct(columns[u]!, newColumn) == GetOrFillDotProduct(u, c))) 
                return null;
            columns[c] = newColumn;
            checkedColumns.Add(c);
        }

        return columns;
    }

    private Column? AddNewColumnOrNull(int a, int b, int c, Column?[] existingColumns)
    {
        int ac = GetOrFillDotProduct(a, c);
        int bc = GetOrFillDotProduct(b, c);
        int sizeC = GetOrFillLenght(c);

        Column columnC = new Column(existingColumns[b]!.StartIndex + existingColumns[b]!.Length - bc, sizeC);

        // check if in the new matrix ac = old ac
        int acNew = ColumnDotProduct(existingColumns[a]!, columnC);

        if (ac != acNew)
        {
            columnC = new Column(existingColumns[b]!.StartIndex + bc - sizeC, sizeC);
        }

        acNew = ColumnDotProduct(existingColumns[a]!, columnC);

        if (ac != acNew) return null;

        if (columnC.StartIndex < 0)
        {
            int q = -columnC.StartIndex;
            columnC.StartIndex = 0;
            for (int i = 0; i < _m; i++)
            {
                if (existingColumns[i] != null)
                {
                    existingColumns[i]!.StartIndex += q;
                }
            }
        }

        // check if depth not exceeded (for all tuples)
        for (int i = 0; i < _m; i++)
            if (existingColumns[i] != null && existingColumns[i]!.StartIndex + existingColumns[i]!.Length > _n)
                return null;

        return columnC;
    }

    private bool ColumnsOverlap(int a, int b)
    {
        int dotProduct = GetOrFillDotProduct(a, b);
        return 0 < dotProduct && dotProduct < Math.Min(GetOrFillLenght(a), GetOrFillLenght(b));
    }

    private bool ColumnAContainsB(int a, int b)
    {
        return GetOrFillDotProduct(a, b) == GetOrFillLenght(b);
    }

    private bool ColumnsDisjoint(int a, int b)
    {
        return GetOrFillDotProduct(a, b) == 0;
    }

    private int GetOrFillLenght(int a)
    {
        if (_columnLengths[a] == -1)
        {
            int length = 0;
            for (int i = 0; i < _n; i++)
            {
                length += _matrix[i, a];
            }

            _columnLengths[a] = length;
        }

        return _columnLengths[a];
    }

    private int GetOrFillDotProduct(int a, int b)
    {
        if (_columnDotProducts[a, b] == -1)
        {
            int dotProduct = 0;
            for (int i = 0; i < _n; i++)
            {
                dotProduct += _matrix[i, a] * _matrix[i, b];
            }

            _columnDotProducts[a, b] = dotProduct;
            _columnDotProducts[b, a] = dotProduct;
        }

        return _columnDotProducts[a, b];
    }

    private int ColumnDotProduct(Column a, Column b)
    {
        return Math.Max(Math.Min(a.StartIndex + a.Length, b.StartIndex + b.Length) - 
                        Math.Max(a.StartIndex, b.StartIndex), 0);
    }
    
    private class Column
    {
        public int StartIndex { get; set; }
        public int Length { get; }

        public Column(int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
        }
    }
}