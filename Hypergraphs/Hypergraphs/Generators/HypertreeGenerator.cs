using Hypergraphs.Graphs.Model;
using Hypergraphs.Model;

namespace Hypergraphs.Generators;

public class HypertreeGenerator
{
    private static readonly Random _r = new Random();
    // todo: do not support m?
    public Hypergraph Generate(int n, int m, int bound = -1)
    {
        bool[] selected = new bool[n];
        for (int i = 0; i < n; i++)
            selected[i] = false;

        if (bound == -1)
            bound = n;

        if (bound > n)
        {
            throw new ArgumentException($"Maximum edge cardinality should not be greater than n, but was {bound} and n was {n}.", "bound");
        }
        
        // 1. wygeneruj drzewo
        Graph t = ...;
// todo: bound to maks wierzcholkow w krawedzi
        while (selected.Contains(false))
        {
            // 2. wybierz losowo wierzcholek
            
            
            // 3. wybierz losowo liczbe od 1 do bounded (tyle wierzcholkow bedzie w hiperkrawedzi)

            
            // mozemy wybierac sasiadow i zostawiac 1 z nich na 100% a kazdego kolejnego z coraz mniejszym prawdopodobienstwem
            
        }

        // 4. wyznacz spojny podgraf poczawszy od tego wierzcholka, ktory bedzie mial dlugosc m
        // (random walk?)
        // 5. podgraf staje sie krawedzia
        // 6. powtarzaj 1-5 dopoki istnieja wierzcholki izolowane
        return null;
    }  
    
}