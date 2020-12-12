using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStarManager : MonoBehaviour
{
    [SerializeField]
    private Sprite starFillImage;

    [SerializeField]
    private GameObject rewardLootBox;

    [SerializeField]
    private GameObject alreadyGotText;

    private Image[] starImages;

    private Dictionary<int, int[]> levelDictionary = new Dictionary<int, int[]>() // Fourth for a chest
    {
        {1, new int[4] { 1000, 3000, 4000, 5000 } },
        {2, new int[4] { 5000, 7000, 8000, 10000 } },
        {3, new int[4] { 5000, 10000, 15000, 22000 } },
        {4, new int[4] { 300, 500, 700, 1000 } },
        {5, new int[4] { 300, 500, 700, 1000 } },
        {6, new int[4] { 5000, 7500, 10000, 12000 } },
        {7, new int[4] { 2000, 4000, 6000, 10000 } },
        {8, new int[4] { 2000, 4000, 6000, 10000 } },
        {9, new int[4] { 1000, 2000, 2500, 3500 } },
        {10, new int[4] { 2500, 5000, 8000, 10000 } },
        {11, new int[4] { 100, 400, 800, 2000 } },
        {12, new int[4] { 5000, 7000, 8000, 10000 } },
        {13, new int[4] { 2000, 5000, 8000, 12000 } }
    };

    private void Awake()
    {
        starImages = GetComponentsInChildren<Image>();
    }

    public void LightUpStars(int level, float score)
    {
        int length = 0;

        int[] starArray = new int[4];
        levelDictionary.TryGetValue(level, out starArray);

        for (int i = 0; i < starArray.Length; i++)
        {
            if (score >= starArray[i])
            {
                length += 1;
            }
        }

        for (int i = 0; i <= length; i++)
        {
            if (i == 4)
            {
                Instantiate(rewardLootBox, new Vector3(0, 1, 0), Quaternion.identity);

                if (LevelData.GetLevelData(level).hasChest == false)
                {
                    LootBoxAmount.SetLootBoxAmount(1);
                }
                else
                {
                    alreadyGotText.SetActive(true);
                }

                break;
            }

            starImages[i].sprite = starFillImage;
        }

        LevelData.SaveLevelData(level, Mathf.Clamp(length, 0, 3), length >= 4);
    }
}
