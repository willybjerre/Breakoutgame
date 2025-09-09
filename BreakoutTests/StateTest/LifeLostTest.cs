namespace BreakoutTests.StateTest;

using Breakout;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using NUnit.Framework;



public class LifeLostTest {
    private StateMachine stateMachine = null!;
    private LifeLost lifeLost = null!;
    private HealthBoard healthBoard = null!;

    [SetUp]
    public void Setup() {
        healthBoard = new HealthBoard();
        HealthBoard.ResetLives();
        ScoreBoard.ResetScore();
        stateMachine = new StateMachine();
        lifeLost = new LifeLost(stateMachine);
    }

    [Test]
    public void LifeChecker_SwitchesToGameRunning_WhenLivesLeft() {
        Thread.Sleep(2100);

        lifeLost.Update();

        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void LifeChecker_SwitchesToGameLost_WhenNoLives() {
        healthBoard.LoseHealth();
        healthBoard.LoseHealth();
        healthBoard.LoseHealth();

        lifeLost = new LifeLost(stateMachine);
        Thread.Sleep(2100);

        lifeLost.Update();

        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }
}