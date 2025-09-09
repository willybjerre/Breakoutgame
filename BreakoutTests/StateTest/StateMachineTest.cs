namespace BreakoutTests.StateTest;

using Breakout;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using NUnit.Framework;




public class StateMachineTest {
    private StateMachine stateMachine;

    private MainMenu mainMenu;

    [SetUp]
    public void InitiateStateMachine() {
        stateMachine = new StateMachine();
        mainMenu = new MainMenu(stateMachine);

    }

    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestSwitchToGameRunning() {
        stateMachine.SwitchState(GameState.GameRunning);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestSwitchToGamePaused() {
        stateMachine.SwitchState(GameState.GamePaused);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }

    [Test]
    public void TestSwitchToMainMenu() {
        stateMachine.SwitchState(GameState.MainMenu);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }



    // [Test]
    // public void TestSwitchToGamePausedFromGameRunning() {
    //     stateMachine.SwitchState(GameState.GameRunning);
    //     stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
    //     Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    // }

    // [Test]
    // public void TestSwitchToMainMenuFromGamePaused() {
    //     stateMachine.SwitchState(GameState.GamePaused);
    //     stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
    //     stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
    //     Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    // }

    // [Test]
    // public void TestSwitchToGameRunningFromGamePaused() {
    //     stateMachine.SwitchState(GameState.GamePaused);
    //     stateMachine.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
    //     Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    // }

    [Test]
    public void TestSwitchToGameLost() {
        stateMachine.SwitchState(GameState.GameLost);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }
    [Test]
    public void TestSwitchToGameWon() {
        stateMachine.SwitchState(GameState.GameWon);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameWon>());
    }
    [Test]
    public void TestSwitchToLifeLost() {
        stateMachine.SwitchState(GameState.LifeLost);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<LifeLost>());
    }
}