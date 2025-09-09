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



public abstract class Effect : Entity {

    private const float MoveSpeed = 0.0035f;


    public Effect(DynamicShape shape, IBaseImage image) : base(shape, image) {
        Shape.AsDynamicShape().Velocity.Y = MoveSpeed;

    }

    public void Move() {

        float newX = Shape.AsDynamicShape().Position.X;
        float newY = Shape.AsDynamicShape().Position.Y - MoveSpeed;

        Shape.AsDynamicShape().Position = new Vector2(newX, newY);

    }


    public virtual void GivePlayerEffect(Player player) {

    }

    public virtual void GiveBallEffect(EntityContainer<Ball> PlayerBalls) {


    }
    public virtual void GiveHealthEffect(HealthBoard healthBoard) {

    }

    public void DeleteEffect() {
        this.DeleteEntity();
    }





}