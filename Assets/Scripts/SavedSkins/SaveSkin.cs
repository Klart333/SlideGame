﻿using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public static class SaveSkin
{
    public static void SaveASkin(Skin skin)
    {
        if (File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream openedFile = File.Open(Application.persistentDataPath + "/SkinData.Main", FileMode.Open);
            SkinData skinData = (SkinData)bf.Deserialize(openedFile);
            openedFile.Close();

            for (int i = 0; i < skinData.unlockedSkins.Count + 1; i++)
            {
                if (i == skinData.unlockedSkins.Count) // If we made it through all the way
                {
                    if (!GetSkin.GetAllUnlockedSkins().Contains(skin))
                    {
                        skinData.unlockedSkins.Add(skin);
                        break;
                    }
                    else
                    {
                        Debug.Log("Skin Already Owned");
                        return;
                    }
                }

                if (skinData.unlockedSkins[i].name == skin.name) // If we already own the skin we break and don't add it
                {
                    break;
                }
            }

            FileStream createdFile = File.Create(Application.persistentDataPath + "/SkinData.Main");
            bf.Serialize(createdFile, skinData);
            createdFile.Close();

            SortSkins();
            Debug.Log("Skin Saved: " + skin.name);
        }
        else
        {
            Debug.LogError("Skins not initialized");
        }
    }

    public static void SortSkins()
    {
        
        if (File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            Debug.Log("Sorting Skins");

            BinaryFormatter bf = new BinaryFormatter();

            FileStream openedFile = File.Open(Application.persistentDataPath + "/SkinData.Main", FileMode.Open);
            SkinData skinData = (SkinData)bf.Deserialize(openedFile);
            openedFile.Close();

            List<Skin> wellDoneList = new List<Skin>();
            List<Skin> rareList = new List<Skin>();
            List<Skin> commonList = new List<Skin>();

            foreach (var skin in skinData.unlockedSkins)
            {
                if (skin.rarity == Rarity.WellDone)
                {
                    wellDoneList.Add(skin);
                    continue;
                }
                else if (skin.rarity == Rarity.Rare)
                {
                    rareList.Add(skin);
                    continue;
                }
                else if (skin.rarity == Rarity.Common)
                {
                    commonList.Add(skin);
                    continue;
                }
            }

            skinData.unlockedSkins.Clear();
            skinData.unlockedSkins.AddRange(wellDoneList);
            skinData.unlockedSkins.AddRange(rareList);
            skinData.unlockedSkins.AddRange(commonList);

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

    public static void SetActiveSkin(Skin skin)
    {
        if (File.Exists(Application.persistentDataPath + "/SkinData.Main"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream openedFile = File.Open(Application.persistentDataPath + "/SkinData.Main", FileMode.Open);
            SkinData skinData = (SkinData)bf.Deserialize(openedFile);
            openedFile.Close();

            skinData.activeSkin = skin;

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
