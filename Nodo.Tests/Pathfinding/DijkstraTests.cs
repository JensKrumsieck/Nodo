using FluentAssertions;
using Nodo.Pathfinding;
using Xunit;

namespace Nodo.Tests.Pathfinding;

public class DijkstraTests
{
    [Fact]
    public void Matrix_Dijkstra_ShouldBeValid()
    {
        //test values from https://www.geeksforgeeks.org/csharp-program-for-dijkstras-shortest-path-algorithm-greedy-algo-7/
        var graph = new[,]
        {
            {0, 4, 0, 0, 0, 0, 0, 8, 0}, 
            {4, 0, 8, 0, 0, 0, 0, 11, 0}, 
            {0, 8, 0, 7, 0, 4, 0, 0, 2},
            {0, 0, 7, 0, 9, 14, 0, 0, 0}, 
            {0, 0, 0, 9, 0, 10, 0, 0, 0}, 
            {0, 0, 4, 14, 10, 0, 2, 0, 0},
            {0, 0, 0, 0, 0, 2, 0, 1, 6}, 
            {8, 11, 0, 0, 0, 0, 1, 0, 7}, 
            {0, 0, 2, 0, 0, 0, 6, 7, 0}
        };
        var expected = new[] {0, 4, 12, 19, 21, 11, 9, 8, 14};
        var result = Dijkstra.FindPath(graph, 0);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Dijkstra_Should_Be_Valid()
    {
        var graph = new UndirectedGraph<int, Edge<int>>();
        graph.Vertices.AddRange(new[]{1, 2, 3, 4, 5, 6});
        graph.Edges.Add((1,6));
        graph.Edges.Add((1,3));
        graph.Edges.Add((1,2));
        graph.Edges.Add((2,3));
        graph.Edges.Add((2,4));
        graph.Edges.Add((3,6));
        graph.Edges.Add((3,4));
        graph.Edges.Add((4,5));
        graph.Edges.Add((5,6));
        var path = Dijkstra.FindShortestPathBetween(graph, 1, 5);
        path[0].Should().Be(1);
    }
}