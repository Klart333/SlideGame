using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Dictionary<int, Vector3> CustomSpawnpoints = new Dictionary<int, Vector3>()
    {
        { 5, new Vector3(465, 84.78f, 59.57f) },
        { 8, new Vector3(-18, 12, -2.5f) },
        { 10, new Vector3(3.6f, -10.7f, -44f) },
        { 11, new Vector3(-22f, -10.7f, -0.3f) },
        { 12, new Vector3(3f, 15f, -15f) },
        { 13, new Vector3(0, 13f, -2.5f) },
        { 14, new Vector3(7.5f, 7.3f, -25f) },
        { 15, new Vector3(15.5f, 37f, -60f) },
        { 16, new Vector3(-5.26f, -9.51f, 7.34f) },
        { 17, new Vector3(40.11f, -20f, -33.18f) },
        { 18, new Vector3(-0.27075f, -6.494f, -17.71f) }
    };

    public static Dictionary<int, Vector3> CustomSpawnrotations = new Dictionary<int, Vector3>() 
    {
        { 8, new Vector3(0, 120, 0) },
        { 11, new Vector3(0, 90, 0) },
        { 15, new Vector3(30, 0, 0) },
        { 16, new Vector3(0, 155, 0) },
        { 17, new Vector3(0, 220, 0) },
        { 18, new Vector3(-4.18f, 0, 0) }
    };

    [SerializeField]
    private GameObject[] players;

    public event Action<GameObject> OnplayerInitiated = delegate { };

    public int Score { get { return Mathf.RoundToInt((Mathf.Pow(Mathf.Abs(savedVelocity), 3)) / levelTimer); } }

    public bool isOnGoalStretch = false;
    public float savedVelocity = 0;
    public int lastLevelIndex;

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

    private void OnSceneChanged(Scene NullFuckingScene, Scene sceneToBe)
    {
        FindObjectOfType<FadePanel>().StartCoroutine(FindObjectOfType<FadePanel>().FadeIn());

        if (sceneToBe.name == "ScoreScene")
        {
            SetPlayButtons();
            LightStars();
            ShowScore();
            UnlockedLevels.SetHighestUnlockedLevel(lastLevelIndex + 1);
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
        Skin activeSkin = GetSkin.GetActiveSkin();
        GameObject prefab = null;

        foreach (GameObject player in players)
        {
            if (player.name == activeSkin.name)
            {
                prefab = player;
                break;
            }
        }

        GameObject gmPlayer = Instantiate(prefab, (CustomSpawnpoints.ContainsKey(lastLevelIndex)) ? CustomSpawnpoints[lastLevelIndex] : new Vector3(0, 15, -15), (CustomSpawnrotations.ContainsKey(lastLevelIndex)) ? Quaternion.Euler(CustomSpawnrotations[lastLevelIndex]) : prefab.transform.rotation);
        OnplayerInitiated(gmPlayer);
    }

    private void LightStars()
    {
        FindObjectOfType<UIStarManager>().StartCoroutine(FindObjectOfType<UIStarManager>().LightUpStars(lastLevelIndex, Score));
    }

    private void ShowScore()
    {
        FindObjectOfType<UIPlayerScore>().PrintScore(Score);

        GameObject.Find("PlayerSpeedText").GetComponent<TextMeshProUGUI>().text = (Mathf.RoundToInt(savedVelocity * 10f) / 10f).ToString();
        GameObject.Find("PlayerTimeText").GetComponent<TextMeshProUGUI>().text = (Mathf.RoundToInt(levelTimer * 10f) / 10f).ToString();
    }
}
