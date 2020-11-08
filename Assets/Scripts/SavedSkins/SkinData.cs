using System;
using System.Collections.Generic;

[Serializable]
public class SkinData
{
    public List<string> unlockedSkins = new List<string>(GetSkin.allSkins.Count);
    public string activeSkin = "";
}
