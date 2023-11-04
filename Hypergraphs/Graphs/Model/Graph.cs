namespace Hypergraphs.Graphs.Model;

public class Graph
{
    private int[,] _matrix;

    public int[,] Matrix
    {
        get { return _matrix; }
        set { _matrix = value; }
    }

    private int _n;

    public int N
    {
        get { return _n;}
        set { _n = value; }
    }
    
    private int _m;

    public int M
    {
        get { return _m; }
        set { _m = value; }
    }

    public Graph()
    {
    }

    public Graph(int n)
    {
        _n = n;
        _m = 0;
        _matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _matrix[i, j] = 0;
            }
        }
    }

    public void AddVertex()
    {
        if (_matrix.Length == 0 || _matrix.GetLength(0) < _n + 1)
        {
            int[,] matrix = new int[_n + 1, _n + 1];
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    matrix[i, j] = _matrix[i, j];
                }
            }

            for (int i = 0; i < _n; i++)
            {
                matrix[_n, i] = 0;
                matrix[i, _n] = 0;
            }

            _matrix = matrix;
        }

        _n++;
    }

    public void AddEdge(int v1, int v2, int w = 1)
    {
        if (v1 >= 0 && v1 < _n && v2 >= 0 && v2 < _n)
        {
            if (_matrix[v1, v2] == 0)
            {
                _matrix[v1, v2] = w;
                _matrix[v2, v1] = w;
                _m++;
            }
        }
    }

    public bool DeleteVertex(int v)
    {
        if (v >= _n || v < 0)
        {
            return false;
        }

        for (int i = 0; i < _n; i++)
        {
            if (_matrix[v, i] != 0)
            {
                _m--;
            }

            _matrix[i, v] = 0;
            _matrix[v, i] = 0;
        }
        
        for (int i = 0; i < _n; i++)
        {
            for (int j = v + 1; j < _n; j++)
            {
                _matrix[j - 1, i] = _matrix[j, i];
            }
        }
        for (int i = 0; i < _n; i++)
        {
            for (int j = v + 1; j < _n; j++)
            {
                _matrix[i, j - 1] = _matrix[i, j];
            }
        }

        for (int i = 0; i < _n; i++)
        {
            _matrix[_n - 1, i] = 0;
            _matrix[i, _n - 1] = 0;
        }

        _n--;
        return true;
    }

    public bool DeleteEdge(int v1, int v2)
    {
        if (_matrix[v1, v2] == 0)
        {
            return false;
        }

        _matrix[v1, v2] = 0;
        _matrix[v2, v1] = 0;
        _m--;
        return true;
    }
    
}