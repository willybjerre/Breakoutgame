namespace BreakoutTests.StateTest;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using Breakout;
using Breakout.Collision;
using Breakout.Effects;
using Breakout.GameEvents;
using Breakout.LevelHandler;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

public class GameRunningTest {
    private StateMachine stateMachine = null!;
    private GameRunning gameRunning = null!;


    [SetUp]
    public void Setup() {
        stateMachine = new StateMachine();
        gameRunning = new GameRunning(stateMachine);
    }
    [Test]
    public void GameRunning_InitializesCorrectly() {
        Assert.That(GameRunning.Instance, Is.EqualTo(gameRunning));
    }

    [Test]
    public void GameRunning_HandlesKeyEvents() {
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Left);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }

    [Test]
    public void GameRunning_HasOnePlayerBallAtStart() {
        Assert.That(gameRunning.GetBallCount(), Is.EqualTo(1));
    }

    [Test]
    public void PlayerPositionIsCorrect() {
        var pos = gameRunning.GetPlayer().GetPosition();
        Assert.That(pos.X, Is.EqualTo(0.4875f).Within(0.001f));
        Assert.That(pos.Y, Is.EqualTo(0.0875f).Within(0.001f));
    }
    [Test]
    public void ScoreBoardIsInitialized() {
        Assert.That(gameRunning.GetScoreBoard(), Is.Not.Null);
    }

    [Test]
    public void HealthBoardIsInitialized() {
        Assert.That(gameRunning.GetHealthBoard(), Is.Not.Null);
    }
    [Test]
    public void Update_CallsPlayerMove() {
        var oldPos = gameRunning.GetPlayer().GetPosition();
        gameRunning.GetPlayer().SetMoveRight(true);
        gameRunning.Update();
        var newPos = gameRunning.GetPlayer().GetPosition();

        Assert.That(newPos.X, Is.GreaterThan(oldPos.X));
    }
    [Test]
    public void NoBalls_TriggersLifeLostState() {
        gameRunning.ClearBalls();
        gameRunning.Update();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<LifeLost>());
    }

    [Test]
    public void BallHitsBlock_IncreasesScore() {
        var block = new Breakout.Blocks.NormalBlock(
            new DIKUArcade.Entities.StationaryShape(0.4875f, 0.12f, 0.05f, 0.05f),
            new DIKUArcade.Graphics.NoImage()
        );

        gameRunning.GetBreakableBlocks().AddEntity(block);

        var ball = gameRunning.GetFirstBall();
        ball.Shape.AsDynamicShape().Position = new Vector2(0.4875f, 0.18f);
        ball.Shape.AsDynamicShape().Velocity = new Vector2(0.0f, -0.01f);

        int scoreBefore = ScoreBoard.Score;

        for (int i = 0; i < 10; i++) {
            gameRunning.Update();
        }

        Assert.That(ScoreBoard.Score, Is.GreaterThan(scoreBefore));
    }

    [Test]
    public void EffectIsRemoved_WhenBelowPlayer() {
        var effect = new DoubleBall(
            new DynamicShape(new Vector2(0.45f, 0.15f), new Vector2(0.03f, 0.03f)),
            new NoImage()
        );

        effect.Shape.AsDynamicShape().Velocity = new Vector2(0.0f, -0.03f);

        gameRunning.GetEffects().AddEntity(effect);

        int initialCount = gameRunning.GetEffects().CountEntities();

        for (int i = 0; i < 70; i++) {
            gameRunning.Update();
        }

        int finalCount = gameRunning.GetEffects().CountEntities();
        Assert.That(finalCount, Is.LessThan(initialCount));
    }


    [Test]
    public void LevelChecker_LoadsNextLevel_WhenNoBreakableBlocks() {
        gameRunning.GetBreakableBlocks().ClearContainer();
        gameRunning.Update();
        Assert.That(gameRunning.GetActiveLevel(), Is.EqualTo(1));
    }

    [Test]
    public void LoadLevel_AddsBlocks() {
        Assert.That(gameRunning.GetBreakableBlocks().CountEntities(), Is.GreaterThan(0));
    }

    [Test]
    public void AddEffect_AddsCorrectEffectTypeDoubleBall() {
        var effectEvent = new AddEffectEvent("DoubleBall", new Vector2(0.3f, 0.3f));

        int before = gameRunning.GetEffects().CountEntities();
        gameRunning.AddEffect(effectEvent);
        int after = gameRunning.GetEffects().CountEntities();

        Assert.That(after, Is.EqualTo(before + 1));
    }
    [Test]
    public void AddEffect_AddsCorrectEffectTypeSlowSpeed() {
        var effectEvent = new AddEffectEvent("SlowSpeed", new Vector2(0.3f, 0.3f));

        int before = gameRunning.GetEffects().CountEntities();
        gameRunning.AddEffect(effectEvent);
        int after = gameRunning.GetEffects().CountEntities();

        Assert.That(after, Is.EqualTo(before + 1));
    }

    [Test]
    public void AddEffect_IgnoresUnknownEffectType() {
        var effectEvent = new AddEffectEvent("UnknownPower", new Vector2(0.3f, 0.3f));

        int before = gameRunning.GetEffects().CountEntities();
        gameRunning.AddEffect(effectEvent);
        int after = gameRunning.GetEffects().CountEntities();

        Assert.That(after, Is.EqualTo(before));
    }
    [Test]
    public void ResetSpeed_ResetsPlayerSpeed() {
        gameRunning.GetPlayer().SetMoveRight(true);
        gameRunning.GetPlayer().Move();
        var movedPos = gameRunning.GetPlayer().GetPosition().X;

        gameRunning.ResetPlayerSpeed(new ResetPlayerSpeedEvent());
        gameRunning.GetPlayer().Move();


        var resetPos = gameRunning.GetPlayer().GetPosition().X;
        Assert.That(resetPos, Is.EqualTo(movedPos).Within(0.1f));
    }

    [Test]
    public void StartLevel_ResetsBallsAndEffects() {
        gameRunning.GetEffects().AddEntity(new DoubleBall(
            new DynamicShape(new Vector2(0.2f, 0.2f), new Vector2(0.03f, 0.03f)),
            new NoImage()));

        gameRunning.ClearBalls();

        gameRunning.StartLevel();

        Assert.That(gameRunning.GetBallCount(), Is.EqualTo(1));
        Assert.That(gameRunning.GetEffects().CountEntities(), Is.EqualTo(0));
    }


}






