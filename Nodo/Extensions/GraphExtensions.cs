namespace Nodo.Extensions;

public static class GraphExtensions
{
    public static int Degree<TVertex, TEdge>(this IGraph<TVertex, TEdge> graph, TVertex test)
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TVertex> =>
        graph.Neighbors(test).Count;
}