using UnityEngine;

public class SetLootDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;
    public void SetDisplay(Loot loot)
    {
        GameObject prefab = null;

        if (loot.money != 0)
        {
            foreach (var item in prefabs)
            {
                if (item.name == "Money")
                {
                    prefab = item;
                    break;
                }
            }
        }
        else if (loot.skin != "")
        {
            foreach (var item in prefabs)
            {
                if (item.name == loot.skin)
                {
                    prefab = item;
                    break;
                }
            }
        }

        if (prefab != null)
        {
            Instantiate(prefab, transform);
        }
    }
}
