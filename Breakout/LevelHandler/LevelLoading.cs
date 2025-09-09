namespace Breakout.LevelHandler;

using System.IO;
using System.Net.Mime;
using System.Reflection;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Microsoft.VisualBasic;
using SixLabors.ImageSharp.Drawing.Processing;

public class LevelLoading {

    public static string LevelStringLoader(string Path) {

        using var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(Path)
            ?? throw new FileNotFoundException($"{Path} not found");



        using (StreamReader reader = new StreamReader(stream!)) {
            var content = reader.ReadToEnd();
            return content;


        }


    }


    public static List<string> StringToMap(string Level) {
        var lines = Level.Split("\n");
        var mapLines = new List<string>();
        bool inmap = false;


        foreach (var rawline in lines) {
            var line = rawline.Trim();
            if (line == "Map:") {
                inmap = true;
                continue;
            }
            if (line == "Map/") {
                break;
            }
            if (inmap) {
                mapLines.Add(line);
            }


        }
        return mapLines;
    }

    public static List<string> StringToMeta(string Level) {
        var lines = Level.Split("\n");
        var metaLines = new List<string>();
        bool inmap = false;


        foreach (var rawline in lines) {
            var line = rawline.Trim();
            if (line == "Meta:") {
                inmap = true;
                continue;
            }
            if (line == "Meta/") {
                break;
            }
            if (inmap) {
                metaLines.Add(line);
            }


        }
        return metaLines;
    }

    public static List<string> StringToLegend(string Level) {
        var lines = Level.Split("\n");
        var LegendLines = new List<string>();
        bool inmap = false;


        foreach (var rawline in lines) {
            var line = rawline.Trim();
            if (line == "Legend:") {
                inmap = true;
                continue;
            }
            if (line == "Legend/") {
                break;
            }
            if (inmap) {
                LegendLines.Add(line);
            }


        }
        return LegendLines;
    }

}