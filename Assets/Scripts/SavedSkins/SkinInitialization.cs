using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
public class SkinInitialization : MonoBehaviour
{
    private void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/SkinData.Main");
        SkinData skinData = new SkinData();

        skinData.activeSkin = GetSkin.Default;
        skinData.unlockedSkins = new List<Skin>() { GetSkin.Default };

        bf.Serialize(file, skinData);
        file.Close();

        print("Skin Initialization Complete");
    }
}
