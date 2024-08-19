/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System;
using Engine.Geometry;
using Xunit;

namespace Engine.Tests.Unit;

// Ensures that Rectangle works correctly
public class RectangleTest
{
    [Fact]
    public void Area()
    {
        Assert.Equal(24.0, new Rectangle(4.0, 6.0).Area());
        Assert.Equal(28.0, new Rectangle(4, 7).Area());
    }
    [Fact]
    public void Perimeter()
    {
        Assert.Equal(20, new Rectangle(4.0, 6.0).Perimeter());
        Assert.Equal(22, new Rectangle(4, 7).Perimeter());
    }

    [Fact]
    public void Parameters() {
        Assert.Throws<ArgumentException>(() => new Rectangle(0, 6));
        Assert.Throws<ArgumentException>(() => new Rectangle(4, 0));
    }
}
