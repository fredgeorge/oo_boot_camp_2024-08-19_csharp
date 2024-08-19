/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System.Collections.Generic;
using Engine.Probability;
using Xunit;

namespace Engine.Tests.Unit;

// Ensures Chance operates correctly
public class ChanceTest {
    [Fact]
    public void Equality() {
        Assert.Equal(new Chance(0.75), new Chance(0.75));
        Assert.NotEqual(new Chance(0.75), new Chance(0.25));
        Assert.NotEqual(new Chance(0.75), new object());
        Assert.NotEqual(new Chance(0.75), null);
    }

    [Fact]
    public void Set() {
        Assert.Single(new HashSet<Chance> { new Chance(0.75), new Chance(0.75) });
        Assert.Contains(new Chance(0.75), new HashSet<Chance> { new Chance(0.75) });
    }

    [Fact]
    public void Hash() {
        Assert.Equal(new Chance(0.75).GetHashCode(), new Chance(0.75).GetHashCode());
    }
}