/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

// ReSharper disable ArrangeThisQualifier

namespace Engine.Probability {
    
    // Understands the likelihood of something specific happening
    public class Chance {
        private const double CertainFraction = 1.0;
        private const double Epsilon = 1e-10;
        
        private readonly double _fraction;

        internal Chance(double likelihoodAsFraction) {
            if (likelihoodAsFraction < 0.0 || likelihoodAsFraction > CertainFraction)
                throw new ArgumentException("Chance value must be between 0.0 and 1.0, inclusive");
            _fraction = likelihoodAsFraction;
        }
    
        public override bool Equals(object? obj) => 
            this == obj || obj is Chance other && this.Equals(other);
    
        private bool Equals(Chance other) => 
            Math.Abs(this._fraction - other._fraction) < Epsilon;

        public override int GetHashCode() => 
            Math.Round(_fraction / Epsilon).GetHashCode();

        public Chance Not() => new(CertainFraction - _fraction);

        public static Chance operator !(Chance c) => c.Not();

        public Chance And(Chance other) => new(this._fraction * other._fraction);

        public static Chance operator &(Chance left, Chance right) => left.And(right);

        // DeMorgan's Law: https://en.wikipedia.org/wiki/De_Morgan%27s_laws
        public Chance Or(Chance other) => !(!this & !other);

        public static Chance operator |(Chance left, Chance right) => left.Or(right);
    }
}

namespace Engine.Probability.Extensions {
    public static class ChanceExtensions {
        public static Chance Chance(this double fraction) => new(fraction);
        public static Chance Chance(this int fraction) => new(fraction);
    }
}