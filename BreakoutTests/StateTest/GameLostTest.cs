namespace BreakoutTests.StateTest;

using Breakout;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using NUnit.Framework;


public class GameLostTest {
    private StateMachine stateMachine = null!;
    private GameLost gameLost = null!; [SetUp]

    public void Setup() {
        stateMachine = new StateMachine();
        gameLost = new GameLost(stateMachine);
    }

    [Test]
    public void TestNavigateUpAndDown() {

        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);

        Assert.Pass("Handled Up/Down input without error");

    }

    [Test]
    public void TestEnterStartsNewGame() {
        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestNavigateUpToWrapAround() {
        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameLost.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

}