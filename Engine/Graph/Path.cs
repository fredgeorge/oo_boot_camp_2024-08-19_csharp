/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Engine.Graph;

// Understands a specific route from one Node to another
public class Path {
    private readonly List<Link> _links = new();
    
    internal Path() {}

    internal Path Prepend(Link link) {
        _links.Insert(0, link);
        return this;
    }
    
    public double Cost() => Link.TotalCost(_links);
    
    public int HopCount() => _links.Count;
}