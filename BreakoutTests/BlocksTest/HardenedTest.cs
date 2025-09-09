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


public class HardenedTest {
    public Breakout.Blocks.Block block1;
    public Breakout.Blocks.Block block3;




    [SetUp]
    public void Setup() {

        block1 = new Breakout.Blocks.Hardened(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), new NoImage());
        block3 = new Breakout.Blocks.NormalBlock(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), new NoImage());


    }
    [Test]
    public void DeleteBlock() {

        for (int i = 0; i < 10; i++) {
            block1.TakeHit();
        }

        Assert.True(block1.IsDeleted());
    }
    [Test]
    public void IsNotDeletedFromStart() {
        Assert.False(block1.IsDeleted());
    }
    [Test]

    public void HasDoubleHealth() {

        Assert.That(block1.Health, Is.EqualTo(block3.Health * 2));
    }

    [Test]
    public void hasDoubleValue() {
        Assert.That(block1.Value, Is.EqualTo(block3.Value * 2));
    }

}