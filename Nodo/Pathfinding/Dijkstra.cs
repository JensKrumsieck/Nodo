namespace Nodo.Pathfinding;

public static class PathfindingUtil
{
    /// <summary>
    /// Returns shortest path between start and end from pathfinding calculation
    /// </summary>
    /// <param name="end"></param>
    /// <param name="previous"></param>
    /// <typeparam name="TVertex"></typeparam>
    /// <returns></returns>
    public static List<TVertex> ReconstructPath<TVertex>(TVertex end, Dictionary<TVertex, TVertex?> previous)
        where TVertex : IEquatable<TVertex>
    {
        var path = new List<TVertex> {end};
        var u = end;
        while (previous.ContainsKey(u) && previous[u] is { } g && !g.Equals(default))
        {
            u = previous[u]!;
            path.Add(u);
        }

        path.Reverse();
        return path;
    }
}

public static class Dijkstra
{
    private const int UnweightedDistance = 1;
    /// <summary>
    /// Matrix based Implementation of Dijkstra Algorithm
    /// </summary>
    /// <param name="adjacency">Adjacency Matrix</param>
    /// <param name="srcV">Source Vertex</param>
    /// <returns></returns>
    public static int[] FindPath(MatrixInt adjacency, int srcV)
    {
        var len = adjacency.Length;
        var dist = new int[len];
        var shortest = new bool[len];
        for (var i = 0; i < len; i++)
        {
            dist[i] = int.MaxValue;
            shortest[i] = false;
        }

        dist[srcV] = 0;
        for (var i = 0; i < len - 1; i++)
        {
            var j = MinimumDistance(dist, shortest);
            shortest[j] = true;
            for (var v = 0; v < len; v++)
                if (!shortest[v] && adjacency[j, v] != 0 && dist[j] != int.MaxValue &&
                    dist[j] + adjacency[j, v] < dist[v])
                    dist[v] = dist[j] + adjacency[j, v];
        }

        return dist;
    }

    /// <summary>
    /// Find shortest path between two nodes
    /// Using UndirectedGraph
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    /// <returns></returns>
    public static (Dictionary<TVertex, TVertex?> previous, Dictionary<TVertex, double> distances) DijkstraDistances<TVertex, TEdge>(UndirectedGraph<TVertex, TEdge> graph, TVertex start, TVertex? end = default) where TVertex : IEquatable<TVertex> where TEdge : IEdge<TVertex>
    {
        var distances = graph.Vertices.ToDictionary(v => v, v => double.MaxValue);
        distances[start] = 0; //per definition
        var previous = graph.Vertices.ToDictionary(v => v, v => default(TVertex?));
        var q = new List<TVertex>(graph.Vertices);
        var isWeightedEdge = typeof(IWeightedEdge<TVertex, double>).IsAssignableFrom(typeof(TEdge));
        while (q.Any())
        {
            var u = q.OrderBy(v => distances[v]).First();
            q.Remove(u);
            if(end != null && u.Equals(end)) break;
            foreach (var v in graph.Neighbors(u).Where(v => q.Contains(v)))
            {
                var alt = distances[u];
                if (isWeightedEdge)
                {
                    var edge = graph.GetEdge(u, v);
                    if (edge != null)
                    {
                        var weightedEdge = edge as IWeightedEdge<TVertex, double>;
                        alt += weightedEdge!.Weight;
                    }
                }
                else
                    alt +=  UnweightedDistance;

                if (!(alt < distances[v])) continue;
                distances[v] = alt;
                previous[v] = u;
            }
        }

        return (previous, distances);
    }

    public static List<TVertex> FindShortestPath<TVertex, TEdge>(UndirectedGraph<TVertex, TEdge> graph, TVertex start,
                                                                 TVertex end)
        where TVertex : IEquatable<TVertex> where TEdge : IEdge<TVertex>
    {
        var (previous, _) = DijkstraDistances(graph, start, end);
        return PathfindingUtil.ReconstructPath(end, previous);
    }

    /// <summary>
    ///     Get Minimum Distance between to vertices
    /// </summary>
    /// <param name="dist"></param>
    /// <param name="shortest"></param>
    /// <returns></returns>
    private static int MinimumDistance(int[] dist, bool[] shortest)
    {
        var min = int.MaxValue;
        var iMin = -1;
        for (var i = 0; i < shortest.Length; i++)
        {
            if (shortest[i] || dist[i] > min) continue;
            min = dist[i];
            iMin = i;
        }

        return iMin;
    }
    
    /// <summary>
    ///     Returns the Distance Matrix using Dijkstra Algorithm
    /// </summary>
    /// <param name="adjacency"></param>
    /// <returns></returns>
    public static MatrixInt DistanceMatrix(this MatrixInt adjacency)
    {
        var len = adjacency.Length;
        var dist = new MatrixInt(len);
        for (var i = 0; i < len; i++)
        {
            var path = FindPath(adjacency, i);
            for (var j = 0; j < path.Length; j++)
                dist[i, j] = path[j];
        }
        return dist;
    }
}