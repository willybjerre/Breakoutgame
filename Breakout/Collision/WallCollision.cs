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


public class WallCollision {



    public static void HandleKollisionWithWall(Ball ball) {

        if (ball.Shape.Position.Y < -0.03f) {
            ball.DeleteEntity();
        } else if (ball.Shape.Position.Y > 1.015f - ball.Shape.Extent.Y && ball.Shape.Position.X > 1.015f - ball.Shape.Extent.X) {
            ball.BallBounceX();
            ball.BallBounceY();
        } else if (ball.Shape.Position.Y > 1.015f - ball.Shape.Extent.Y) {
            ball.BallBounceY();
        } else if (ball.Shape.Position.X < -0.015f || ball.Shape.Position.X > 1.015f - ball.Shape.Extent.X) {
            ball.BallBounceX();
        }
    }
}