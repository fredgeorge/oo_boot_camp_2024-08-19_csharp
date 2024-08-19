/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Engine.Geometry;

// Understands a polygon with four sides at right angles
public class Rectangle {
    private readonly double _length;
    private readonly double _width;

    public Rectangle(double length, double width) {
        _length = length;
        _width = width;
    }

    public double Area() {
        return _length * _width;
    }
}