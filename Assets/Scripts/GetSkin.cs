using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class GetSkin
{
    public const float maxSlideSpeed = 75;
    public const float maxAirSpeed = 40;
    public const float maxReverseSpeed = -75;

    public static Skin Nebula = new Skin("Nebula", Rarity.WellDone, new Color(1, 0, 1, 1), 60, 20, -30);
    public static Skin Reverser = new Skin("Reverser", Rarity.WellDone, new Color(1, 0, 1, 1), 20, 5, -75);
    public static Skin CouchPotato = new Skin("Couch Potato", Rarity.WellDone, new Color(1, 0, 1, 1), 75, 0, -0);
    public static Skin Caveman = new Skin("Caveman", Rarity.Rare, Color.blue, 55, 1, -15);
    public static Skin Fashionista = new Skin("Fashionista", Rarity.Rare, Color.blue, 45, 12, -20);
    public static Skin Beach = new Skin("Beach", Rarity.Rare, Color.blue, 55, 3, -10);
    public static Skin Default = new Skin("Default", Rarity.Common, Color.white, 40, 6, -15);
    public static Skin Metal = new Skin("Metal", Rarity.Common, Color.white, 43, 0, -15);
    public static Skin Mint = new Skin("Mint", Rarity.Common, Color.white, 40, 6, -15);
    public static Skin WhyIsntThisOneTheDefault = new Skin("Why isn't this one the default", Rarity.Common, Color.white, 45, 6, -15);

    public static List<Skin> allSkins = new List<Skin>() 
    {   Nebula,
        Reverser,
        Default,
        Metal,
        Mint,
        WhyIsntThisOneTheDefault,
        Beach,
        Fashionista,
        Caveman,
        CouchPotato 
    };
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
