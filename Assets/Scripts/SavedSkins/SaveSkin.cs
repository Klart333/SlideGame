using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSkin
{
    public static void SaveASkin(string skin)
    {
        if (File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream openedFile = File.Open(Application.persistentDataPath + "/SkinData.Main", FileMode.Open);
            SkinData skinData = (SkinData)bf.Deserialize(openedFile);
            openedFile.Close();

            if (!skinData.unlockedSkins.Contains(skin)) // If we dont already own the skin
            {
                skinData.unlockedSkins.Add(skin);
            }

            FileStream createdFile = File.Create(Application.persistentDataPath + "/SkinData.Main");
            bf.Serialize(createdFile, skinData);
            createdFile.Close();

            Debug.Log("Skin Saved");
        }
        else
        {
            Debug.LogError("Skins not initialized");
        }
    }

    public static void SetActiveSkin(string skin)
    {
        if (File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream openedFile = File.Open(Application.persistentDataPath + "/SkinData.Main", FileMode.Open);
            SkinData skinData = (SkinData)bf.Deserialize(openedFile);
            openedFile.Close();

            if (skinData.unlockedSkins.Contains(skin)) // If we own the skin
            {
                skinData.activeSkin = skin;
            }
            else
            {
                Debug.LogError("Trying to set active skin to a skin not owned");
            }

            FileStream createdFile = File.Create(Application.persistentDataPath + "/SkinData.Main");
            bf.Serialize(createdFile, skinData);
            createdFile.Close();

            Debug.Log("Skin Saved");
        }
        else
        {
            Debug.LogError("Skins not initialized");
        }
    }
}
