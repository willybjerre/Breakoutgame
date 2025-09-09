namespace Breakout.Blocks;


using System;
using System.Collections.Generic;
using System.Numerics;
using Breakout.Effects;
using Breakout.GameEvents;
using Breakout.States;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;


public class PowerUp : Block {
    Random rand = new Random();

    public PowerUp(StationaryShape shape, IBaseImage image) : base(shape, image) {
        value = 10;
    }

    public override void TakeHit() {
        health -= 1;
        if (health <= 0) {
            DeleteEntity();

            int Value = rand.Next(1, 8);
            string effectType =
            (Value == 1) ? "DoubleBall" :
            (Value == 2) ? "SlowSpeed" :
            (Value == 3) ? "ExtraLife" :
            (Value == 4) ? "LoseLife" :
            (Value == 5) ? "DoubleBSpeed" :
            (Value == 6) ? "QuadBall" :
            (Value == 7) ? "UpgradeSpeed" : "";
            BreakoutBus.Instance.RegisterEvent(new AddEffectEvent(effectType, Shape.Position));
        }
    }
}