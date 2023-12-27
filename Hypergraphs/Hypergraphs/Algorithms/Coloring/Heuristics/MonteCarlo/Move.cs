namespace Hypergraphs.Algorithms;

public class Move
{
    public int Vertex { get; set; }
    public int Color { get; set; }

    public Move(int vertex, int color)
    {
        Vertex = vertex;
        Color = color;
    }
    
}