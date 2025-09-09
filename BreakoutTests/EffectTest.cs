namespace BreakoutTests;

using System;
using System.Numerics;
using Breakout;
using Breakout.Effects;
using Breakout.States;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using NUnit.Framework;


public class EffectTests {
    private EntityContainer<Ball> balls;
    private Player player;

    [SetUp]
    public void Setup() {
        balls = new EntityContainer<Ball>();
        player = new Player(
            new DynamicShape(new Vector2(0.4f, 0.1f), new Vector2(0.1f, 0.03f)),
            new NoImage());
    }

    [Test]
    public void Effect_MovesDownward() {
        Effect effect = new DoubleBall(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        float before = effect.Shape.Position.Y;
        effect.Move();
        Assert.That(effect.Shape.Position.Y, Is.LessThan(before));
    }


    [Test]
    public void Effect_DeleteEffect_RemovesEffect() {
        Effect effect = new SlowSpeed(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        effect.DeleteEffect();

        Assert.That(effect.IsDeleted(), Is.True);
    }

    [Test]
    public void DoubleBall_AddsExtraBall() {
        balls.AddEntity(new Ball(
            new DynamicShape(new Vector2(0.3f, 0.3f), new Vector2(0.03f, 0.03f)),
            new NoImage()));

        Effect effect = new DoubleBall(
             new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
             new NoImage());

        int before = balls.CountEntities();

        effect.GiveBallEffect(balls);

        Assert.That(balls.CountEntities(), Is.EqualTo(before + 1));
    }

    [Test]
    public void SlowSpeed_LowersPlayerSpeed() {
        float before = player.GetSpeed();
        Effect effect = new SlowSpeed(
            new DynamicShape(new Vector2(0.4f, 0.4f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        effect.GivePlayerEffect(player);

        float after = player.GetSpeed();

        Assert.That(after, Is.LessThan(before));
    }

    [Test]
    public void ExtraLife_IncreasesPlayerLives() {
        HealthBoard healthBoard = new HealthBoard();
        Effect effect = new ExtraLife(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        int beforeLives = HealthBoard.Lives;

        effect.GiveHealthEffect(healthBoard);

        Assert.That(HealthBoard.Lives, Is.EqualTo(beforeLives + 1));
    }
    [Test]
    public void LoseLife_DecreasesPlayerLives() {
        HealthBoard healthBoard = new HealthBoard();
        Effect effect = new LoseLife(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        int beforeLives = HealthBoard.Lives;

        effect.GiveHealthEffect(healthBoard);

        Assert.That(HealthBoard.Lives, Is.EqualTo(beforeLives - 1));
    }
    [Test]
    public void DoubleBSpeed_DoublesBallSpeed() {
        EntityContainer<Ball> playerBalls = new EntityContainer<Ball>();
        playerBalls.AddEntity(new Ball(
            new DynamicShape(new Vector2(0.3f, 0.3f), new Vector2(0.03f, 0.03f)),
            new NoImage()));

        Effect effect = new DoubleBSpeed(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        float beforeSpeedX = 0;
        float beforeSpeedY = 0;
        float afterSpeedX = 0;
        float afterSpeedY = 0;
        playerBalls.Iterate(ball => {
            beforeSpeedX = ball.Shape.AsDynamicShape().Velocity.X;
            beforeSpeedY = ball.Shape.AsDynamicShape().Velocity.Y;
        });

        effect.GiveBallEffect(playerBalls);

        playerBalls.Iterate(ball => {
            afterSpeedX = ball.Shape.AsDynamicShape().Velocity.X;
            afterSpeedY = ball.Shape.AsDynamicShape().Velocity.Y;
        });

        Assert.That(afterSpeedX, Is.EqualTo(beforeSpeedX * 2));
        Assert.That(afterSpeedY, Is.EqualTo(beforeSpeedY * 2));
    }
    [Test]
    public void QuadBall_AddsThreeExtraBalls() {
        EntityContainer<Ball> playerBalls = new EntityContainer<Ball>();
        playerBalls.AddEntity(new Ball(
            new DynamicShape(new Vector2(0.3f, 0.3f), new Vector2(0.03f, 0.03f)),
            new NoImage()));

        Effect effect = new QuadBall(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        int beforeCount = playerBalls.CountEntities();

        effect.GiveBallEffect(playerBalls);

        Assert.That(playerBalls.CountEntities(), Is.EqualTo(beforeCount + 3));
    }
    [Test]
    public void UpgradeSpeed_IncreasesPlayerSpeed() {
        Player player = new Player(
            new DynamicShape(new Vector2(0.4f, 0.1f), new Vector2(0.1f, 0.03f)),
            new NoImage());

        float beforeSpeed = player.GetSpeed();
        Effect effect = new UpgradeSpeed(
            new DynamicShape(new Vector2(0.5f, 0.5f), new Vector2(0.03f, 0.03f)),
            new NoImage());

        effect.GivePlayerEffect(player);

        float afterSpeed = player.GetSpeed();

        Assert.That(afterSpeed, Is.GreaterThan(beforeSpeed));
    }
}