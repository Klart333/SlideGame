using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class GetSkin
{
    public static List<string> allSkins = new List<string>() { "Default", "Nebula", "Beach" };
    public static string GetActiveSkin()
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
            return null;
        }
    }

    public static List<string> GetAllUnlockedSkins()
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
