using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class LevelData
{
    public static void SaveLevelData(int levelIndex, int starCount, bool hasChest)
    {
        if (File.Exists(Application.persistentDataPath + "/LevelData." + levelIndex)) // If we already have something saved, we only want to override it if our new save is better
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/LevelData." + levelIndex, FileMode.Open);
            LevelInfo levelInfo = (LevelInfo)bf.Deserialize(file);
            file.Close();

            if (starCount > levelInfo.starCount)
            {
                Debug.Log("Overriding Already Existing Data");
                CreateSaveFile(levelIndex, starCount, hasChest);
            }
            else if (levelInfo.starCount == starCount && (levelInfo.hasChest == false && hasChest == true))
            {
                Debug.Log("Overriding Already Existing Data");
                CreateSaveFile(levelIndex, starCount, hasChest);
            }
            else
            {
                Debug.Log("Score Insufficient To Override Data");
                return;
            }
        }
        else
        {
            Debug.Log("Data Not Found, Thus Creating Save");
            CreateSaveFile(levelIndex, starCount, hasChest);
        }
    }

    public static LevelInfo GetLevelData(int levelIndex)
    {
        if (File.Exists(Application.persistentDataPath + "/LevelData." + levelIndex)) 
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/LevelData." + levelIndex, FileMode.Open);
            LevelInfo levelInfo = (LevelInfo)bf.Deserialize(file);
            file.Close();

            return levelInfo;
        }
        else
        {
            CreateSaveFile(levelIndex, 0, false);
            return new LevelInfo(levelIndex, 0, false);
        }
    }

    private static void CreateSaveFile(int levelIndex, int starCount, bool hasChest)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/LevelData." + levelIndex);
        LevelInfo skinData = new LevelInfo(levelIndex, starCount, hasChest);

        bf.Serialize(file, skinData);
        file.Close();
    }
}
