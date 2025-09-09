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


public static class PlayerEffectCollision {

    public static void HandleKollisionWithEffect(Effects.Effect effect, Player player, EntityContainer<Ball> playerBalls, HealthBoard healthBoard) {
        var effectBox = effect.Shape.AsDynamicShape();
        var playerBox = player.Shape.AsDynamicShape();

        var adjustedPlayerBox = new DynamicShape(
            new Vector2(playerBox.Position.X, playerBox.Position.Y + 0.05f),
            playerBox.Extent
        );

        if (CollisionDetection.Aabb(effectBox, adjustedPlayerBox).Collision) {
            effect.GivePlayerEffect(player);
            effect.GiveBallEffect(playerBalls);
            effect.GiveHealthEffect(healthBoard);
            effect.DeleteEntity();
        }
    }
}