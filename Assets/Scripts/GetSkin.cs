using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class GetSkin
{
    public static List<Skin> allSkins = new List<Skin>() { new Skin("Default", Rarity.Common, Color.white), new Skin("Why isn't this one the default", Rarity.Common, Color.white), new Skin("Nebula", Rarity.WellDone, new Color(1, 0, 1, 1)), new Skin("Beach", Rarity.Rare, Color.blue), new Skin("Fashionista", Rarity.Rare, Color.blue), new Skin("Caveman", Rarity.Rare, Color.blue) };
    public static Skin GetActiveSkin()
    {
        if (File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/SkinData.Main", FileMode.Open);
            SkinData skinData = (SkinData)bf.Deserialize(file);
            file.Close();

            return skinData.activeSkin;
        }
        else
        {
            Debug.LogError("Skins not initialized");
            return new Skin();
        }
    }

    public static List<Skin> GetAllUnlockedSkins()
    {
        if (File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/SkinData.Main", FileMode.Open);
            SkinData skinData = (SkinData)bf.Deserialize(file);
            file.Close();

            return skinData.unlockedSkins;
        }
        else
        {
            Debug.LogError("Skins not initialized");
            return null;
        }
    }
}
