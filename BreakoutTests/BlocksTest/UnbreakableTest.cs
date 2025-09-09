namespace BreakoutTests.BlocksTest;

using System.Collections.Generic;
using System.Numerics;
using Breakout;
using Breakout.Blocks;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using NUnit.Framework;


public class UnbreakableTest {
    public Breakout.Blocks.Block block1;




    [SetUp]
    public void Setup() {

        block1 = new Breakout.Blocks.Unbreakable(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), new NoImage());


    }

    [Test]
    public void CannotDeleteBlock() {
        for (int i = 0; i < 10; i++) {
            block1.TakeHit();
        }
        Assert.False(block1.IsDeleted());
    }
    [Test]
    public void CannotLoseHealth() {
        int expected = 1;
        for (int i = 0; i < 10; i++) {
            block1.TakeHit();

        }
        Assert.That(expected, Is.EqualTo(block1.Health));
    }
    [Test]
    public void CannotGetValue() {
        int val = block1.Value;
        int expected = 0;
        Assert.That(expected, Is.EqualTo(val));

    }











}