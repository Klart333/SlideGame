using System;
using UnityEngine;

[Serializable]
public struct Skin 
{
    public string name;
    public Rarity rarity;
    public SerializableColor color;

    public float slideSpeed;
    public float airSpeed;
    public float reverseSpeed;

    public Skin(string name, Rarity rarity, Color color, float slideSpeed, float airSpeed, float reverseSpeed)
    {
        this.name = name;
        this.rarity = rarity;
        this.color = new SerializableColor(color);
        this.slideSpeed = slideSpeed;
        this.airSpeed = airSpeed;
        this.reverseSpeed = reverseSpeed;
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
