namespace Nodo.Tests.Pathfinding;

public static class PathFindingDataProvider
{
    public static UndirectedGraph<int, Edge<int>> ProvideUnweightedGraph()
    {
        var graph = new UndirectedGraph<int, Edge<int>>();
        graph.Vertices.AddRange(new[] {1, 2, 3, 4, 5, 6});
        graph.Edges.Add((1, 6));
        graph.Edges.Add((1, 3));
        graph.Edges.Add((1, 2));
        graph.Edges.Add((2, 3));
        graph.Edges.Add((2, 4));
        graph.Edges.Add((3, 6));
        graph.Edges.Add((3, 4));
        graph.Edges.Add((4, 5));
        graph.Edges.Add((5, 6));
        return graph;
    }

    public static UndirectedGraph<int, WeightedEdge<int>> ProvideWeightedGraph()
    {
        var graph = new UndirectedGraph<int, WeightedEdge<int>>();
        graph.Vertices.AddRange(new[] {1, 2, 3, 4, 5, 6});
        graph.Edges.Add((1, 6, 14));
        graph.Edges.Add((1, 3, 9));
        graph.Edges.Add((1, 2, 7));
        graph.Edges.Add((2, 3, 10));
        graph.Edges.Add((2, 4, 15));
        graph.Edges.Add((3, 6, 2));
        graph.Edges.Add((3, 4, 11));
        graph.Edges.Add((4, 5, 6));
        graph.Edges.Add((5, 6, 9));
        return graph;
    }
}