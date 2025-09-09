namespace BreakoutTests;

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


public class BallTest {

    public Breakout.Ball ball1;




    [SetUp]
    public void Setup() {
        ball1 = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());



    }

    [Test]
    public void IsNotDeletedBall() {
        Assert.False(ball1.IsDeleted());
    }
    [Test]

    public void IsNotLaunchedFromStart() {
        Assert.False(ball1.IsLaunched);
    }
    [Test]

    public void CanMoveBallWhenLaunched() {
        ball1.balllaunch();
        ball1.Move();
        Assert.True(ball1.Shape.AsDynamicShape().Position.Y > 0.5f);
    }

    [Test]
    public void CanLaunchBall() {
        ball1.balllaunch();
        Assert.True(ball1.IsLaunched);
    }
    [Test]
    public void VelocityisZeroWhenNotLaunched() {
        ball1.Move();
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X == 0.0f);
    }

    [Test]
    public void VelocityisNotZeroWhenLaunched() {
        ball1.balllaunch();
        ball1.Move();
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X != 0.0f);
    }

    [Test]

    public void ballbounceXTest() {
        ball1.balllaunch();
        var expected = -ball1.Shape.AsDynamicShape().Velocity.X;
        ball1.BallBounceX();
        var result = ball1.Shape.AsDynamicShape().Velocity.X;
        Assert.That(result, Is.EqualTo(expected));
    }

    public void ballbounceYTest() {
        ball1.balllaunch();
        var expected = -ball1.Shape.AsDynamicShape().Velocity.Y;
        ball1.BallBounceY();
        var result = ball1.Shape.AsDynamicShape().Velocity.Y;
        Assert.That(result, Is.EqualTo(expected));
    }

    public void setMoveLeftTest() {
        ball1.SetMoveLeft(true);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X < 0.0f);
    }
    public void setMoveRightTest() {
        ball1.SetMoveRight(true);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X > 0.0f);
    }
    public void setMoveLeftFalseTest() {
        ball1.SetMoveLeft(false);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X == 0.0f);
    }
    public void setMoveRightFalseTest() {
        ball1.SetMoveRight(false);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X == 0.0f);
    }

    //keyhandler test
    [Test]

    public void KeyHandler_Launch_Test() {

        ball1.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Space);
        Assert.True(ball1.IsLaunched);
    }

    public void KeyHandler_MoveLeft_Test() {
        ball1.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Left);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X < 0.0f);
    }

    public void KeyHandler_MoveRight_Test() {
        ball1.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Right);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X > 0.0f);
    }
    public void KeyHandler_MoveLeft_Release_Test() {
        ball1.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Left);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X < 0.0f);
        ball1.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.Left);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X == 0.0f);
    }
    public void KeyHandler_MoveRight_Release_Test() {
        ball1.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Right);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X > 0.0f);
        ball1.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.Right);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X == 0.0f);
    }
}