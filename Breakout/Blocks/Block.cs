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



public abstract class Block : Entity {


    protected int health = 1;

    public int Health {
        get {
            return health;
        }

        private set {
        }
    }

    protected int value = 10;

    public int Value {
        get {
            return value;
        }
        private set {
        }
    }


    public float XPos {
        get; private set;
    }

    public float YPos {
        get; private set;
    }

    public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {

        XPos = shape.Position.X;
        YPos = shape.Position.Y;
    }

    private void DeleteBlock() {
        this.DeleteEntity();

    }




    public virtual void TakeHit() {
        health -= 1;

        if (health <= 0) {
            DeleteBlock();

        }
    }

    public int GetHealth() {
        return health;
    }


}
