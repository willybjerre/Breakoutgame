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


public class PlayerCollision {



    public static void HandleKollisionWithPlayer(Ball ball, Player player) {

        float ballCenterX = ball.Shape.AsDynamicShape().Position.X + ball.Shape.AsDynamicShape().Extent.X / 2;
        float playerCenterX = player.Shape.AsDynamicShape().Position.X + player.Shape.AsDynamicShape().Extent.X / 2;


        float playerWidth = player.Shape.AsDynamicShape().Extent.X;
        float hitPosition = (ballCenterX - playerCenterX) / (playerWidth / 2);


        hitPosition = MathF.Max(-1.0f, MathF.Min(1.0f, hitPosition));


        float maxAngle = MathF.PI / 4;


        float bounceAngle = hitPosition * maxAngle;


        float speed = ball.Shape.AsDynamicShape().Velocity.Length();
        ball.Shape.AsDynamicShape().Velocity = new Vector2(
            speed * MathF.Sin(bounceAngle),
            speed * MathF.Cos(bounceAngle)
        );
    }
}