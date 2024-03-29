﻿using FluentAssertions;
using Nodo.Search;
using Xunit;

namespace Nodo.Tests.Search;

public class DFSTests
{
    [Fact]
    public void DepthFirstSearch_ReturnsValidResult()
    {
        var result = SearchDataProvider.ProvideGraph().DepthFirstSearch();
        result.Should().BeEquivalentTo(new[] {1, 3, 6, 5, 8, 9, 10, 7, 4, 2}, o => o.WithStrictOrdering());
    }

    [Fact]
    public void ConnectedFigures_ReturnsValidResult()
    {
        // Same graph as above but Edges 1<->2 and 5<->7 were removed, leading to 2 connected figures!
        //2,4,7 and rest
        var vertices = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        var edges = new[]
        {
            new Edge<int>(1, 3), new Edge<int>(2, 4), new Edge<int>(3, 5), new Edge<int>(3, 6),
            new Edge<int>(4, 7), new Edge<int>(5, 8), new Edge<int>(5, 6), new Edge<int>(8, 9),
            new Edge<int>(9, 10)
        };
        var graph = new UndirectedGraph<int, Edge<int>>(vertices, edges);
        var result = graph.ConnectedFigures().ToArray();
        result.Length.Should().Be(2);
        result[0].Count.Should().Be(7);
        result[1].Count.Should().Be(3);
        result[1].Should().BeEquivalentTo(new[] {2, 4, 7});
    }
}