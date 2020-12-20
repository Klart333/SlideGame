using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    const string privateCode = "ZAOzlEXddEK1gT78QIn08QPT5uSCM7E0OsDXfmJTx3VQ";
    const string publicCode = "5fd3425feb36fe27140e78c3";
    const string webURL = "http://dreamlo.com/lb/";

    public event Action<Highscore[]> OnDownloadDone = delegate { };

    public Highscore[] highscoreList;

    public void AddNewHighscore(int score, int levelIndex)
    {
        StartCoroutine(UploadNewHighscore(score, levelIndex));
    }

    public IEnumerator UploadNewHighscore(int score, int levelIndex) // They're public becuase I need to wait until they're done (still keeping the normal methods for when I don't care about the timing)
    {
        UnityWebRequest www = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL("Level" + levelIndex) + "/" + score + "/" + levelIndex);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.LogError("Error Uploading " + www.error);
        }
        else
        {
            print("Upload Successful");
            DownloadHighscores();
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    public IEnumerator DownloadHighscoresFromDatabase()
    {
        UnityWebRequest www = new UnityWebRequest(webURL + publicCode + "/pipe/");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.LogError("Error Uploading " + www.error);
        }
        else
        {
            FormatHighscores(www.downloadHandler.text);
        }

        OnDownloadDone(highscoreList);
    }

    private void FormatHighscores(string textStream)
    {
        if (textStream.Length == 0)
        {
            return;
        }

        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];

        if (entries.Length > 0)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                if (entryInfo.Length < 3)
                {
                    return; // Something went wrong and we just abort
                }

                string username = entryInfo[0]; // Still here for clarity that the first thing is the username, but we do not use it

                int score = 0;
                int.TryParse(entryInfo[1], out score);
                int levelIndex = 0;
                int.TryParse(entryInfo[2], out levelIndex);

                highscoreList[i] = new Highscore(score, levelIndex);
            }
        }
    }
}

public struct Highscore
{
    public int score;
    public int levelIndex;

    public Highscore(int score, int levelIndex)
    {
        this.score = score;
        this.levelIndex = levelIndex;
    }
}