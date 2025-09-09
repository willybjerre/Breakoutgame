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

public class BlockTest {
    public Breakout.Blocks.Block block1 = null!;




    [SetUp]
    public void Setup() {

        block1 = new NormalBlock(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), new NoImage());


    }


    [Test]
    public void IsNotDeletedBlock() {



        Assert.False(block1.IsDeleted());
    }

    [Test]
    public void CanLoseHealth() {

        int PreHealth = block1.GetHealth();

        block1.TakeHit();

        Assert.True(block1.GetHealth() < PreHealth);
    }

    [Test]
    public void CanDeleteWithTakeHit() {

        for (int i = 0; i < 2; i++) {
            block1.TakeHit();
        }

        Assert.True(block1.IsDeleted());
    }



}