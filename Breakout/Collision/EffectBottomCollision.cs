namespace Breakout.Collision;

using System.Collections.Generic;
using System.Linq.Expressions;
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
using LevelHandler;


public class EffectBottomCollision {



    public static void HandleKollisionWithBottom(Effects.Effect effect) {

        if (effect.Shape.Position.Y < -0.03f) {
            effect.DeleteEffect();
        }
    }
}