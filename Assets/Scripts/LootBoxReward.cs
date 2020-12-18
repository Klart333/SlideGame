using UnityEngine;

public class LootBoxReward : MonoBehaviour
{
    [SerializeField]
    private GameObject lootDisplay;

    private float wellDoneChance = 5f / 100f;
    private float rareChance = 25f / 100f;
    private float commonChance = 55f / 100f;

    private void OnEnable()
    {
        GiveLoot();
    }

    private void GiveLoot()
    {
        Loot loot = new Loot(0, new Skin());

        float randNum = Random.Range(0.00f, 1.00f);

        if (randNum < wellDoneChance)
        {
            loot.skin = GetRandomSkin(Rarity.WellDone);
        }
        else if (randNum < rareChance)
        {
            loot.skin = GetRandomSkin(Rarity.Rare);
        }
        else if (randNum < commonChance)
        {
            loot.skin = GetRandomSkin(Rarity.Common);
        }
        else
        {
            loot.money = UnityEngine.Random.Range(5, 50);
        }

        if (GetSkin.allSkins.Contains(loot.skin))
        {
            SaveSkin.SaveASkin(loot.skin);
        }

        // MoneyClass.Money += loot.money;

        GameObject gmLootDisplay = Instantiate(lootDisplay, transform.position, Quaternion.identity);
        gmLootDisplay.GetComponent<AcceptLoot>().lootbox = this.transform.parent.gameObject;
        gmLootDisplay.GetComponent<SetLootDisplay>().SetDisplay(loot);

        if (loot.skin.color != null)
        {
            gmLootDisplay.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(loot.skin.color.r, loot.skin.color.g, loot.skin.color.b, loot.skin.color.a);
        }

        GetComponent<UIMoveCamera>().OnButton();
    }

    private Skin GetRandomSkin(Rarity rarity)
    {
        Skin skin = GetSkin.allSkins[UnityEngine.Random.Range(0, GetSkin.allSkins.Count)];

        while (skin.rarity != rarity) // We keep banging our head into the wall into it breaks
        {
            skin = GetSkin.allSkins[UnityEngine.Random.Range(0, GetSkin.allSkins.Count)];
        }
        
        return skin;
    }
}
