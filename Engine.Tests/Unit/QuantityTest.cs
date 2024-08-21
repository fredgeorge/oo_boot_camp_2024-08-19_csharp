/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System.Collections.Generic;
using Engine.Quantities;
using Xunit;
using static Engine.Quantities.Unit;

namespace Engine.Tests.Unit;

// Ensures Quantities operate correctly
public class QuantityTest {
    [Fact]
    public void EqualityOfLikeUnits() {
        Assert.Equal(new Quantity(8.0, Tablespoon), new Quantity(8.0, Tablespoon));
        Assert.NotEqual(new Quantity(8, Tablespoon), new Quantity(6.0, Tablespoon));
        Assert.NotEqual(new Quantity(8, Tablespoon), new object());
        Assert.NotEqual(new Quantity(8, Tablespoon), null);
    }

    [Fact]
    public void EqualityOfDifferentUnits() {
        Assert.NotEqual(new Quantity(8.0, Tablespoon), new Quantity(8.0, Pint));
    }

    [Fact]
    public void Set() {
        Assert.Single(new HashSet<Quantity> { new Quantity(8.0, Tablespoon), new Quantity(8.0, Tablespoon) });
        Assert.Contains(new Quantity(8.0, Tablespoon), new HashSet<Quantity> { new Quantity(8.0, Tablespoon) });
    }

    [Fact]
    public void Hash() {
        Assert.Equal(new Quantity(8.0, Tablespoon).GetHashCode(), new Quantity(8.0, Tablespoon).GetHashCode());
    }
}