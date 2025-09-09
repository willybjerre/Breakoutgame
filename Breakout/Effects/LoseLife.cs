namespace Breakout.Effects;

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



public class LoseLife : Effect {



    public LoseLife(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    public override void GivePlayerEffect(Player player) {
    }

    public override void GiveBallEffect(EntityContainer<Ball> PlayerBalls) {
    }

    public override void GiveHealthEffect(HealthBoard healthBoard) {
        healthBoard.LoseHealth();
    }

}