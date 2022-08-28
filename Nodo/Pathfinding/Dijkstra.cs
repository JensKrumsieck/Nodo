namespace Nodo.Pathfinding;

public static class Dijkstra
{
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

    const int UnweightedDistance = 1;
    /// <summary>
    /// Find shortest path between two nodes
    /// Using Undirected & Unweighted Graph!
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    /// <returns></returns>
    public static List<TVertex> FindShortestPathBetween<TVertex, TEdge>(UndirectedGraph<TVertex, TEdge> graph, TVertex start,
        TVertex end) where TVertex : IEquatable<TVertex> where TEdge : IEdge<TVertex>
    {
        var current = start;
        var visited = new HashSet<TVertex>();
        var stack = new Stack<TVertex>(graph.Vertices);
        var distances = graph.Vertices.ToDictionary(v => v, v => int.MaxValue); //set distances of each vertex to max
        distances[start] = 0;
        while (true)
        {
            //calculate distances for current's neighbors
            foreach (var neighbor in graph.Neighbors(current).Where(v => !visited.Contains(v)))
            {
                var currentDistance = distances[start] + UnweightedDistance; //unweighted graph -> distance is 1
                if (currentDistance < distances[neighbor]) distances[neighbor] = currentDistance;
            }
            //add current node to visited set
            visited.Add(current);
            TVertex next = stack.Pop();
            stack
            if (distances[next] == int.MaxValue)
            {
                if (distances[end] == int.MaxValue) return new List<TVertex>(); //no path found :(
                visited.Add(end);
                break;
            }

            var smallest = next;
            current = smallest;
        }

        return BuildPath(graph, start, end, visited, distances);
    }

    private static List<TVertex> BuildPath<TVertex, TEdge>(UndirectedGraph<TVertex, TEdge> graph, TVertex start, TVertex end, 
        HashSet<TVertex> visited, Dictionary<TVertex, int> distances) 
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TVertex>
    {
        var current = end;
        var path = new List<TVertex>() {current};
        var currentDistance = distances[end];
        while (true)
        {
            if(current.Equals(start)) break;
            foreach (var neighbor in graph.Neighbors(current).Where(v => !visited.Contains(v)))
            {
                
                if (currentDistance - UnweightedDistance == distances[neighbor])
                {
                    current = neighbor;
                    path.Add(current);
                    currentDistance -= UnweightedDistance;
                    break;
                }
            }
        }

        path.Reverse();
        return path;
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