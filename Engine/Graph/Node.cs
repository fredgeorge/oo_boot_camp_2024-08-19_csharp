/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Engine.Graph;

// Understands its neighbors
public class Node {
    private const int Unreachable = -1;
    private readonly List<Node> _neighbors = new();

    public Node To(Node neighbor) {
        _neighbors.Add(neighbor);
        return neighbor;
    }

    public bool CanReach(Node destination) => HopCount(destination, NoVisitedNodes()) != Unreachable;

    public int HopCount(Node destination) {
        var result = HopCount(destination, NoVisitedNodes());
        if (result == Unreachable) throw new ArgumentException("Destination is unreachable");
        return result;
    }

    private int HopCount(Node destination, List<Node> visitedNodes) {
        if (this == destination) return 0;
        if (visitedNodes.Contains(this)) return Unreachable;
        visitedNodes.Add(this);
        foreach (var n in _neighbors) {
            var result = n.HopCount(destination, visitedNodes);
            if (result != Unreachable) return result + 1;
        }
        return Unreachable;
    }

    private List<Node> NoVisitedNodes() => new();
}