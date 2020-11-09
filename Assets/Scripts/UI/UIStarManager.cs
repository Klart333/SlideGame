using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStarManager : MonoBehaviour
{
    [SerializeField]
    private Sprite starFillImage;

    private Image[] starImages;

    private Dictionary<int, int[]> levelDictionary = new Dictionary<int, int[]>();

    private int[] levelOneStars = new int[4] { 500, 1000, 1500, 2000 }; // Fourth for a chest

    private void Awake()
    {
        starImages = GetComponentsInChildren<Image>();

        levelDictionary.Add(1, levelOneStars);
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
                LootBoxAmount.SetLootBoxAmount(1);
                break;
            }

            starImages[i].sprite = starFillImage;
        }
    }
}
