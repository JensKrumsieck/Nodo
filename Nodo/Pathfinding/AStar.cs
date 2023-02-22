namespace Nodo.Pathfinding;

public static class AStar
{
    private const int UnweightedDistance = 1;

    public static List<TVertex>? FindShortestPath<TVertex, TEdge>(UndirectedGraph<TVertex, TEdge> graph, TVertex start,
                                                                  TVertex end, Func<TVertex, double> h)
        where TVertex : IEquatable<TVertex> where TEdge : IEdge<TVertex>
    {
        var q = new List<TVertex> {start};
        var previous = graph.Vertices.ToDictionary(v => v, v => default(TVertex?));
        var gScore = graph.Vertices.ToDictionary(v => v, v => double.MaxValue);
        gScore[start] = 0; //per definition
        var fScore = graph.Vertices.ToDictionary(v => v, v => double.MaxValue);
        fScore[start] = h(start); //per definition
        var isWeightedEdge = typeof(IWeightedEdge<TVertex, double>).IsAssignableFrom(typeof(TEdge));
        while (q.Any())
        {
            var current = q.OrderBy(v => fScore[v]).First();
            if (current.Equals(end)) return PathfindingUtil.ReconstructPath(end, previous);
            q.Remove(current);
            foreach (var neighbor in graph.Neighbors(current))
            {
                var tentativeGScore = gScore[current];
                if (isWeightedEdge)
                {
                    var edge = graph.GetEdge(current, neighbor);
                    if (edge != null)
                    {
                        var weightedEdge = edge as IWeightedEdge<TVertex, double>;
                        tentativeGScore += weightedEdge!.Weight;
                    }
                }
                else
                    tentativeGScore += UnweightedDistance;

                if (tentativeGScore < gScore[neighbor])
                {
                    previous[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + h(neighbor);
                    if (!q.Contains(neighbor)) q.Add(neighbor);
                }
            }
        }

        return null;
    }
}