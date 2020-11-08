using System;
using UnityEngine;

public class SpawnLootBox : MonoBehaviour
{
    [SerializeField]
    private GameObject lootBox;

    [SerializeField]
    private GameObject lootBoxCanvas;

    public event Action OnLootBoxOpened = delegate { };

    public void SpawnBox()
    {
        if (LootBoxAmount.GetLootBoxAmount() > 0)
        {
            LootBoxAmount.SetLootBoxAmount(-1);

            for (int i = 0; i < lootBoxCanvas.transform.childCount; i++)
            {
                lootBoxCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }

            Instantiate(lootBox, Vector3.zero, Quaternion.identity);
            OnLootBoxOpened();
        }
    }
}
