namespace Breakout;

using System;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;


public class HealthBoard {

    public static int Lives { get; private set; } = 3;
    private EntityContainer<Entity> hearts;
    private Image heartImage;

    public HealthBoard() {
        hearts = new EntityContainer<Entity>();
        heartImage = new Image("Breakout.Assets.Images.heart_filled.png");

        UpdateHearts();
    }

    public void LoseHealth() {
        Lives--;
    }
    public void GainHealth() {
        Lives++;
    }

    public void UpdateHearts() {
        hearts.ClearContainer();

        for (int i = 0; i < Lives; i++) {
            var shape = new StationaryShape(
                new Vector2(0.01f + i * 0.04f, 0.96f),
                new Vector2(0.03f, 0.03f));

            var heart = new Entity(shape, heartImage);
            hearts.AddEntity(heart);
        }
    }

    public static void ResetLives() {
        Lives = 3;
    }

    public EntityContainer<Entity> DisplayHearts() {
        return hearts;
    }
}
