namespace BreakoutTests.StateTest;

using Breakout;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using NUnit.Framework;

public class GamePausedTest {
    private StateMachine stateMachine = null!;
    private GamePaused gamePaused = null!;

    [SetUp]
    public void Setup() {
        stateMachine = new StateMachine();
        gamePaused = new GamePaused(stateMachine);

    }

    [Test]
    public void CanNavigateUpAndDown() {
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);

        Assert.Pass("Handled Up/Down input without error");
    }


    [Test]
    public void TestContinueOptionSwitchesToGameRunning() {
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);

        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestBackToMenuSwitchesToMainMenu() {
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);

        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestQuitSwitchesToMainMenu() {
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }
    // [Test]
    // public void FromGamerunningSwichToGamePaused() {
    //     stateMachine.SwitchState(GameState.GameRunning);
    //     gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);

    //     Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    // }
}