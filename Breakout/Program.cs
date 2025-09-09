namespace Breakout;

using System;
using System.Reflection;
using Breakout.LevelHandler;
using DIKUArcade.GUI;
using SixLabors.ImageSharp.Drawing.Processing;
class Program {

    static void Main(string[] args) {

        var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
        var game = new Game(windowArgs);
        game.Run();


    }
}