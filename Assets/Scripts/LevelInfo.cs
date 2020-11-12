using System;

[Serializable]
public struct LevelInfo
{
    public int levelIndex;
    public int starCount;
    public bool hasChest;

    public LevelInfo(int levelIndex, int starCount, bool hasChest)
    {
        this.levelIndex = levelIndex;
        this.starCount = starCount;
        this.hasChest = hasChest;
    }
}
