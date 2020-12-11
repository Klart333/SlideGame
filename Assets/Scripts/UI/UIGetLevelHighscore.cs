using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGetLevelHighscore : MonoBehaviour
{
    private Highscores highscoresScript;
    private TextMeshProUGUI highscoreText;

    private void Awake()
    {
        highscoresScript = FindObjectOfType<Highscores>();
        highscoresScript.AddNewHighscore(GameManager.Instance.Score, GameManager.Instance.lastLevelIndex); // Maybe shouldn't be this script adding the highscore, but whatever
        highscoresScript.OnDownloadDone += GetRelevantScore;

        highscoreText = GetComponent<TextMeshProUGUI>();
    }

    private void GetRelevantScore(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreList.Length; i++)
        {
            if (highscoreList[i].levelIndex == GameManager.Instance.lastLevelIndex)
            {
                UpdateText(highscoreList[i].score);
                return;
            }
        }

        UpdateText(0); // If we didn't find any highscore
    }

    private void UpdateText(int score)
    {
        string text = score.ToString();
        highscoreText.text = text;
    }
}
