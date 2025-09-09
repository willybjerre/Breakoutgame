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



public class QuadBall : Effect {



    public QuadBall(DynamicShape shape, IBaseImage image) : base(shape, image) {

    }

    public override void GivePlayerEffect(Player player) {
    }

    public override void GiveBallEffect(EntityContainer<Ball> PlayerBalls) {
        PlayerBalls.Iterate(ball => {
            Ball Newball = new Ball(
                    new DynamicShape(ball.Shape.AsDynamicShape().Position, new Vector2(0.03f, 0.03f)),
                    new Image("Breakout.Assets.Images.ball.png"));
            Newball.balllaunch();
            PlayerBalls.AddEntity(Newball);
        });
        PlayerBalls.Iterate(ball => {
            Ball Newball = new Ball(
                    new DynamicShape(ball.Shape.AsDynamicShape().Position, new Vector2(0.03f, 0.03f)),
                    new Image("Breakout.Assets.Images.ball.png"));
            Newball.balllaunch();
            PlayerBalls.AddEntity(Newball);
        });

    }

}