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
public class GamePaused : IGameState {
    private Text[] PausedOptions;
    private Text pause;
    private int activePauseOption = 0;
    private StateMachine stateMachine;



    public GamePaused(StateMachine stateMachine) {
        this.stateMachine = stateMachine;

        pause = new Text("Paused", new Vector2(0.35f, 0.8f), 0.8f);
        pause.SetColor(255, 30, 255);



        PausedOptions = new Text[] {
            new Text("Continue", new Vector2(0.4f, 0.5f), 0.4f),
            new Text("Back To Menu", new Vector2(0.35f, 0.4f), 0.4f)
        };

        PausedOptions[0].SetColor(255, 0, 255);
    }




    public void Render(WindowContext context) {
        pause.Render(context);

        foreach (var option in PausedOptions) {
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

        PausedOptions[activePauseOption].SetColor(255, 255, 255);

        activePauseOption = (activePauseOption + direction + PausedOptions.Length) % PausedOptions.Length;

        PausedOptions[activePauseOption].SetColor(255, 0, 255);
    }

    private void SelectOption() {
        if (activePauseOption == 0) {
            stateMachine.SwitchState(GameState.GameRunning);
        } else if (activePauseOption == 1) {
            stateMachine.SwitchState(GameState.MainMenu);
        }
    }

}

