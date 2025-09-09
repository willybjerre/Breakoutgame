namespace Breakout;

using System;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;


public class ScoreBoard {

    public static int Score { get; private set; } = 0;


    public void AddScore(Blocks.Block block) {
        if (block.IsDeleted()) {
            Score += block.Value;
        }

    }

    public Text[] DisplayScore() {
        Text[] ShowScore = new Text[] { new Text($"Score: {Score}", new Vector2(0.8f, 0.98f), 0.25f) };
        return ShowScore;
    }

    public static void ResetScore() {
        Score = 0;
    }

}

