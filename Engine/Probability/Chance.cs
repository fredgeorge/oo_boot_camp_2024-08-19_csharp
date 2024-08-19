/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Engine.Probability;

// Understands the likelihood of something specific happening
public class Chance {
    private readonly double _fraction;

    public Chance(double likelihoodAsFraction) {
        _fraction = likelihoodAsFraction;
    }
    
    public override bool Equals(object? obj) => 
        this == obj || obj is Chance other && this.Equals(other);
    
    private bool Equals(Chance other) => 
        this._fraction.Equals(other._fraction);

    public override int GetHashCode() => _fraction.GetHashCode();
}