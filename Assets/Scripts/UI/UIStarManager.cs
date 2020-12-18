using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIStarManager : MonoBehaviour
{
    [SerializeField]
    private Sprite starFillImage;

    [SerializeField]
    private GameObject rewardLootBox;

    [SerializeField]
    private GameObject alreadyGotText;

    [SerializeField]
    private SimpleAudioEvent simpleAudioEvent;

    private Image[] starImages;
    private AudioSource audioSource;

    private Dictionary<int, int[]> levelDictionary = new Dictionary<int, int[]>() // Fourth for a chest
    {
        {1, new int[4] { 1000, 2000, 3000, 5000 } },
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
        {13, new int[4] { 2000, 5000, 8000, 12000 } },
        {14, new int[4] { 1000, 2000, 5000, 12000 } },
        {15, new int[4] { 2000, 4000, 6000, 12000 } },
        {16, new int[4] { 300, 700, 1200, 2500 } },
        {17, new int[4] { 2000, 4000, 6000, 12000 } },
        {18, new int[4] { 2000, 3000, 5000, 12000 } }
    };

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        starImages = GetComponentsInChildren<Image>();
    }

    public IEnumerator LightUpStars(int level, float score)
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

        for (int i = 0; i < length; i++)
        {
            if (i == 3)
            {
                Instantiate(rewardLootBox, new Vector3(0, 1, 0), Quaternion.identity);

                if (LevelData.GetLevelData(level).hasChest == false)
                {
                    LootBoxAmount.SetLootBoxAmount(1);
                    // We Only play a sound if the box is real
                    yield return new WaitForSeconds(1);
                    //Play bing-bing-BONG
                }
                else
                {
                    alreadyGotText.SetActive(true);
                }


                break;
            }

            simpleAudioEvent.Play(audioSource);
            starImages[i].sprite = starFillImage;
            yield return StartCoroutine(AnimateStar(starImages[i]));
        }

        LevelData.SaveLevelData(level, Mathf.Clamp(length, 0, 3), length >= 4);
    }

    private IEnumerator AnimateStar(Image image)
    {
        float t = 0;
        float speed = 2;

        Vector3 ogScale = image.transform.localScale;
        Vector3 bigScale = new Vector3(1.5f, 1.5f, 1.5f);

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            image.transform.localScale = Vector3.Lerp(ogScale, bigScale, t);
            yield return null;
        }

        t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            image.transform.localScale = Vector3.Lerp(bigScale, ogScale, t);
            yield return null;
        }
        image.transform.localScale = Vector3.Lerp(bigScale, ogScale, 1);
    }
}
