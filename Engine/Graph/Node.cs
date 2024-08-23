/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Engine.Graph;

// Understands its neighbors
public class Node {
    private const double Unreachable = double.PositiveInfinity;
    private static readonly List<Node> NoVisitedNodes = new();
    
    private readonly List<Node> _neighbors = new();

    public Node To(Node neighbor) {
        _neighbors.Add(neighbor);
        return neighbor;
    }

    public bool CanReach(Node destination) => HopCount(destination, NoVisitedNodes) != Unreachable;

    public int HopCount(Node destination) {
        var result = HopCount(destination, NoVisitedNodes);
        if (result == Unreachable) throw new ArgumentException("Destination is unreachable");
        return (int)result;
    }

    private double HopCount(Node destination, List<Node> visitedNodes) {
        if (this == destination) return 0.0;
        if (visitedNodes.Contains(this) || _neighbors.Count == 0) return Unreachable;
        return _neighbors.Min(n => n.HopCount(destination, CopyWithThis(visitedNodes)) + 1);
    }
    
    private List<Node> CopyWithThis(List<Node> originals) => new(originals) { this };
}