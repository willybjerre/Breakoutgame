namespace Breakout;

using System;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;


public class Player : Entity {

    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private float MOVEMENT_SPEED = 0.015f;

    private const float SPEED = 0.015f;


    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    public void Move() {

        float newX = Shape.AsDynamicShape().Velocity.X + Shape.AsDynamicShape().Position.X;

        if (newX > 0.0 && newX < 1.0 - Shape.AsDynamicShape().Extent.X) {

            Shape.AsDynamicShape().Position = new Vector2(newX, Shape.AsDynamicShape().Position.Y);
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

    public void ChangeSpeed(float speed) {
        MOVEMENT_SPEED = speed;
    }
    public void ResetSpeed() {
        if (MOVEMENT_SPEED != SPEED) {
            MOVEMENT_SPEED = SPEED;
        }
    }

    private void UpdateVelocity() {



        Shape.AsDynamicShape().Velocity.X = moveLeft + moveRight;



    }

    public Vector2 GetPosition() {
        return new Vector2(
            Shape.AsDynamicShape().Position.X + Shape.AsDynamicShape().Extent.X / 2,
            Shape.AsDynamicShape().Position.Y + Shape.AsDynamicShape().Extent.Y / 4);

    }

    public void HandleKeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                SetMoveLeft(true);
                break;

            case KeyboardKey.Right:
            case KeyboardKey.D:
                SetMoveRight(true);
                break;
        }
    }

    public void HandleKeyRelease(KeyboardKey key) {
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

    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            HandleKeyPress(key);
        } else if (action == KeyboardAction.KeyRelease) {
            HandleKeyRelease(key);
        }
    }

    public float GetSpeed() {
        return MOVEMENT_SPEED;
    }

}

