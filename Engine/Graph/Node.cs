/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using static Engine.Graph.Path;

namespace Engine.Graph;

// Understands its neighbors
public class Node {
    private static readonly List<Node> NoVisitedNodes = new();

    private readonly List<Link> _links = new();

    public bool CanReach(Node destination) =>
        Path(destination, NoVisitedNodes, LeastCost) != None;

    public int HopCount(Node destination) => Path(destination, FewestHops).HopCount();

    public double Cost(Node destination) => Path(destination).Cost();

    public Path Path(Node destination) => Path(destination, LeastCost);

    private Path Path(Node destination, PathStrategy strategy) {
        var result = Path(destination, NoVisitedNodes, strategy);
        if (result == None) throw new ArgumentException("Destination is unreachable");
        return result;
    }

    internal Path Path(Node destination, List<Node> visitedNodes, PathStrategy strategy) {
        if (this == destination) return new ActualPath();
        if (visitedNodes.Contains(this)) return None;
        return _links
                   .Select(l => l.Path(destination, CopyWithThis(visitedNodes), strategy))
                   .MinBy(strategy.Invoke)
               ?? None;
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