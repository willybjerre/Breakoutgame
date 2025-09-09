namespace Breakout.States;

using System.Collections.Generic;
using System.Numerics;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;


public class MainMenu : IGameState {
    private Text[] menuOptions;

    private int activeMenuOption = 0;
    private StateMachine stateMachine;
    private Image backgroundImage;

    public MainMenu(StateMachine stateMachine) {
        this.stateMachine = stateMachine;
        backgroundImage = new Image("Breakout.Assets.Images.BreakoutTitleScreen.png");

        menuOptions = new Text[] {
            new Text("New Game", new Vector2(0.4f, 0.5f), 0.4f),
            new Text("Quit", new Vector2(0.45f, 0.4f), 0.4f)
        };

        menuOptions[0].SetColor(255, 0, 255);

    }

    public void Render(WindowContext context) {
        backgroundImage.Render(context, new StationaryShape(Vector2.Zero, Vector2.One));


        foreach (var option in menuOptions) {
            option.Render(context);
        }
    }

    public void Update() {
        HealthBoard.ResetLives();
        ScoreBoard.ResetScore();
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            switch (key) {
                case KeyboardKey.Up:
                    ChangeSelection(-1);
                    break;
                case KeyboardKey.Down:
                    ChangeSelection(1);
                    break;
                case KeyboardKey.Enter:
                    SelectOption();
                    break;
            }
        }
    }

    private void ChangeSelection(int direction) {
        menuOptions[activeMenuOption].SetColor(255, 255, 255);

        activeMenuOption = (activeMenuOption + direction + menuOptions.Length) % menuOptions.Length;

        menuOptions[activeMenuOption].SetColor(255, 0, 255);
    }

    private void SelectOption() {
        if (activeMenuOption == 0) {
            stateMachine.SwitchState(GameState.GameRunning);
        } else if (activeMenuOption == 1) {
            System.Environment.Exit(0);
        }
    }
}