using System.Text;

namespace Hypergraphs.Model;

public class Hypergraph
{
    private int _n;

    public int N
    {
        get { return _n; }
        set { _n = value; }
    }

    private int _m;

    public int M
    {
        get { return _m; }
        set { _m = value; }
    }

    private int[,] _matrix;

    public int[,] Matrix
    {
        get { return _matrix; }
        set { _matrix = value; }
    }

    public Hypergraph()
    {
    }

    public Hypergraph(Hypergraph other)
    {
        _n = other._n;
        _m = other._m;
        _matrix = new int[_n, _m];
        for (int v = 0; v < _n; v++)
        {
            for (int e = 0; e < _m; e++)
            {
                _matrix[v, e] = other._matrix[v, e];
            }
        }
    }

    public Hypergraph(int n, int m)
    {
        _n = n;
        _m = m;
        _matrix = new int[n, m];
        for (int v = 0; v < n; v++)
        {
            for (int e = 0; e < m; e++)
            {
                _matrix[v, e] = 0;
            }
        }
    }

    public bool VertexInEdge(int vertex, int edge)
    {
        if (vertex < 0 || vertex >= _n)
        {
            return false;
        }

        if (edge < 0 || edge >= _m)
        {
            return false;
        }

        return _matrix[vertex, edge] != 0;
    }

    public int VertexDegree(int v)
    {
        int degree = 0;
        for (int i = 0; i < _m; i++)
        {
            degree += _matrix[v, i];
        }

        return degree;
    }

    public int EdgeCardinality(int e)
    {
        int size = 0;
        for (int i = 0; i < _n; i++)
        {
            size += _matrix[i, e];
        }

        return size;
    }

    // vertex and edge adding and deleting

    public void AddVertex()
    {
        if (_matrix.Length == 0 || _matrix.GetLength(0) < _n + 1)
        {
            int[,] matrix = new int[_n + 1, _m];
            for (int v = 0; v < _n; v++)
            {
                for (int e = 0; e < _m; e++)
                {
                    matrix[v, e] = _matrix[v, e];
                }
            }

            for (int e = 0; e < _m; e++)
            {
                matrix[_n, e] = 0;
            }

            _matrix = matrix;
        }

        _n++;
    }

    public void AddVertexToEdge(int v, int e)
    {
        _matrix[v, e] = 1;
    }

    public bool WeakDeleteVertex(int v)
    {
        if (v >= _n || v < 0)
        {
            return false;
        }

        for (int e = 0; e < _m; e++)
        {
            for (int i = v + 1; i < _n; i++)
            {
                _matrix[i - 1, e] = _matrix[i, e];
            }
        }

        for (int i = 0; i < _m; i++)
        {
            _matrix[_n - 1, i] = 0;
        }

        _n--;
        return true;
    }

    public bool StrongDeleteVertex(int v)
    {
        if (v >= _n)
        {
            return false;
        }

        List<int> edgesToDelete = new List<int>();
        // we would like to remove the final edge first and the firs one last
        for (int e = _m - 1; e >= 0; e--)
        {
            if (_matrix[v, e] != 0)
            {
                edgesToDelete.Add(e);
            }
        }

        foreach (int e in edgesToDelete)
        {
            if (!WeakDeleteEdge(e))
            {
                return false;
            }
        }

        return WeakDeleteVertex(v);
    }

    public void AddEdge()
    {
        if (_matrix.Length == 0 || _matrix.GetLength(1) < _m + 1)
        {
            int[,] matrix = new int[_n, _m + 1];
            for (int v = 0; v < _n; v++)
            {
                for (int e = 0; e < _m; e++)
                {
                    matrix[v, e] = _matrix[v, e];
                }
            }

            for (int v = 0; v < _n; v++)
            {
                matrix[v, _m] = 0;
            }

            _matrix = matrix;
        }

        _m++;
    }

    public bool WeakDeleteEdge(int e)
    {
        if (e >= _m || e < 0)
        {
            return false;
        }


        for (int v = 0; v < _n; v++)
        {
            for (int i = e + 1; i < _m; i++)
            {
                _matrix[v, i - 1] = _matrix[v, i];
            }
        }

        for (int i = 0; i < _n; i++)
        {
            _matrix[i, _m - 1] = 0;
        }

        _m--;

        return true;
    }

    public bool StrongDeleteEdge(int e)
    {
        if (e >= _m)
        {
            return false;
        }

        List<int> verticesToDelete = new List<int>();
        for (int v = _n - 1; v >= 0; v--)
        {
            if (_matrix[v, e] != 0)
            {
                verticesToDelete.Add(v);
            }
        }

        foreach (int v in verticesToDelete)
        {
            if (!WeakDeleteVertex(v))
            {
                return false;
            }
        }

        return WeakDeleteEdge(e);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _n; i++)
        {
            for (int j = 0; j < _m; j++)
            {
                sb.Append($"{_matrix[i,j]}; ");
            }

            sb.Append("\n");
        }

        return sb.ToString();
    }

    public bool EdgesIntersect(int e1, int e2)
    {
        for (int i = 0; i < _n; i++)
            if (_matrix[i, e1] > 0 && _matrix[i, e2] > 0)
                return true;
        return false;
    }

    public List<int> EdgeIntersection(int e1, int e2)
    {
        List<int> intersection = new List<int>();
        for (int v = 0; v < _n; v++)
            if (_matrix[v, e1] > 0 && _matrix[v, e2] > 0)
                intersection.Add(v);
        return intersection;
    }

    public List<int> EdgeIntersection(List<int> vertices, int e)
    {
        List<int> intersection = new List<int>();
        foreach (int v in vertices)
            if (_matrix[v, e] > 0)
                intersection.Add(v);
        return intersection;
    }

    public List<int> GetEdgeVertices(int e)
    {
        List<int> vertices = new List<int>();
        for (int v = 0; v < _n; v++)
            if (_matrix[v, e] > 0)
                vertices.Add(v);
        return vertices;
    }
    
    public List<int> GetVertexEdges(int v)
    {
        List<int> edges = new List<int>();
        for (int e = 0; e < _m; e++)
            if (_matrix[v, e] > 0)
                edges.Add(e);
        return edges;
    }
    
}