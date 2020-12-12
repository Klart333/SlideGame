using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class LootBoxAmount
{
    public static int GetLootBoxAmount()
    {
        if (File.Exists(Application.persistentDataPath + "/LootBox.Amount"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/LootBox.Amount", FileMode.Open);
            int amount = (int)bf.Deserialize(file);
            file.Close();

            return amount;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Create(Application.persistentDataPath + "/LootBox.Amount");
            int amount = 0;

            bf.Serialize(file, amount);
            file.Close();

            return GetLootBoxAmount();
        }
    }

    public static void SetLootBoxAmount(int variation)
    {
        if (File.Exists(Application.persistentDataPath + "/LootBox.Amount"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/LootBox.Amount", FileMode.Open);
            int amount = (int)bf.Deserialize(file);
            file.Close();

            amount += variation;

            FileStream createdFile = File.Create(Application.persistentDataPath + "/LootBox.Amount");
            bf.Serialize(createdFile, amount);
            createdFile.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Create(Application.persistentDataPath + "/LootBox.Amount");
            int amount = 0;

            bf.Serialize(file, amount);
            file.Close();

            SetLootBoxAmount(variation);
        }
    }
}
