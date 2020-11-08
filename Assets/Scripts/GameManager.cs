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
        Debug.Log("Hej");
        //OnplayerInitiated = delegate { };

        if (sceneToBe.buildIndex == 1)
        {
            isOnGoalStretch = false;
            levelTimer = 0;
            savedVelocity = 0;

            SpawnPlayer();
        }
        else if (sceneToBe.buildIndex == 2)
        {
            ShowScore();
        }
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

    private void ShowScore()
    {
        FindObjectOfType<UIPlayerScore>().PrintScore(CalculateScore(savedVelocity));
    }

    private int CalculateScore(float velocity)
    {
        return Mathf.RoundToInt((Mathf.Pow(velocity, 3)) / (levelTimer * timerWeight));
    }
}
