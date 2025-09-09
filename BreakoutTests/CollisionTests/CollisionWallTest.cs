namespace BreakoutTests;

using System.Collections.Generic;
using System.Numerics;
using Breakout;
using Breakout.Blocks;
using Breakout.Collision;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using NUnit.Framework;


public class CollisionWallTest {

    public Breakout.Ball ball1;




    [SetUp]
    public void Setup() {
        ball1 = new Ball(
             new DynamicShape(new Vector2(0.02f, 0.5f), new Vector2(0.02f, 0.02f)),
             new NoImage());



    }

    [Test]
    public void CollisionWithLeftWallTest() {

        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.0f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(-0.02f, 0.0f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X > 0.0f);
    }

    [Test]
    public void CollisionWithRightWallTest() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.98f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.02f, 0.0f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X < 0.0f);
    }
    [Test]
    public void CollisionWithTopWallTest() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.98f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.0f, 0.02f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.True(ball1.Shape.AsDynamicShape().Velocity.Y < 0.0f);
    }
    [Test]
    public void BallGetsDeletedAtBottomWallTest() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.1f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.0f, -0.02f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.True(ball1.IsDeleted());
    }

    [Test]

    public void CollisionAtCornerTest() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.98f, 0.98f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.02f, 0.02f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X < 0.0f);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.Y < 0.0f);
    }
    [Test]

    public void CollisionAtCornerTest2() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.02f, 0.98f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(-0.02f, 0.02f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.True(ball1.Shape.AsDynamicShape().Velocity.X > 0.0f);
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.Y < 0.0f);
    }

    [Test]
    public void BallDoesNotGetDeletedAtTopWallTest() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.98f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.0f, -0.02f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.False(ball1.IsDeleted());
    }

    [Test]
    public void BallDoesNotGetDeletedAtLeftWallTest() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.0f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(-0.02f, 0.0f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.False(ball1.IsDeleted());
    }

    [Test]
    public void BallDoesNotGetDeletedAtRightWallTest() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.98f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.02f, 0.0f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            WallCollision.HandleKollisionWithWall(ball1);
        }


        Assert.False(ball1.IsDeleted());
    }




}