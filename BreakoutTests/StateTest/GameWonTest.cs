namespace BreakoutTests.StateTest;

using Breakout;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using NUnit.Framework;



public class GameWonTest {
    private StateMachine stateMachine = null!;
    private GameWon gameWon = null!;

    [SetUp]
    public void Setup() {
        stateMachine = new StateMachine();
        gameWon = new GameWon(stateMachine);
    }

    [Test]
    public void CanNavigateUpAndDown() {
        gameWon.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gameWon.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);

        Assert.Pass("Handled Up/Down input without error");
    }

    [Test]
    public void SelectMainMenuFromGameWon() {
        gameWon.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }


    [Test]
    public void CanNavigateUpToWrapAround() {
        gameWon.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameWon.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameWon.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }
}