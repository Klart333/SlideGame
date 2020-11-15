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

    private Dictionary<int, int[]> levelDictionary = new Dictionary<int, int[]>();

    private int[] levelOneStars = new int[4] { 1000, 3000, 4000, 5000 }; // Fourth for a chest
    private int[] levelTwoStars = new int[4] { 5000, 7000, 8000, 8700 }; 
    private int[] levelThreeStars = new int[4] { 2500, 4000, 6000, 7500 }; 
    private int[] levelFourStars = new int[4] { 2500, 4000, 6000, 7500 }; 

    private void Awake()
    {
        starImages = GetComponentsInChildren<Image>();

        levelDictionary.Add(1, levelOneStars);
        levelDictionary.Add(2, levelTwoStars);
        levelDictionary.Add(3, levelThreeStars);
        levelDictionary.Add(4, levelFourStars);
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
