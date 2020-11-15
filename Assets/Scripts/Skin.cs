using System;

[Serializable]
public struct Skin 
{
    public string name;
    public Rarity rarity;

    public Skin(string name, Rarity rarity)
    {
        this.name = name;
        this.rarity = rarity;
    }
}

public enum Rarity
{
    Common,
    Rare,
    WellDone, 
}
