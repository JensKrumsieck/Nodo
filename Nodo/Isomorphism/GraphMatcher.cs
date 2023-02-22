using Nodo.Extensions;

namespace Nodo.Isomorphism;

/// <summary>
///     This is basically a translation of GraphMatcher@isomorphvf2.py of the networkx library
///     which is licensed under 3-clause BSD license: https://github.com/networkx/networkx/blob/main/LICENSE.txt
///     Copyright (C) 2004-2022, NetworkX Developers
///     Aric Hagberg // hagberg@lanl.gov
///     Dan Schult // dschult@colgate.edu
///     Pieter Swart // swart@lanl.gov
///     original file can be found here:
///     https://github.com/networkx/networkx/blob/main/networkx/algorithms/isomorphism/isomorphvf2.py
/// </summary>
public class GraphMatcher
{
    private readonly List<int> _g1Nodes;

    private readonly Dictionary<int, int> _g2NodeOrder;
    private readonly List<int> _g2Nodes;
    public readonly IGraph<int, Edge<int>> G1;
    public readonly IGraph<int, Edge<int>> G2;

    private TestMode _mode;
    private GraphMatcherState _state;
    internal Dictionary<int, int> Core1;
    internal Dictionary<int, int> Core2;

    internal Dictionary<int, int> Inout1;
    internal Dictionary<int, int> Inout2;

    public Dictionary<int, int> Mapping;

#pragma warning disable CS8618
    public GraphMatcher(IGraph<int, Edge<int>> g1, IGraph<int, Edge<int>> g2)
#pragma warning restore CS8618
    {
        G1 = g1;
        G2 = g2;
        //save copies
        _g1Nodes = g1.Vertices.ToList();
        _g2Nodes = g2.Vertices.ToList();
        _g2NodeOrder = _g2Nodes.ToDictionary(v => v, v => _g2Nodes.IndexOf(v));
        Initialize();
    }

    private void Initialize()
    {
        Core1 = new Dictionary<int, int>();
        Core2 = new Dictionary<int, int>();
        Inout1 = new Dictionary<int, int>();
        Inout2 = new Dictionary<int, int>();
        Mapping = new Dictionary<int, int>();
        _state = new GraphMatcherState(this);
    }

    private IEnumerable<(int, int)> CandidatePairsIter()
    {
        var t1Inout = Inout1.Where(k => !Core1.ContainsKey(k.Key)).ToList();
        var t2Inout = Inout2.Where(k => !Core2.ContainsKey(k.Key)).ToList();
        if (t1Inout.Any() && t2Inout.Any())
        {
            var node2 = G2.Vertices[t2Inout.Min(k => _g2NodeOrder[k.Key])];
            foreach (var node1 in t1Inout) yield return (node1.Key, node2);
        }
        else
        {
            var other = G2.Vertices[_g2Nodes.Where(n => !Core2.ContainsKey(n)).Min(v => _g2NodeOrder[v])];
            foreach (var node in _g1Nodes.Where(node => !Core1.ContainsKey(node))) yield return (node, other);
        }
    }

    public bool IsIsomorphic()
    {
        if (G1.Vertices.Count != G2.Vertices.Count) return false;
        var degrees1 = G1.Vertices.Select(v => G1.Degree(v)).OrderBy(s => s).ToList();
        var degrees2 = G2.Vertices.Select(v => G2.Degree(v)).OrderBy(s => s).ToList();
        return !degrees1.Where((t, i) => t != degrees2[i]).Any() && IsomorphismsIter().GetEnumerator().MoveNext();
    }

    public bool SubgraphIsIsomorphic() => SubgraphIsomorphismsIter().GetEnumerator().MoveNext();

    private IEnumerable<Dictionary<int, int>> SubgraphIsomorphismsIter()
    {
        _mode = TestMode.Subgraph;
        Initialize();
        foreach (var match in Match()) yield return match;
    }

    private IEnumerable<Dictionary<int, int>> IsomorphismsIter()
    {
        _mode = TestMode.Graph;
        Initialize();
        return Match();
    }

    private IEnumerable<Dictionary<int, int>> Match()
    {
        if (Core1.Count == G2.Vertices.Count)
        {
            Mapping = new Dictionary<int, int>(Core1); //copy
            yield return Mapping;
        }
        else
            foreach (var (g1Node, g2Node) in CandidatePairsIter())
            {
                if (!SyntacticFeasible(g1Node, g2Node)) continue;
                var newstate = new GraphMatcherState(this, g1Node, g2Node);
                _state = newstate;
                foreach (var m in Match()) yield return m;
                newstate.Restore();
            }
    }

    private bool SyntacticFeasible(int g1Node, int g2Node)
    {
        if (G1.Neighbors(g1Node).Where(neighbor => Core1.ContainsKey(neighbor))
              .Any(neighbor => !G2.Neighbors(g2Node).Contains(Core1[neighbor])))
            return false;
        if (G2.Neighbors(g2Node).Where(neighbor => Core2.ContainsKey(neighbor))
              .Any(neighbor => !G1.Neighbors(g1Node).Contains(Core2[neighbor])))
            return false;

        var num1 = G1.Neighbors(g1Node).Count(neighbor => Inout1.ContainsKey(neighbor) && !Core1.ContainsKey(neighbor));
        var num2 = G2.Neighbors(g2Node).Count(neighbor => Inout2.ContainsKey(neighbor) && !Core2.ContainsKey(neighbor));
        switch (_mode)
        {
            case TestMode.Graph when num1 != num2:
            case TestMode.Subgraph when !(num1 >= num2):
                return false;
        }

        num1 = G1.Neighbors(g1Node).Count(neighbor => !Inout1.ContainsKey(neighbor));
        num2 = G2.Neighbors(g2Node).Count(neighbor => !Inout2.ContainsKey(neighbor));

        return _mode switch
        {
            TestMode.Graph when num1 != num2 => false,
            TestMode.Subgraph when !(num1 >= num2) => false,
            _ => true
        };
    }
}