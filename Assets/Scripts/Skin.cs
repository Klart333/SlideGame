using System;
using UnityEngine;

[Serializable]
public struct Skin 
{
    public string name;
    public Rarity rarity;
    public SerializableColor color;
    public Skin(string name, Rarity rarity, Color color)
    {
        this.name = name;
        this.rarity = rarity;
        this.color = new SerializableColor(color);
    }
}

public enum Rarity
{
    Common,
    Rare,
    WellDone, 
}

[Serializable]
public class SerializableColor
{
    public float r;
    public float g;
    public float b;
    public float a;

    public SerializableColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
    }
}
