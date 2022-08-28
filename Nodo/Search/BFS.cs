namespace Nodo.Search;

public static class BFS
{
    /// <summary>
    /// Breadth First Search Algorithm
    /// </summary>
    /// <param name="graph">The Graph Object</param>
    /// <param name="start">Start vertex or null</param>
    /// <param name="visited"></param>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    /// <returns>Ordered Hashset</returns>
    public static HashSet<TVertex> BreadthFirstSearch<TVertex, TEdge>(
        this IGraph<TVertex, TEdge> graph,
        TVertex? start = default,
        HashSet<TVertex>? visited = null)
        where TEdge : IEdge<TVertex>
        where TVertex : IEquatable<TVertex>
    {
        if (start == null || start.Equals(default!))
            start = graph.Vertices.First();
        var results = new HashSet<TVertex>();
        var queue = new Queue<TVertex>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!results.Contains(current)) results.Add(current);
            var currentAdj = graph.Neighbors(current);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < currentAdj.Count; i++)
                if (!results.Contains(currentAdj[i]))
                    queue.Enqueue(currentAdj[i]);
        }

        visited?.UnionWith(results);
        return results;
    }
}