using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxReward : MonoBehaviour
{
    [SerializeField]
    private GameObject lootDisplay;

    private void OnEnable()
    {
        GiveLoot();
    }

    private void GiveLoot()
    {
        Loot loot = new Loot(0, "");
        if (UnityEngine.Random.Range(1, 3) == 1)
        {
            loot.money = UnityEngine.Random.Range(5, 50);
        }
        else
        {
            loot.skin = GetRandomSkin();
        }


        if (GetSkin.allSkins.Contains(loot.skin))
        {
            SaveSkin.SaveASkin(loot.skin);
        }

        // MoneyClass.Money += loot.money;

        GameObject gmLootDisplay = Instantiate(lootDisplay, transform.position, Quaternion.identity);
        gmLootDisplay.GetComponent<SetLootDisplay>().SetDisplay(loot);

        GetComponent<UIMoveCamera>().OnButton();

        Destroy(gameObject.transform.parent.gameObject, 3);
    }

    private string GetRandomSkin()
    {
        return GetSkin.allSkins[UnityEngine.Random.Range(0, GetSkin.allSkins.Count)];
    }
}
