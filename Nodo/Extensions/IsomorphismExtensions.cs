using Nodo.Isomorphism;

namespace Nodo.Extensions;

public static class IsomorphismExtensions
{
    /// <summary>
    ///     Returns true if graphG is isomorphic to graphH
    /// </summary>
    /// <param name="graphG"></param>
    /// <param name="graphH"></param>
    /// <param name="mapping"></param>
    /// <returns></returns>
    public static bool IsIsomorphicTo(this IGraph<int, Edge<int>> graphG, IGraph<int, Edge<int>> graphH) =>
        IsIsomorphicTo(graphG, graphH, out _);

    /// <summary>
    ///     Returns true if graphG is isomorphic to graphH
    /// </summary>
    /// <param name="graphG"></param>
    /// <param name="graphH"></param>
    /// <param name="mapping"></param>
    /// <returns></returns>
    public static bool IsIsomorphicTo(this IGraph<int, Edge<int>> graphG, IGraph<int, Edge<int>> graphH,
                                      out Dictionary<int, int> mapping)
    {
        var matcher = new GraphMatcher(graphG, graphH);
        var result = matcher.IsIsomorphic();
        mapping = matcher.Mapping;
        return result;
    }

    /// <summary>
    ///     Returns true if target has subgraph which is isomorphic to search
    ///     search must not contain more nodes than target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="search"></param>
    /// <param name="mapping"></param>
    /// <returns></returns>
    public static bool IsSubgraphIsomorphicTo(this IGraph<int, Edge<int>> target, IGraph<int, Edge<int>> search,
                                              out Dictionary<int, int> mapping)
    {
        var matcher = new GraphMatcher(target, search);
        var result = matcher.SubgraphIsIsomorphic();
        mapping = matcher.Mapping;
        return result;
    }

    /// <summary>
    ///     Returns true if target has subgraph which is isomorphic to search
    ///     search must not contain more nodes than target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="search"></param>
    /// <param name="mapping"></param>
    /// <returns></returns>
    public static bool IsSubgraphIsomorphicTo(this IGraph<int, Edge<int>> target, IGraph<int, Edge<int>> search) =>
        IsSubgraphIsomorphicTo(target, search, out _);
}