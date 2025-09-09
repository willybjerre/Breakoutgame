namespace BreakoutTests;

using System.Numerics;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using NUnit.Framework;




public class HealthBoardTest {
    private Breakout.HealthBoard healthBoard = null!;


    [SetUp]
    public void Setup() {
        HealthBoard.ResetLives();
        healthBoard = new HealthBoard();
    }

    [Test]
    public void HealthBoardStartsWithThreeLives() {
        Assert.That(HealthBoard.Lives, Is.EqualTo(3));
    }

    [Test]
    public void LoseHealthReducesLives() {
        healthBoard.LoseHealth();
        Assert.That(HealthBoard.Lives, Is.EqualTo(2));
    }

    [Test]
    public void ResetLivesSetsBackToThree() {
        healthBoard.LoseHealth();
        healthBoard.LoseHealth();
        Assert.That(HealthBoard.Lives, Is.EqualTo(1));

        HealthBoard.ResetLives();
        Assert.That(HealthBoard.Lives, Is.EqualTo(3));
    }

    [Test]
    public void DisplayHeartsReturnsCorrectCount() {
        Assert.That(healthBoard.DisplayHearts().CountEntities(), Is.EqualTo(3));

        healthBoard.LoseHealth();
        healthBoard.UpdateHearts();
        Assert.That(healthBoard.DisplayHearts().CountEntities(), Is.EqualTo(2));
    }
}