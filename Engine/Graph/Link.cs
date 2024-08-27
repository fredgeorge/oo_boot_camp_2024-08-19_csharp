/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using static Engine.Graph.Path;

namespace Engine.Graph;

// Understands a connection from one Node to another
internal class Link {
    internal delegate double CostStrategy(double cost);
    internal static readonly CostStrategy LeastCost = cost => cost;
    internal static readonly CostStrategy FewestHops = _ => 1.0;
    
    private readonly double _cost;
    private readonly Node _target;

    internal static double TotalCost(List<Link> links) => links.Sum(l => l._cost);

    internal Link(double cost, Node target) {
        _cost = cost;
        _target = target;
    }

    internal Path Path(Node destination, List<Node> visitedNodes, PathStrategy strategy) {
        return _target.Path(destination, visitedNodes, strategy).Prepend(this);
    }
}