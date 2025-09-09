namespace BreakoutTests;

using System.Numerics;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using NUnit.Framework;

public class ScoreBoardTests {
    public ScoreBoard scoreBoard;

    public Breakout.Blocks.Block block;

    public int expectation;





    [SetUp]
    public void Setup() {
        scoreBoard = new ScoreBoard();

        block = new NormalBlock(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), new NoImage());

        expectation = ScoreBoard.Score;



    }

    [Test]

    public void AddScore_WhenBlock_Is_NotDeleted_Test() {
        var expectation = ScoreBoard.Score;
        scoreBoard.AddScore(block);
        var result = ScoreBoard.Score;
        Assert.That(expectation, Is.EqualTo(result));
    }

    [Test]

    public void AddScore_WhenBlock_Is_Deleted_Test() {
        var expectation = ScoreBoard.Score;
        block.DeleteEntity();
        scoreBoard.AddScore(block);
        var result = ScoreBoard.Score;
        Assert.That(expectation, Is.Not.EqualTo(result));
    }

    [Test]
    public void DisplayScore_CreatesTextObject() {
        var result = scoreBoard.DisplayScore();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.GreaterThan(0));
    }

    [Test]
    public void ResetScore_SetsScoreToZero() {
        ScoreBoard.ResetScore();
        Assert.That(ScoreBoard.Score, Is.EqualTo(0));
    }
}