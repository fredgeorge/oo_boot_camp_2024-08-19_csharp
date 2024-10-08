/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System;
using System.Collections.Generic;
using Engine.Probability;
using Engine.Probability.Extensions;
using Xunit;

namespace Engine.Tests.Unit;

// Ensures Chance operates correctly
public class ChanceTest {
    [Fact]
    public void Equality() {
        Assert.Equal(0.75.Chance(), 0.75.Chance());
        Assert.NotEqual(0.75.Chance(), 0.25.Chance());
        Assert.NotEqual(0.75.Chance(), new object());
#pragma warning disable xUnit2000
        Assert.NotEqual(0.75.Chance(), null);
#pragma warning restore xUnit2000
    }

    [Fact]
    public void Set() {
        Assert.Single(new HashSet<Chance> { 0.75.Chance(), 0.75.Chance() });
        Assert.Contains(0.75.Chance(), new HashSet<Chance> { 0.75.Chance() });
    }

    [Fact]
    public void Hash() {
        Assert.Equal(0.75.Chance().GetHashCode(), 0.75.Chance().GetHashCode());
        Assert.Equal(0.3.Chance().GetHashCode(), (!!0.3.Chance()).GetHashCode());
    }

    [Fact]
    public void Not() {
        Assert.Equal(0.25.Chance(), !0.75.Chance());
        Assert.Equal(0.75.Chance(), !!0.75.Chance());
        Assert.Equal(0.75.Chance(), 0.75.Chance().Not().Not());
        Assert.Equal(0.Chance(), !1.Chance());
        Assert.Equal(1.Chance(), !0.Chance());
        Assert.Equal(0.3.Chance(), !!0.3.Chance());
    }

    [Fact]
    public void And()
    {
        Assert.Equal(0.25.Chance(), 0.5.Chance() & 0.5.Chance());
        Assert.Equal(0.1875.Chance(), 0.75.Chance() & 0.25.Chance());
        Assert.Equal(0.25.Chance().And(0.75.Chance()), 0.75.Chance() & 0.25.Chance());
        Assert.Equal(0.75.Chance(), 0.75.Chance() & 1.Chance());
        Assert.Equal(0.Chance(), 0.Chance() & 0.75.Chance());
    }
        
    [Fact]
    public void Or()
    {
        Assert.Equal(0.75.Chance(), 0.5.Chance() | 0.5.Chance());
        Assert.Equal(0.8125.Chance(), 0.75.Chance() | 0.25.Chance());
        Assert.Equal(0.25.Chance().Or(0.75.Chance()), 0.75.Chance() | 0.25.Chance());
        Assert.Equal(1.Chance(), 0.75.Chance() | 1.Chance());
        Assert.Equal(0.75.Chance(), 0.Chance() | 0.75.Chance());
    }

    [Fact]
    public void InvalidFractions() {
        Assert.Throws<ArgumentException>(() => (-0.1).Chance());
        Assert.Throws<ArgumentException>(() => 1.1.Chance());
    }
}