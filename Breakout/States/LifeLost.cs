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





public class LifeLost : IGameState {

    private Text lifeLeft;
    private StateMachine stateMachine;
    private Stopwatch stopwatch;



    public LifeLost(StateMachine stateMachine) {
        this.stateMachine = stateMachine;

        lifeLeft = new Text($"You Have {HealthBoard.Lives} Lives Left", new Vector2(0.2f, 0.5f), 0.5f);
        lifeLeft.SetColor(255, 30, 255);
        stopwatch = new Stopwatch();
        stopwatch.Start();

    }




    public void Render(WindowContext context) {
        lifeLeft.Render(context);
    }

    public void Update() {
        LifeChecker();
    }

    public void LifeChecker() {
        if (stopwatch.Elapsed.TotalSeconds > 2.0) {
            if (HealthBoard.Lives > 0) {
                stateMachine.SwitchState(GameState.GameRunning);
            } else {
                stateMachine.SwitchState(GameState.GameLost);
                ScoreBoard.ResetScore();
                HealthBoard.ResetLives();
            }
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
    }

}

