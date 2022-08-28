using FluentAssertions;
using Nodo.Search;
using Xunit;

namespace Nodo.Tests.Search;

public class BFSTests
{
    [Fact]
    public void DepthFirstSearch_ReturnsValidResult()
    {
        var result = SearchDataProvider.ProvideGraph().BreadthFirstSearch();
        result.Should().BeEquivalentTo(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, o => o.WithStrictOrdering());
    }
}