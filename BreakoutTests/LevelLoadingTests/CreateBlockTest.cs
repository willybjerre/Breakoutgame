namespace BreakoutTests.LevelLoadingTests;

using System.Collections.Generic;
using System.Numerics;
using Breakout;
using Breakout.Blocks;
using Breakout.LevelHandler;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using NUnit.Framework;

public class CreateblocksTest {


    private EntityContainer<Breakout.Blocks.Block> EmptyContainer = new EntityContainer<Breakout.Blocks.Block>();


    private List<string> lvl1map;

    private List<string> lvl1legend;

    private List<string> lvl1meta;

    private string lvl1;

    private StationaryShape shape;

    private Image image = new Image("BreakoutTests.Assets.Images.purple-block.png");




    [SetUp]
    public void Setup() {


        lvl1 = LevelLoading.LevelStringLoader("BreakoutTests.Assets.Levels.level1.txt");
        lvl1map = LevelLoading.StringToMap(lvl1);
        lvl1legend = LevelLoading.StringToLegend(lvl1);
        lvl1meta = LevelLoading.StringToMeta(lvl1);

        shape = new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f);






    }



    //MatchMetaTest

    [Test]
    public void MatchMetaTestReturnCorrectBlocks() {
        List<string> meta = new List<string> { "Hardened: a", "Unbreakable: b", "PowerUp: c" };

        var block1 = CreateLevels.MatchMeta(meta, 'a', shape, image);
        var block2 = CreateLevels.MatchMeta(meta, 'b', shape, image);
        var block3 = CreateLevels.MatchMeta(meta, 'c', shape, image);


        Assert.IsInstanceOf<Breakout.Blocks.Hardened>(block1);
        Assert.IsInstanceOf<Breakout.Blocks.Unbreakable>(block2);
        Assert.IsInstanceOf<Breakout.Blocks.PowerUp>(block3);
    }

    [Test]
    public void MatchMetaTestReturnsNormalBlock() {
        List<string> meta = new List<string> { "Hardened: a", "Unbreakable: b", "PowerUp: c" };

        var block = CreateLevels.MatchMeta(meta, 'd', shape, image);


        Assert.IsInstanceOf<Breakout.Blocks.Block>(block);
    }



    [Test]
    public void Test_CreateBlocks_InvalidLegendFormat_SkipsBlockCreation() {

        string level =
            @"Map:
            --a---
            Map/
            Meta:
            Hardened: y
            Meta/
            Legend:
            
            Legend/";



        Assert.Throws<System.IO.FileNotFoundException>(() => CreateLevels.Createblocks(level));

    }

    //MatchLegendTest
    [Test]
    public void Test_MatchLegend_ReturnsCorrectImage() {

        List<string> legend = new List<string> {
            "a) Breakout.Assets.Images.blue-block.png",
            "b) Breakout.Assets.Images.grey-block.png",
            "c) Breakout.Assets.Images.orange-block.png"
        };
        char symbol = 'a';

        var expectedImage = new Image("BreakoutTests.Assets.Images.blue-block.png");


        var result = CreateLevels.MatchLegend(legend, symbol);


        Assert.IsNotNull(result);
        Assert.That(result.ToString(), Is.EqualTo(expectedImage.ToString()));
    }

    [Test]

    public void Test_MatchLegend_ReturnsNullForInvalidSymbol() {

        List<string> legend = new List<string> {
            "a) Breakout.Assets.Images.blue-block.png",
            "b) Breakout.Assets.Images.grey-block.png",
            "c) Breakout.Assets.Images.orange-block.png"
        };
        char symbol = 'z';


        var result = CreateLevels.MatchLegend(legend, symbol);


        Assert.IsNull(result);
    }

    public void MatchLegend_FileNotFound_ThrowsFileNotFoundException() {
        List<string> legend = new List<string> {
            "a) Breakout.Assets.Images.nonexistent.png"
        };

        Assert.Throws<System.IO.FileNotFoundException>(() => CreateLevels.MatchLegend(legend, 'a'));
    }



}