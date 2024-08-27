/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Engine.Graph;

// Understands a specific route from one Node to another
public abstract class Path {
    internal delegate double PathStrategy(Path path);
    internal static readonly PathStrategy LeastCost = p => p.Cost();
    
    internal static readonly Path None = new NoPath();

    internal virtual Path Prepend(Link link) => this;

    public abstract double Cost();

    public abstract int HopCount();

    internal class ActualPath : Path {
        private readonly List<Link> _links = new();

        internal override Path Prepend(Link link) {
            _links.Insert(0, link);
            return this;
        }

        public override double Cost() => Link.TotalCost(_links);

        public override int HopCount() => _links.Count;
    }

    private class NoPath : Path {
        public override double Cost() => double.PositiveInfinity;
        public override int HopCount() => int.MaxValue;
    }
}