namespace Nodo;

public class UndirectedGraph<TVertex, TEdge> : IGraph<TVertex, TEdge>
    where TEdge : IEdge<TVertex> where TVertex : IEquatable<TVertex>
{
    private Dictionary<TVertex, List<TVertex>>? _cachedNeighbors;

    public bool CacheNeighbors = true;

    public UndirectedGraph(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges) : this()
    {
        Vertices.AddRange(vertices);
        Edges.AddRange(edges);
    }

    public UndirectedGraph()
    {
    }

    public List<TVertex> Vertices { get; } = new();
    public List<TEdge> Edges { get; } = new();

    public List<TVertex> Neighbors(TVertex needle)
    {
        if (CacheNeighbors && (_cachedNeighbors == null || _cachedNeighbors.Count != Vertices.Count))
            RebuildCache();
        return CacheNeighbors ? _cachedNeighbors![needle] : _Neighbors(needle).ToList();
    }

    private IEnumerable<TVertex> _Neighbors(TVertex needle) =>
        Edges
            .Where(e => e.Source.Equals(needle) || e.Target.Equals(needle))
            .Select(e => e.Source.Equals(needle) ? e.Target : e.Source);

    public void RebuildCache() => _cachedNeighbors = Vertices.ToDictionary(v => v, v => _Neighbors(v).ToList());

    /// <summary>
    ///     Returns edge containing u and v if exists, null otherwise
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public TEdge? GetEdge(TVertex u, TVertex v) =>
        Edges.FirstOrDefault(e => (e.Source.Equals(u) && e.Target.Equals(v)) ||
                                  (e.Source.Equals(v) && e.Target.Equals(u)));
}