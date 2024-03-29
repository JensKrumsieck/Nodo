﻿namespace Nodo;

public interface IGraph<TVertex, TEdge> where TEdge : IEdge<TVertex> where TVertex : IEquatable<TVertex>
{
    public List<TVertex> Vertices { get; }
    public List<TEdge> Edges { get; }

    public List<TVertex> Neighbors(TVertex needle);
}