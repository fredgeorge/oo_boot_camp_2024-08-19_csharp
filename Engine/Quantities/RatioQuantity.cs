/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

// ReSharper disable ArrangeThisQualifier
namespace Engine.Quantities;

// Understands a specific measurement
public class RatioQuantity {
    internal const double Epsilon = 1e-10;
    
    private readonly double _amount;
    private readonly Unit _unit;
    
    internal RatioQuantity(double amount, Unit unit) {
        _amount = amount;
        _unit = unit;
    }

    public override bool Equals(object? obj) => 
        this == obj || obj is RatioQuantity other && Equals(other);
    
    private bool Equals(RatioQuantity other) => 
        this.IsCompatible(other) && Math.Abs(this._amount - ConvertedAmount(other)) < Epsilon;

    private bool IsCompatible(RatioQuantity other) => this._unit.IsCompatible(other._unit);

    private double ConvertedAmount(RatioQuantity other) => 
        this._unit.ConvertedAmount(other._amount, other._unit);

    public override int GetHashCode() => _unit.HashCode(_amount);
    
    public static RatioQuantity operator +(RatioQuantity left, RatioQuantity right) => 
        new(left._amount + left.ConvertedAmount(right), left._unit);
    
    public static RatioQuantity operator -(RatioQuantity q) => new(-q._amount, q._unit);

    public static RatioQuantity operator -(RatioQuantity left, RatioQuantity right) => left + -right;
}