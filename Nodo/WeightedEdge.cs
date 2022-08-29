namespace Nodo;

public class WeightedEdge<TVertex> : IEdge<TVertex>, IWeightedEdge<TVertex, double>
{
    public WeightedEdge(TVertex source, TVertex target, double weight)
    {
        Source = source;
        Target = target;
        Weight = weight;
    }

    public TVertex Source { get; }
    public TVertex Target { get; }
    public double Weight { get; set; }
    
    public static implicit operator WeightedEdge<TVertex>((TVertex, TVertex, double) tuple) => new(tuple.Item1, tuple.Item2, tuple.Item3);
    public static implicit operator (TVertex, TVertex, double)(WeightedEdge<TVertex> edge) => (edge.Source, edge.Target, edge.Weight);
    public override string ToString() => $"{Source} <-> {Target}: {Weight}";
}