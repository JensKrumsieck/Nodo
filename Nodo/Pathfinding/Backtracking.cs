namespace Nodo.Pathfinding;

public static class Backtracking
{
    /// <summary>
    ///     Uses backtracking for path detection of length limit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <param name="neighbors"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static HashSet<T> BackTrack<T>(T start, T goal, Func<T, IEnumerable<T>> neighbors, int limit)
    {
        var path = new HashSet<T> {start};
        var maxSteps = 2000;
        BackTrack(goal, path, neighbors, limit, ref maxSteps);
        return path;
    }
    
    /// <summary>
    ///     Recursive Implementation of backtracking
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="goal"></param>
    /// <param name="visited"></param>
    /// <param name="neighbors"></param>
    /// <param name="limit"></param>
    /// <param name="maxSteps"></param>
    /// <returns></returns>
    private static bool BackTrack<T>(T goal, ISet<T> visited, Func<T, IEnumerable<T>> neighbors, int limit,
        ref int maxSteps)
    {
        if (--maxSteps <= 0) return false;

        var last = visited.Last();
        if (Equals(last, goal) && visited.Count == limit)
        {
            visited.Add(goal);
            return true;
        }

        if (visited.Count >= limit) return false;

        foreach (var neighbor in neighbors(last))
        {
            if (visited.Contains(neighbor)) continue;

            visited.Add(neighbor);
            if (BackTrack(goal, visited, neighbors, limit, ref maxSteps)) return true;

            visited.Remove(visited.Last());
        }

        return false;
    }
}