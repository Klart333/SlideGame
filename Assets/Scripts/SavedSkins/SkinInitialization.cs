using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
public class SkinInitialization : MonoBehaviour
{
    private void Awake()
    {
        Initialize();

        if (!File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
        }
    }

    private void Initialize()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/SkinData.Main");
        SkinData skinData = new SkinData();

        skinData.activeSkin = "Default";
        skinData.unlockedSkins = new List<string>() { "Default" };

        bf.Serialize(file, skinData);
        file.Close();

        print("Initialization Complete");
    }
}
