/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Engine.Quantities;

// Understands a specific measurement
public class Quantity(double amount, Unit unit) {
    private readonly double _amount = amount;
    private readonly Unit _unit = unit;

    public override bool Equals(object? obj) => 
        this == obj || obj is Quantity other && Equals(other);
    
    private bool Equals(Quantity other) => 
        this._unit == other._unit && this._amount == other._amount;
    
    public override int GetHashCode() => HashCode.Combine(_amount, _unit);
}