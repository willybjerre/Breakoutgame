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



public class DoubleBSpeed : Effect {



    public DoubleBSpeed(DynamicShape shape, IBaseImage image) : base(shape, image) {

    }

    public override void GivePlayerEffect(Player player) {
    }

    public override void GiveBallEffect(EntityContainer<Ball> PlayerBalls) {
        PlayerBalls.Iterate(ball => {
            float PrevVelocityX = ball.Shape.AsDynamicShape().Velocity.X;
            float PrevVelocityY = ball.Shape.AsDynamicShape().Velocity.Y;

            ball.Shape.AsDynamicShape().Velocity.X = PrevVelocityX * 2;
            ball.Shape.AsDynamicShape().Velocity.Y = PrevVelocityY * 2;

        });
        BreakoutBus.Instance.RegisterTimedEvent(
        new ResetBallSpeedEvent(),
        TimePeriod.NewSeconds(4.0)
    );
    }

}