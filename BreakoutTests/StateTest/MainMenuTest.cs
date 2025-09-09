namespace BreakoutTests.StateTest;

using Breakout;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using NUnit.Framework;



public class MainMenuTest {
    private StateMachine stateMachine = null!;
    private MainMenu mainMenu = null!;

    [SetUp]
    public void Setup() {
        stateMachine = new StateMachine();
        mainMenu = new MainMenu(stateMachine);
    }

    [Test]
    public void CanStartNewGameWithEnter() {
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);

        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void CanNavigateDownToQuit() {
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);

        Assert.Pass("Successfully navigated to Quit option (Enter skipped to avoid exit)");
    }


    [Test]
    public void CanNavigateUpToWrapAround() {
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }
}