using FluentAssertions;
using Nodo.Pathfinding;
using Xunit;
using static Nodo.Tests.Pathfinding.PathFindingDataProvider;

namespace Nodo.Tests.Pathfinding;

public class AStarTests
{
    [Fact]
    public void AStar_Is_Valid()
    {
        var graph = ProvideWeightedGraph();
        var path = AStar.FindShortestPath(graph, 1, 5, v => 0)!;
        path[0].Should().Be(1);
        path[1].Should().Be(3);
        path[2].Should().Be(6);
        path[3].Should().Be(5);
    }
}