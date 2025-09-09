namespace Breakout.GameEvents;

using System.Numerics;

public readonly struct AddEffectEvent {
    public string EffectType {
        get;
    }
    public Vector2 Position {
        get;
    }

    public AddEffectEvent(string effectType, Vector2 position) {
        EffectType = effectType;
        Position = position;
    }
}