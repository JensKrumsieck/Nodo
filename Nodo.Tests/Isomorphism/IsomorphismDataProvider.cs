namespace Nodo.Tests.Isomorphism;

public static class IsomorphismDataProvider
{
    public static IGraph<int, Edge<int>> ProvideG1()
    {
        var g1 = new UndirectedGraph<int, Edge<int>>();
        g1.Vertices.AddRange(new []{1,2,3,5});
        g1.Edges.Add((1, 2));
        g1.Edges.Add((1, 3));
        g1.Edges.Add((1, 5));
        g1.Edges.Add((2, 3));
        return g1;
    }
    
    public static UndirectedGraph<int, Edge<int>> ProvideG2()
    {
        var g2 = new UndirectedGraph<int, Edge<int>>();
        g2.Vertices.AddRange(new[] {10, 20, 30, 50});
        g2.Edges.Add((10,20));
        g2.Edges.Add((20, 30));
        g2.Edges.Add((10, 30));
        g2.Edges.Add((10, 50));
        return g2;
    }
    
    public static UndirectedGraph<int, Edge<int>> ProvideG3()
    {
        var g3 = new UndirectedGraph<int, Edge<int>>();
        g3.Vertices.AddRange(new[] {10, 20, 30, 40, 50});
        g3.Edges.Add((10,20));
        g3.Edges.Add((20, 30));
        g3.Edges.Add((10, 30));
        g3.Edges.Add((10, 50));
        return g3;
    }
    
    public static UndirectedGraph<int, Edge<int>> ProvideG4()
    {
        var g4 = new UndirectedGraph<int, Edge<int>>();
        g4.Vertices.AddRange(new[] {1,2,3,5});
        g4.Edges.Add((1, 2));
        g4.Edges.Add((1, 3));
        g4.Edges.Add((1, 5));
        g4.Edges.Add((2, 5));
        g4.Edges.Add((2, 3));
        return g4;
    }
    
    public static UndirectedGraph<int, Edge<int>> ProvideG5()
    {
        var  g5 = new UndirectedGraph<int, Edge<int>>(); //G5=> Wikipedia Example G1
        g5.Vertices.AddRange(new []{1,2,3,4,5,6,8,7});
        g5.Edges.Add((1,2));
        g5.Edges.Add((1,4));
        g5.Edges.Add((1,5));
        g5.Edges.Add((2,3));
        g5.Edges.Add((2,6));
        g5.Edges.Add((3,4));
        g5.Edges.Add((3,7));
        g5.Edges.Add((4,8));
        g5.Edges.Add((5,6));
        g5.Edges.Add((5,8));
        g5.Edges.Add((6,7));
        g5.Edges.Add((7,8));
        return g5;
    }
    
    public static UndirectedGraph<int, Edge<int>> ProvideG6()
    {
        var g6 = new UndirectedGraph<int, Edge<int>>(); //G6=> Wikipedia Example G2
        g6.Vertices.AddRange(new[] {11,12,13,14,17,18,19,20});
        g6.Edges.Add((11,17));
        g6.Edges.Add((11,18));
        g6.Edges.Add((11,19));
        g6.Edges.Add((12,17));
        g6.Edges.Add((12,18));
        g6.Edges.Add((12,20));
        g6.Edges.Add((13,17));
        g6.Edges.Add((13,19));
        g6.Edges.Add((13,20));
        g6.Edges.Add((14,18));
        g6.Edges.Add((14,19));
        g6.Edges.Add((14,20));
        return g6;
    }
    public static UndirectedGraph<int, Edge<int>> ProvideG7()
    {
        var g7 = new UndirectedGraph<int, Edge<int>>(); //G7 is G6 with removed last Edge
        g7.Vertices.AddRange(new[] {11,12,13,14,17,18,19,20});
        g7.Edges.Add((11,17));
        g7.Edges.Add((11,18));
        g7.Edges.Add((11,19));
        g7.Edges.Add((12,17));
        g7.Edges.Add((12,18));
        g7.Edges.Add((12,20));
        g7.Edges.Add((13,17));
        g7.Edges.Add((13,19));
        g7.Edges.Add((13,20));
        g7.Edges.Add((14,18));
        g7.Edges.Add((14,19));
        return g7;
    }
    public static UndirectedGraph<int, Edge<int>> ProvideHexagon()
    {
        var hexagon = new UndirectedGraph<int, Edge<int>>();
        hexagon.Vertices.AddRange(new []{1,2,3,4,5,6});
        hexagon.Edges.Add((1,2));
        hexagon.Edges.Add((2,3));
        hexagon.Edges.Add((3,4));
        hexagon.Edges.Add((4,5));
        hexagon.Edges.Add((5,6));
        hexagon.Edges.Add((1,6));
        return hexagon;
    }

    
}
