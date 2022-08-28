using FluentAssertions;
using Nodo.Extensions;
using Nodo.Isomorphism;
using Xunit;

namespace Nodo.Tests.Isomorphism;
using static IsomorphismDataProvider;
public class IsomorphismsTests
{
    [Fact]
    public void Graphs_Of_Different_Lenght_Are_Not_Isomorphic()
    {
        var matcher = new GraphMatcher(ProvideG2(), ProvideG3());
        Assert.False(matcher.IsIsomorphic());
    }
    
    [Fact]
    public void Test_Is_Isomorphic()
    {
        var matcher = new GraphMatcher(ProvideG1(), ProvideG1());
        Assert.True(matcher.IsIsomorphic());
        matcher = new GraphMatcher(ProvideG1(), ProvideG2());
        Assert.True(matcher.IsIsomorphic());
        matcher = new GraphMatcher(ProvideG1(), ProvideG4());
        Assert.False(matcher.IsIsomorphic());
        matcher = new GraphMatcher(ProvideG1(), ProvideG3());
        Assert.False(matcher.IsIsomorphic());
        matcher = new GraphMatcher(ProvideG5(), ProvideG6());
        Assert.True(matcher.IsIsomorphic());
        matcher = new GraphMatcher(ProvideG6(), ProvideG7());
        Assert.False(matcher.IsIsomorphic());
        matcher = new GraphMatcher(ProvideHexagon(), ProvideHexagon());
        Assert.True(matcher.IsIsomorphic());
    }
    
    [Fact]
    public void Test_Is_Subgraph_Isomorphic()
    {
        var matcher = new GraphMatcher(ProvideG1(), ProvideG1()); //same graph
        Assert.True(matcher.SubgraphIsIsomorphic());
        matcher = new GraphMatcher(ProvideG1(), ProvideG2()); //isomorphic, should have subgraph
        Assert.True(matcher.SubgraphIsIsomorphic());
        matcher = new GraphMatcher(ProvideG3(), ProvideG1());
        Assert.True(matcher.SubgraphIsIsomorphic());
        matcher = new GraphMatcher(ProvideHexagon(), ProvideHexagon());
        Assert.True(matcher.SubgraphIsIsomorphic());
        matcher = new GraphMatcher(ProvideHexagon(), ProvideG1());
        Assert.False(matcher.SubgraphIsIsomorphic());
    }

    [Fact]
    public void Test_IsomorphicExtensionMethod()
    {
        Assert.True(ProvideG1().IsIsomorphicTo(ProvideG2()));
        Assert.False(ProvideG3().IsIsomorphicTo(ProvideG1()));
        ProvideG1().IsIsomorphicTo(ProvideG2(), out var mapping);
        mapping.Should().HaveCount(ProvideG1().Vertices.Count);
    }
    
    [Fact]
    public void Test_SubgraphIsomorphicExtensionMethod()
    {
        Assert.True(ProvideG1().IsSubgraphIsomorphicTo(ProvideG2()));
        Assert.True(ProvideG3().IsSubgraphIsomorphicTo(ProvideG1()));
        ProvideG3().IsSubgraphIsomorphicTo(ProvideG1(), out var mapping);
        mapping.Should().HaveCount(ProvideG1().Vertices.Count);
    }
}