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


public class LevelLoadingTest {

    private EntityContainer<Breakout.Blocks.Block> blocks = new EntityContainer<Breakout.Blocks.Block>();


    private List<string> lvl1map;

    private List<string> lvl1legend;

    private List<string> lvl1meta;

    private string lvl1;



    [SetUp]
    public void Setup() {


        lvl1 = LevelLoading.LevelStringLoader("BreakoutTests.Assets.Levels.level1.txt");
        lvl1map = LevelLoading.StringToMap(lvl1);
        lvl1legend = LevelLoading.StringToLegend(lvl1);
        lvl1meta = LevelLoading.StringToMeta(lvl1);




    }



    // Test for LevelStringLoader method
    [Test]

    public void LevelStringLoader_ValidPath_ReturnsCorrectString() {
        string expected = @"
Map:
------------
------------
-aaaaaaaaaa-
-aaaaaaaaaa-
-000----000-
-000-%%-000-
-000-11-000-
-000-%%-000-
-000----000-
-%%%%%%%%%%-
-%%%%%%%%%%-
------------
------------
------------
------------
------------
------------
------------
------------
------------
------------
------------
------------
------------
------------
Map/

Meta:
Name: LEVEL 1
Time: 300
Hardened: %
PowerUp: 1
Meta/

Legend:
%) Breakout.Assets.Images.blue-block.png
0) Breakout.Assets.Images.grey-block.png
1) Breakout.Assets.Images.orange-block.png
a) Breakout.Assets.Images.purple-block.png
Legend/";

        string actual = LevelLoading.LevelStringLoader("BreakoutTests.Assets.Levels.level1.txt");

        Assert.That(expected.Trim(), Is.EqualTo(actual.Trim()));
    }

    [Test]
    public void LevelStringLoader_InvalidPath_ThrowsException() {
        Assert.Throws<System.IO.FileNotFoundException>(() => LevelLoading.LevelStringLoader("invalid/path.txt"));
    }

    // Test for the StringToMap method



    [Test]
    public void StringToMap_ValidInput_ReturnsCorrectList() {

        List<string> expected = new List<string> {
            "------------",
            "------------",
            "-aaaaaaaaaa-",
            "-aaaaaaaaaa-",
            "-000----000-",
            "-000-%%-000-",
            "-000-11-000-",
            "-000-%%-000-",
            "-000----000-",
            "-%%%%%%%%%%-",
            "-%%%%%%%%%%-",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
        };

        List<string> actual = LevelLoading.StringToMap(lvl1);

        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]
    public void StringToMap_InvalidInput_ReturnsEmptyList() {
        string invalidInput = "Invalid input";
        List<string> expected = new List<string>();
        List<string> actual = LevelLoading.StringToMap(invalidInput);
        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]

    public void StopsReadingMapAtMapSlash() {
        string input = "Map:\n-aaaaaaaaaa-\n-aaaaaaaaaa-\nMap/\n-000----000-\n";
        List<string> expected = new List<string> {
            "-aaaaaaaaaa-",
            "-aaaaaaaaaa-"
        };
        List<string> actual = LevelLoading.StringToMap(input);
        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]

    public void StringWithNoMapSection_ReturnsEmptyList() {
        string input = "No map section";
        List<string> expected = new List<string>();
        List<string> actual = LevelLoading.StringToMap(input);
        Assert.That(expected, Is.EqualTo(actual));
    }

    // Test for the StringToMeta method

    [Test]
    public void StringToMeta_ValidInput_ReturnsCorrectList() {
        List<string> expected = new List<string> {
            "Name: LEVEL 1",
            "Time: 300",
            "Hardened: %",
            "PowerUp: 1"
        };

        List<string> actual = LevelLoading.StringToMeta(lvl1);

        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void StringToMeta_InvalidInput_ReturnsEmptyList() {
        string invalidInput = "Invalid input";
        List<string> expected = new List<string>();
        List<string> actual = LevelLoading.StringToMeta(invalidInput);
        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]
    public void StopsReadingMetaAtMetaSlash() {
        string input = "Meta:\nName: LEVEL 1\nTime: 300\nMeta/\nHardened: %\n";
        List<string> expected = new List<string> {
            "Name: LEVEL 1",
            "Time: 300"
        };
        List<string> actual = LevelLoading.StringToMeta(input);
        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]
    public void StringWithNoMetaSection_ReturnsEmptyList() {
        string input = "No meta section";
        List<string> expected = new List<string>();
        List<string> actual = LevelLoading.StringToMeta(input);
        Assert.That(expected, Is.EqualTo(actual));
    }
    // Test for the StringToLegend method
    [Test]
    public void StringToLegend_ValidInput_ReturnsCorrectList() {
        List<string> expected = new List<string> {
            "%) Breakout.Assets.Images.blue-block.png",
            "0) Breakout.Assets.Images.grey-block.png",
            "1) Breakout.Assets.Images.orange-block.png",
            "a) Breakout.Assets.Images.purple-block.png"
        };

        List<string> actual = LevelLoading.StringToLegend(lvl1);

        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]
    public void StringToLegend_InvalidInput_ReturnsEmptyList() {
        string invalidInput = "Invalid input";
        List<string> expected = new List<string>();
        List<string> actual = LevelLoading.StringToLegend(invalidInput);
        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]
    public void StopsReadingLegendAtLegendSlash() {
        string input = "Legend:\n%) Breakout.Assets.Images.blue-block.png\n0) Breakout.Assets.Images.grey-block.png\nLegend/\n1) Breakout.Assets.Images.orange-block.png\n";
        List<string> expected = new List<string> {
            "%) Breakout.Assets.Images.blue-block.png",
            "0) Breakout.Assets.Images.grey-block.png"
        };
        List<string> actual = LevelLoading.StringToLegend(input);
        Assert.That(expected, Is.EqualTo(actual));
    }
    [Test]
    public void StringWithNoLegendSection_ReturnsEmptyList() {
        string input = "No legend section";
        List<string> expected = new List<string>();
        List<string> actual = LevelLoading.StringToLegend(input);
        Assert.That(expected, Is.EqualTo(actual));
    }



}