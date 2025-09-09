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


public class BlockCollision {

    public static void HandleKollisionWithBlock(Ball ball, Blocks.Block block) {
        var ballShape = ball.Shape.AsDynamicShape();
        var blockShape = block.Shape.AsStationaryShape();

        var ballPos = ballShape.Position;
        var ballExtent = ballShape.Extent;
        var blockPos = blockShape.Position;
        var blockExtent = blockShape.Extent;

        // Beregn overlap i X og Y
        float overlapX = Math.Min(
            ballPos.X + ballExtent.X - blockPos.X,
            blockPos.X + blockExtent.X - ballPos.X
        );
        float overlapY = Math.Min(
            ballPos.Y + ballExtent.Y - blockPos.Y,
            blockPos.Y + blockExtent.Y - ballPos.Y
        );

        if (overlapX < overlapY) {
            // Ramte fra siden
            ball.Shape.AsDynamicShape().Velocity.X *= -1;
        } else if (overlapX == overlapY) {
            // Ramte i hjørnet
            ball.Shape.AsDynamicShape().Velocity.X *= -1;
            ball.Shape.AsDynamicShape().Velocity.Y *= -1;
        } else {
            // Ramte fra top eller bund
            ball.Shape.AsDynamicShape().Velocity.Y *= -1;
        }

        block.TakeHit();
    }

}