using System;
using System.Collections.Generic;

[Serializable]
public class SkinData
{
    public List<Skin> unlockedSkins = new List<Skin>(GetSkin.allSkins.Count);

    public Skin activeSkin = new Skin();
}
