namespace Breakout.States;

using System.Collections.Generic;
using System.Diagnostics;
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




public class GameWon : IGameState {

    private Image backgroundImage;
    private Text[] menuOptions;
    private int activeMenuOption = 0;
    private StateMachine stateMachine;
    // private Text gameWon;
    private Text scoreBoard;



    public GameWon(StateMachine stateMachine) {
        this.stateMachine = stateMachine;
        backgroundImage = new Image("Breakout.Assets.Images.Winning.png");

        // gameWon = new Text($"CONGRATULATIONS \n You Won!!!", new Vector2(0.07f, 0.65f), 1.4f);
        // gameover.SetColor(255, 0, 0);
        scoreBoard = new Text($"Score: {ScoreBoard.Score}", new Vector2(0.07f, 0.2f), 0.8f);
        scoreBoard.SetColor(255, 200, 255);

        menuOptions = new Text[] {
            new Text("Main Menu", new Vector2(0.4f, 0.5f), 0.4f),
            new Text("Quit", new Vector2(0.45f, 0.4f), 0.4f)
        };
        menuOptions[0].SetColor(255, 0, 255);
    }




    public void Render(WindowContext context) {
        backgroundImage.Render(context, new StationaryShape(Vector2.Zero, Vector2.One));
        // gameWon.Render(context);
        scoreBoard.Render(context);

        foreach (var option in menuOptions) {
            option.Render(context);
        }
    }

    public void Update() {
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
            stateMachine.SwitchState(GameState.MainMenu);
        } else if (activeMenuOption == 1) {
            System.Environment.Exit(0);
        }
    }

}

