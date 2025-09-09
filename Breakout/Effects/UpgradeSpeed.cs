namespace Breakout.Effects;

using System.Collections.Generic;
using System.Numerics;
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



public class UpgradeSpeed : Effect {



    public UpgradeSpeed(DynamicShape shape, IBaseImage image) : base(shape, image) {

    }

    public override void GivePlayerEffect(Player player) {
        player.ChangeSpeed(0.018f);

        BreakoutBus.Instance.RegisterTimedEvent(
            new ResetPlayerSpeedEvent(),
            TimePeriod.NewSeconds(4.0)
        );
    }

    public override void GiveBallEffect(EntityContainer<Ball> PlayerBalls) {

    }

}