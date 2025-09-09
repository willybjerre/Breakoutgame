namespace Breakout.Blocks;


using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

public class Hardened : Block {

    public Hardened(StationaryShape shape, IBaseImage image) : base(shape, image) {

        health = health + health;
        value = 20;
    }


}