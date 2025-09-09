namespace BreakoutTests;

using System.Numerics;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using NUnit.Framework;

public class TestsPlayer {
    public Player testPlayer;

    public Player testPlayer1;

    public Player testPlayer2;

    [SetUp]
    public void Setup() {

        testPlayer = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f), new Vector2(0.1f, 0.1f)),
            new NoImage());

        testPlayer1 = new Player(
            new DynamicShape(new Vector2(1.0f, 0.1f), new Vector2(0.1f, 0.1f)),
            new NoImage());

        testPlayer2 = new Player(
            new DynamicShape(new Vector2(0.0f, 0.1f), new Vector2(0.1f, 0.1f)),
            new NoImage());

    }

    [Test]
    public void MoveLeftTest() {
        float StartPos = testPlayer.Shape.Position.X;
        testPlayer.SetMoveLeft(true);
        testPlayer.Move();
        testPlayer.SetMoveLeft(false);
        Assert.True(StartPos > testPlayer.Shape.Position.X);
    }

    [Test]
    public void MoveRightTest() {
        float StartPos = testPlayer.Shape.Position.X;
        testPlayer.SetMoveRight(true);
        testPlayer.Move();
        testPlayer.SetMoveRight(false);
        Assert.True(StartPos < testPlayer.Shape.Position.X);
    }

    [Test]
    public void RightBoundaryTest() {
        float StartPos = testPlayer1.Shape.Position.X;
        testPlayer1.SetMoveRight(true);
        testPlayer1.Move();
        testPlayer1.SetMoveRight(false);
        Assert.True(StartPos == testPlayer1.Shape.Position.X);
    }

    [Test]
    public void LeftBoundaryTest() {
        float StartPos = testPlayer2.Shape.Position.X;
        testPlayer2.SetMoveLeft(true);
        testPlayer2.Move();
        testPlayer2.SetMoveLeft(false);
        Assert.True(StartPos == testPlayer2.Shape.Position.X);
    }
    [Test]
    public void UpdateVelocityLeftTest() {
        float StartVelocity = testPlayer.Shape.AsDynamicShape().Velocity.X;
        testPlayer.SetMoveLeft(true);

        Assert.True(StartVelocity > testPlayer.Shape.AsDynamicShape().Velocity.X);
    }

    [Test]
    public void UpdateVelocityRightTest() {
        float StartVelocity = testPlayer.Shape.AsDynamicShape().Velocity.X;
        testPlayer.SetMoveRight(true);

        Assert.True(StartVelocity < testPlayer.Shape.AsDynamicShape().Velocity.X);
    }
}