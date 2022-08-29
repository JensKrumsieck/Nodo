namespace Nodo;

public interface IWeightedEdge<out TVertex, TNumber> : IEdge<TVertex> where TNumber : IComparable<TNumber>
{
    public TNumber Weight { get; set; }
}