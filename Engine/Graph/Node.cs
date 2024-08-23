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
    
    private readonly List<Link> _links = new();

    public bool CanReach(Node destination) => HopCount(destination, NoVisitedNodes) != Unreachable;

    public int HopCount(Node destination) {
        var result = HopCount(destination, NoVisitedNodes);
        if (result == Unreachable) throw new ArgumentException("Destination is unreachable");
        return (int)result;
    }

    public double Cost(Node destination) {
        var result = Cost(destination, NoVisitedNodes);
        if (result == Unreachable) throw new ArgumentException("Destination is unreachable");
        return result;
    }

    internal double HopCount(Node destination, List<Node> visitedNodes) {
        if (this == destination) return 0.0;
        if (visitedNodes.Contains(this) || _links.Count == 0) return Unreachable;
        return _links.Min(n => n.HopCount(destination, CopyWithThis(visitedNodes)));
    }

    internal double Cost(Node destination, List<Node> visitedNodes) {
        if (this == destination) return 0.0;
        if (visitedNodes.Contains(this) || _links.Count == 0) return Unreachable;
        return _links.Min(n => n.Cost(destination, CopyWithThis(visitedNodes)));
    }
    
    private List<Node> CopyWithThis(List<Node> originals) => [..originals, this];
    
    public LinkBuilder Cost(double amount) => new LinkBuilder(amount, _links);

    public class LinkBuilder {
        private readonly double _cost;
        private readonly List<Link> _links;

        internal LinkBuilder(double cost, List<Link> links) {
            _cost = cost;
            _links = links;
        }

        public Node To(Node neighbor) {
            _links.Add(new Link(_cost, neighbor));
            return neighbor;
        }
    }
}