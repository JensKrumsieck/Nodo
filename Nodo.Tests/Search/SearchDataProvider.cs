namespace Nodo.Tests.Search;

public static class SearchDataProvider
{
    public static UndirectedGraph<int, Edge<int>> ProvideGraph()
    {
        var vertices = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        var edges = new[]
        {
            new Edge<int>(1, 2), new Edge<int>(1, 3), new Edge<int>(2, 4), new Edge<int>(3, 5), new Edge<int>(3, 6),
            new Edge<int>(4, 7), new Edge<int>(5, 7), new Edge<int>(5, 8), new Edge<int>(5, 6), new Edge<int>(8, 9),
            new Edge<int>(9, 10),
        };
        return new UndirectedGraph<int, Edge<int>>(vertices, edges);
    }
}