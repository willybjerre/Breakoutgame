namespace Breakout;

using System.Collections.Generic;
using System.Numerics;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

public class Game : DIKUGame {

    private StateMachine stateMachine;


    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
    }


    public override void Render(WindowContext context) {
        stateMachine.ActiveState.Render(context);
    }

    public override void Update() {
        stateMachine.ActiveState.Update();


    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {

        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }
}