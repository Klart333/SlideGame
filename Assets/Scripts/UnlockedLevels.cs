using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class UnlockedLevels
{
    public static int GetHighestUnlockedLevel()
    {
        if (!File.Exists(Application.persistentDataPath + "/LevelData.UnlockedLevels"))
        {
            Initialize();
        }

        if (File.Exists(Application.persistentDataPath + "/LevelData.UnlockedLevels"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/LevelData.UnlockedLevels", FileMode.Open);
            int highestUnlockedLevel = (int)bf.Deserialize(file);
            file.Close();

            return highestUnlockedLevel;
        }
        else
        {
            Debug.LogError("How the fuck did you get trough the initialization");
            return 1;
        }
    }

    public static void SetHighestUnlockedLevel(int highestLevel)
    {
        if (highestLevel > GetHighestUnlockedLevel())
        {
            if (!File.Exists(Application.persistentDataPath + "/LevelData.UnlockedLevels"))
            {
                Initialize();
            }

            if (File.Exists(Application.persistentDataPath + "/LevelData.UnlockedLevels"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                FileStream file = File.Create(Application.persistentDataPath + "/LevelData.UnlockedLevels");

                bf.Serialize(file, highestLevel);
                file.Close();
            }
            else
            {
                Debug.LogError("How the fuck did you get trough the initialization");
            }
        }
    }

    private static void Initialize()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/LevelData.UnlockedLevels");

        bf.Serialize(file, 1);
        file.Close();
    }
}
