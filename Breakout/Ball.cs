namespace Breakout;

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



public class Ball : Entity {
    public bool IsLaunched { get; private set; } = false;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.015f;
    Random rand = new Random();


    public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {

    }

    public void Move() {

        float newX = Shape.AsDynamicShape().Velocity.X + Shape.AsDynamicShape().Position.X;
        float newY = Shape.AsDynamicShape().Velocity.Y + Shape.AsDynamicShape().Position.Y;


        if (0.07f < newX && newX < 0.9f && !IsLaunched) {

            Shape.AsDynamicShape().Position = new Vector2(newX, newY);
        } else if (IsLaunched) {
            Shape.AsDynamicShape().Position = new Vector2(newX, newY);

        }
    }

    public void ResetBallSpeed() {

        float PrevVelocityX = Shape.AsDynamicShape().Velocity.X;
        float PrevVelocityY = Shape.AsDynamicShape().Velocity.Y;
        if (Shape.AsDynamicShape().Velocity.X > MOVEMENT_SPEED && Shape.AsDynamicShape().Velocity.Y > MOVEMENT_SPEED) {
            Shape.AsDynamicShape().Velocity.X = MOVEMENT_SPEED;
            Shape.AsDynamicShape().Velocity.Y = MOVEMENT_SPEED;
        }

    }


    public void SetMoveLeft(bool val) {

        moveLeft = val ? -MOVEMENT_SPEED : 0.0f;
        UpdateVelocity();
    }

    public void SetMoveRight(bool val) {

        moveRight = val ? MOVEMENT_SPEED : 0.0f;
        UpdateVelocity();

    }

    private void UpdateVelocity() {

        Shape.AsDynamicShape().Velocity.X = moveLeft + moveRight;

    }


    public void balllaunch() {
        IsLaunched = true;
        float randomFloat = (float) (rand.NextDouble() * 0.02 - 0.01);
        Shape.AsDynamicShape().Velocity = new Vector2(randomFloat, 0.017f);
    }

    public void BallBounceX() {
        Shape.AsDynamicShape().Velocity.X = Shape.AsDynamicShape().Velocity.X * -1.0f;
    }
    public void BallBounceY() {
        Shape.AsDynamicShape().Velocity.Y = Shape.AsDynamicShape().Velocity.Y * -1.0f;
    }

    public void HandleKeyPress(KeyboardKey key) {
        if (!IsLaunched) {
            switch (key) {
                case KeyboardKey.Left:
                case KeyboardKey.A:
                    SetMoveLeft(true);
                    break;

                case KeyboardKey.Right:
                case KeyboardKey.D:
                    SetMoveRight(true);
                    break;

                case KeyboardKey.Space:
                    balllaunch();
                    break;
            }
        }
    }

    public void HandleKeyRelease(KeyboardKey key) {
        if (!IsLaunched) {
            switch (key) {
                case KeyboardKey.Left:
                case KeyboardKey.A:
                    SetMoveLeft(false);
                    break;

                case KeyboardKey.Right:
                case KeyboardKey.D:
                    SetMoveRight(false);
                    break;
            }
        }
    }

    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (!IsLaunched) {
            if (action == KeyboardAction.KeyPress) {
                HandleKeyPress(key);
            } else if (action == KeyboardAction.KeyRelease) {
                HandleKeyRelease(key);
            }
        }
    }
}