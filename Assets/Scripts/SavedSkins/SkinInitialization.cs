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

    private void Initialize()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/SkinData.Main");
        SkinData skinData = new SkinData();

        skinData.activeSkin = new Skin("Default", Rarity.Common, Color.white);
        skinData.unlockedSkins = new List<Skin>() { new Skin("Default", Rarity.Common, Color.white) };

        bf.Serialize(file, skinData);
        file.Close();

        print("Initialization Complete");
    }
}
