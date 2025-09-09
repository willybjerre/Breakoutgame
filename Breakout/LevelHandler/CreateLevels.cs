namespace Breakout.LevelHandler;

using System.IO;
using System.Net.Mime;
using System.Reflection;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using LevelHandler;
using Microsoft.VisualBasic;
using SixLabors.ImageSharp.Drawing.Processing;
public class CreateLevels {


    public static EntityContainer<Blocks.Block> Createblocks(string Path) {
        string Level = LevelLoading.LevelStringLoader(Path);
        EntityContainer<Blocks.Block> blockLevel = new EntityContainer<Blocks.Block>();
        List<string> map = LevelLoading.StringToMap(Level);
        List<string> legend = LevelLoading.StringToLegend(Level);
        List<string> meta = LevelLoading.StringToMeta(Level);

        float blockWidth = 0.07f;
        float blockHeight = 0.05f;

        for (int row = 0; row < map.Count; row++) {
            string line = map[row];
            for (int col = 0; col < line.Length; col++) {
                char symbol = line[col];
                if (symbol == '-')
                    continue;


                Image? image = MatchLegend(legend, symbol);

                if (image == null) {
                    throw new FileNotFoundException($"Image file not found for symbol: {symbol}");
                }

                float XPos = col * blockWidth + 0.08f;
                float YPos = 1.0f - row * blockHeight;

                var shape = new StationaryShape(XPos, YPos, blockWidth, blockHeight);

                Blocks.Block block = MatchMeta(meta, symbol, shape, image);

                blockLevel.AddEntity(block);

            }
        }
        return blockLevel;
    }

    public static EntityContainer<Blocks.Block> GetBreakableBlocks(string Path) {
        EntityContainer<Blocks.Block> BlockLevel = new EntityContainer<Blocks.Block>();
        BlockLevel = Createblocks(Path);
        EntityContainer<Blocks.Block> breakableblocks = new EntityContainer<Blocks.Block>();
        BlockLevel.Iterate(block => {
            if (!(block is Blocks.Unbreakable)) {
                breakableblocks.AddEntity(block);
            }
        });
        return breakableblocks;

    }
    public static EntityContainer<Blocks.Block> GetUnbreakableBlocks(string Path) {
        EntityContainer<Blocks.Block> BlockLevel = new EntityContainer<Blocks.Block>();
        BlockLevel = Createblocks(Path);
        EntityContainer<Blocks.Block> Unbreakableblocks = new EntityContainer<Blocks.Block>();
        BlockLevel.Iterate(block => {
            if (block is Blocks.Unbreakable) {
                Unbreakableblocks.AddEntity(block);
            }
        });
        return Unbreakableblocks;

    }



    public static Image MatchLegend(List<string> legend, char symbol) {

        string? match = legend.Find(entry => entry.StartsWith(symbol));
        if (match == null) {
            return null;
        }


        string imagePath = match.Split(')')[1].Trim();

        // if (!File.Exists(imagePath)) {
        // throw new FileNotFoundException($"Image file not found: {imagePath}");
        // }


        var image = new Image(imagePath);

        return image;

    }
    public static Blocks.Block MatchMeta(List<string> meta, char symbol, StationaryShape shape, Image image) {

        Blocks.Block block;

        bool isHardened = meta.Any(entry => entry.StartsWith("Hardened:") && entry.Contains(" " + symbol));
        bool isPowerUp = meta.Any(entry => entry.StartsWith("PowerUp:") && entry.Contains(" " + symbol));
        bool isUnbreable = meta.Any(entry => entry.StartsWith("Unbreakable:") && entry.Contains(" " + symbol));



        if (isHardened) {
            block = new Blocks.Hardened(shape, image);
        } else if (isPowerUp) {
            block = new Blocks.PowerUp(shape, image);
        } else if (isUnbreable) {
            block = new Blocks.Unbreakable(shape, image);
        } else {
            block = new Blocks.NormalBlock(shape, image);
        }
        return block;


    }
}



