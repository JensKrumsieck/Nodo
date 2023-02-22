using FluentAssertions;
using Nodo.Pathfinding;
using Xunit;
using static Nodo.Tests.Pathfinding.PathFindingDataProvider;

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
    public void Dijkstra_Distance_Should_Be_Valid()
    {
        var (_, distances) = Dijkstra.DijkstraDistances(ProvideUnweightedGraph(), 1, 5);
        var distanceToFive = distances[5];
        distanceToFive.Should().Be(2); //1,6,5
    }

    [Fact]
    public void Dijkstra_Path_Should_Be_Valid()
    {
        var graph = ProvideUnweightedGraph();
        var path = Dijkstra.FindShortestPath(graph, 1, 5);
        path[0].Should().Be(1);
        path[1].Should().Be(6);
        path[2].Should().Be(5);
    }

    [Fact]
    public void Dijkstra_Distance_Should_Be_Valid_For_Weighted()
    {
        //wikipedia.de example
        var graph = ProvideWeightedGraph();
        var (previous, distances) = Dijkstra.DijkstraDistances(graph, 1, 5);
        distances[5].Should().Be(20);
        var path = PathfindingUtil.ReconstructPath(5, previous);
        path[0].Should().Be(1);
        path[1].Should().Be(3);
        path[2].Should().Be(6);
        path[3].Should().Be(5);
    }
}