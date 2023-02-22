namespace Nodo;

public class Edge<TVertex> : IEdge<TVertex>
{
    public Edge(TVertex source, TVertex target)
    {
        Source = source;
        Target = target;
    }

    public TVertex Source { get; }
    public TVertex Target { get; }

    public static implicit operator Edge<TVertex>((TVertex, TVertex) tuple) => new(tuple.Item1, tuple.Item2);
    public static implicit operator (TVertex, TVertex)(Edge<TVertex> edge) => (edge.Source, edge.Target);
    public override string ToString() => $"{Source} <-> {Target}";
}