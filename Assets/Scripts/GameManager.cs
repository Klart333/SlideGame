using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private float timerWeight = 1;

    public event Action<GameObject> OnplayerInitiated = delegate { };

    public bool isOnGoalStretch = false;
    public float savedVelocity = 0;

    private float levelTimer = 0;
    private int lastLevelIndex;

    private void Awake()
    {
        if (Instance == null) // Initialisation for the GameManager
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            SceneManager.activeSceneChanged += OnSceneChanged;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (!isOnGoalStretch)
        {
            levelTimer += Time.deltaTime;
        }
    }

    private void OnSceneChanged(Scene currentScene, Scene sceneToBe)
    {
        if (sceneToBe.name == "ScoreScene")
        {
            SetPlayButtons();
            LightStars();
            ShowScore();
            return;
        }

        int res = 0;
        int.TryParse(sceneToBe.name, out res);

        if (res > 0)
        {
            isOnGoalStretch = false;
            levelTimer = 0;
            savedVelocity = 0;
            lastLevelIndex = int.Parse(sceneToBe.name);

            SpawnPlayer();
        }

    }

    private void SetPlayButtons()
    {
        GameObject.Find("PlayAgainText").GetComponent<UILoadScene>().sceneToLoadIndex = lastLevelIndex;
        GameObject.Find("NextLevelText").GetComponent<UILoadScene>().sceneToLoadIndex = lastLevelIndex + 1;
    }

    private void SpawnPlayer()
    {
        string activeSkin = GetSkin.GetActiveSkin();
        GameObject prefab = null;

        foreach (GameObject player in players)
        {
            if (player.name == activeSkin)
            {
                prefab = player;
                break;
            }
        }

        GameObject gmPlayer = Instantiate(prefab, new Vector3(0, 15, -15), prefab.transform.rotation);
        OnplayerInitiated(gmPlayer);

        print("Player Initiated");
    }

    private void LightStars()
    {
        FindObjectOfType<UIStarManager>().LightUpStars(lastLevelIndex, CalculateScore(savedVelocity));
    }

    private void ShowScore()
    {
        FindObjectOfType<UIPlayerScore>().PrintScore(CalculateScore(savedVelocity));
    }

    private int CalculateScore(float velocity)
    {
        return Mathf.RoundToInt((Mathf.Pow(velocity, 3)) / (levelTimer * timerWeight));
    }
}
