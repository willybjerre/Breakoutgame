namespace BreakoutTests.CollisionTests;

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


public class CollisionBlockTest {

    public Breakout.Ball ball1 = null!;

    public Breakout.Blocks.Block block1 = null!;




    [SetUp]
    public void Setup() {
        ball1 = new Ball(
             new DynamicShape(new Vector2(0.02f, 0.5f), new Vector2(0.02f, 0.02f)),
             new NoImage());




    }


    [Test]
    public void BallHitsFromTop_BouncesY() {
        var ball = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );
        ball.balllaunch();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(0.0f, -0.01f);

        var block = new Breakout.Blocks.NormalBlock(
            new StationaryShape(0.5f, 0.42f, 0.08f, 0.05f),
            new NoImage()
     );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsStationaryShape()).Collision) {
                BlockCollision.HandleKollisionWithBlock(ball, block);
                break;
            }
        }

        Assert.Greater(ball.Shape.AsDynamicShape().Velocity.Y, 0.0f);
    }

    [Test]
    public void BallHitsFromBottom_BouncesY() {
        var ball = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.4f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );
        ball.balllaunch();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(0.0f, 0.01f);

        var block = new Breakout.Blocks.NormalBlock(
            new StationaryShape(0.5f, 0.44f, 0.08f, 0.05f),
            new NoImage()
        );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsStationaryShape()).Collision) {
                BlockCollision.HandleKollisionWithBlock(ball, block);
                break;
            }
        }

        Assert.Less(ball.Shape.AsDynamicShape().Velocity.Y, 0.0f);

    }

    [Test]
    public void BallHitsFromLeft_BouncesX() {
        var ball = new Ball(
            new DynamicShape(new Vector2(0.4f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );
        ball.balllaunch();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(0.01f, 0.0f);

        var block = new Breakout.Blocks.NormalBlock(
            new StationaryShape(0.44f, 0.5f, 0.08f, 0.05f),
            new NoImage()
        );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsStationaryShape()).Collision) {
                BlockCollision.HandleKollisionWithBlock(ball, block);
                break;
            }
        }

        Assert.Less(ball.Shape.AsDynamicShape().Velocity.X, 0.0f);
    }
    [Test]
    public void BallHitsFromRight_BouncesX() {
        var ball = new Ball(
            new DynamicShape(new Vector2(0.6f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );
        ball.balllaunch();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(-0.01f, 0.0f);

        var block = new Breakout.Blocks.NormalBlock(
            new StationaryShape(0.50f, 0.5f, 0.08f, 0.05f),
            new NoImage()
        );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsStationaryShape()).Collision) {
                BlockCollision.HandleKollisionWithBlock(ball, block);
                break;
            }
        }

        Assert.Greater(ball.Shape.AsDynamicShape().Velocity.X, 0.0f);
    }
    [Test]

    public void BallHitsFromCorner_BouncesXandY() {
        var ball = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );
        ball.balllaunch();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(0.01f, 0.01f);

        var block = new Breakout.Blocks.NormalBlock(
            new StationaryShape(0.54f, 0.54f, 0.05f, 0.05f),
            new NoImage()
        );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsStationaryShape()).Collision) {
                BlockCollision.HandleKollisionWithBlock(ball, block);
                break;
            }
        }

        Assert.Less(ball.Shape.AsDynamicShape().Velocity.X, 0.0f);
        Assert.Less(ball.Shape.AsDynamicShape().Velocity.Y, 0.0f);
    }
    [Test]
    public void BallHitsFromCorner_BouncesXandY2() {
        var ball = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );
        ball.balllaunch();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(-0.01f, -0.01f);

        var block = new Breakout.Blocks.NormalBlock(
            new StationaryShape(0.44f, 0.44f, 0.05f, 0.05f),
            new NoImage()
        );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsStationaryShape()).Collision) {
                BlockCollision.HandleKollisionWithBlock(ball, block);
                break;
            }
        }

        Assert.Greater(ball.Shape.AsDynamicShape().Velocity.X, 0.0f);
        Assert.Greater(ball.Shape.AsDynamicShape().Velocity.Y, 0.0f);
    }
    [Test]
    public void BallHitsFromCorner_BouncesXandY3() {
        var ball = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );
        ball.balllaunch();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(0.01f, -0.01f);

        var block = new Breakout.Blocks.NormalBlock(
            new StationaryShape(0.53f, 0.44f, 0.05f, 0.05f),
            new NoImage()
        );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsStationaryShape()).Collision) {
                BlockCollision.HandleKollisionWithBlock(ball, block);
                break;
            }
        }

        Assert.Less(ball.Shape.AsDynamicShape().Velocity.X, 0.0f);
        Assert.Greater(ball.Shape.AsDynamicShape().Velocity.Y, 0.0f);
    }



}