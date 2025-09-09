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


public class CollisionPlayerTest {

    public Breakout.Ball ball1;

    public Breakout.Player player1;




    [SetUp]
    public void Setup() {
        ball1 = new Ball(
             new DynamicShape(new Vector2(0.02f, 0.5f), new Vector2(0.02f, 0.02f)),
             new NoImage());
        player1 = new Player(
           new DynamicShape(new Vector2(0.5f, 0.2f), new Vector2(0.1f, 0.02f)),
           new NoImage());



    }

    [Test]
    public void collisionwithplayer() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.3f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.00f, -0.02f);


        for (int i = 0; i < 14; i++) {
            ball1.Move();
            PlayerCollision.HandleKollisionWithPlayer(ball1, player1);
        }

        Assert.True(ball1.Shape.AsDynamicShape().Velocity.Y > 0.0f);
    }

    public void nocollisionwithplayer() {
        var ball1 = new Ball(
            new DynamicShape(new Vector2(0.5f, 0.3f), new Vector2(0.02f, 0.02f)),
            new NoImage()
        );

        ball1.balllaunch();
        ball1.Shape.AsDynamicShape().Velocity = new Vector2(0.00f, -0.02f);
        for (int i = 0; i < 14; i++) {
            ball1.Move();
            PlayerCollision.HandleKollisionWithPlayer(ball1, player1);
        }
        Assert.True(ball1.Shape.AsDynamicShape().Velocity.Y < 0.0f);
    }

}