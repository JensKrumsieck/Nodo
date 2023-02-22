namespace Nodo.Isomorphism;

/// <summary>
///     This is basically a translation of GMState@isomorphvf2.py of the networkx library
///     which is licensed under 3-clause BSD license: https://github.com/networkx/networkx/blob/main/LICENSE.txt
///     Copyright (C) 2004-2022, NetworkX Developers
///     Aric Hagberg // hagberg@lanl.gov
///     Dan Schult // dschult@colgate.edu
///     Pieter Swart // swart@lanl.gov
///     original file can be found here:
///     https://github.com/networkx/networkx/blob/main/networkx/algorithms/isomorphism/isomorphvf2.py
/// </summary>
internal class GraphMatcherState
{
    private readonly int _depth;
    private readonly int _g1Node;
    private readonly int _g2Node;
    private readonly GraphMatcher _gm;

    public GraphMatcherState(GraphMatcher gm, int? g1Node = null, int? g2Node = null)
    {
        _gm = gm;
        _depth = _gm.Core1.Count;

        if (g1Node == null || g2Node == null)
        {
            _gm.Core1 = new Dictionary<int, int>();
            _gm.Core2 = new Dictionary<int, int>();
            _gm.Inout1 = new Dictionary<int, int>();
            _gm.Inout2 = new Dictionary<int, int>();
            return;
        }

        _g1Node = g1Node.Value;
        _g2Node = g2Node.Value;
        _gm.Core1[_g1Node] = _g2Node;
        _gm.Core2[_g2Node] = _g1Node;
        _depth = _gm.Core1.Count;
        if (!_gm.Inout1.ContainsKey(_g1Node)) _gm.Inout1[_g1Node] = _depth;
        if (!_gm.Inout2.ContainsKey(_g2Node)) _gm.Inout2[_g2Node] = _depth;

        var newNodes = new List<int>();
        foreach (var neighbors in _gm.Core1
                                     .Select(node => _gm.G1.Neighbors(node.Key)))
            newNodes.AddRange(neighbors
                                  .Where(n => !_gm.Core1.ContainsKey(n)));

        foreach (var node in newNodes
                     .Where(node => !_gm.Inout1.ContainsKey(node)))
            _gm.Inout1[node] = _depth;

        newNodes = new List<int>();
        foreach (var neighbors in _gm.Core2
                                     .Select(node => _gm.G2.Neighbors(node.Key)))
            newNodes.AddRange(neighbors
                                  .Where(n => !_gm.Core2.ContainsKey(n)));
        foreach (var node in newNodes
                     .Where(node => !_gm.Inout2.ContainsKey(node)))
            _gm.Inout2[node] = _depth;
    }

    public void Restore()
    {
        _gm.Core1.Remove(_g1Node);
        _gm.Core2.Remove(_g2Node);
        foreach (var inout in _gm.Inout1
                                 .Where(inout => inout.Value == _depth))
            _gm.Inout1.Remove(inout.Key);
        foreach (var inout in _gm.Inout2
                                 .Where(inout => inout.Value == _depth))
            _gm.Inout2.Remove(inout.Key);
    }
}