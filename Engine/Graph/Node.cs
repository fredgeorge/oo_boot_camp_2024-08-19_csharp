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

    public bool CanReach(Node destination) => 
        Path(destination, NoVisitedNodes) != Graph.Path.None;

    public int HopCount(Node destination) => (int)Cost(destination, Link.FewestHops);

    public double Cost(Node destination) => Cost(destination, Link.LeastCost);

    public Path Path(Node destination) {
        var result = Path(destination, NoVisitedNodes);
        if (result == Graph.Path.None) throw new ArgumentException("Destination is unreachable");
        return result;
    }

    internal Path Path(Node destination, List<Node> visitedNodes) {
        if (this == destination) return new Path.ActualPath();
        if (visitedNodes.Contains(this)) return Graph.Path.None;
        Path champion = Graph.Path.None;
        foreach (var link in _links) {
            var challenger = link.Path(destination, CopyWithThis(visitedNodes));
            if (challenger.Cost() < champion.Cost()) champion = challenger;
        }

        return champion;
    }

    private double Cost(Node destination, Link.CostStrategy strategy) {
        var result = Cost(destination, NoVisitedNodes, strategy);
        if (result == Unreachable) throw new ArgumentException("Destination is unreachable");
        return result;
    }

    internal double Cost(Node destination, List<Node> visitedNodes, Link.CostStrategy strategy) {
        if (this == destination) return 0.0;
        if (visitedNodes.Contains(this) || _links.Count == 0) return Unreachable;
        return _links.Min(n => n.Cost(destination, CopyWithThis(visitedNodes), strategy));
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