using FluentAssertions;
using Nodo.Pathfinding;
using Xunit;

namespace Nodo.Tests.Pathfinding;

public class BacktrackingTests
{
    [Fact]
    public void Should_Produce_Valid_Paths()
    {
        var graph = new UndirectedGraph<int, Edge<int>>();
        graph.Vertices.AddRange(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
        graph.Edges.Add((1, 2));
        graph.Edges.Add((2, 3));
        graph.Edges.Add((2, 4));
        graph.Edges.Add((4, 5));
        graph.Edges.Add((5, 6));
        graph.Edges.Add((3, 7));
        graph.Edges.Add((7, 8));
        graph.Edges.Add((8, 9));
        graph.Edges.Add((9, 10));

        var start = 1;
        var end = 10;
        //path therefore should be 1,2,3,7,8,9,10 with a length of 7!
        var result = Backtracking.BackTrack(start, end, graph.Neighbors, 7);
        result.Should().HaveCount(7);
        result.Should().BeEquivalentTo(new[] {1, 2, 3, 7, 8, 9, 10});
    }
}