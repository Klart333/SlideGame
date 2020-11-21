using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Dictionary<int, Vector3> CustomSpawnpoints = new Dictionary<int, Vector3>();
    public static Dictionary<int, Vector3> CustomSpawnrotations = new Dictionary<int, Vector3>();

    [SerializeField]
    private GameObject[] players;

    public event Action<GameObject> OnplayerInitiated = delegate { };

    public int Score { get { return Mathf.RoundToInt((Mathf.Pow(Mathf.Abs(savedVelocity), 3)) / levelTimer); } }

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
            CustomSpawnpoints.Add(8, new Vector3(-18, 12, -2.5f));
            CustomSpawnrotations.Add(8, new Vector3(0, 120, 0));
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
        FindObjectOfType<UIStarManager>().LightUpStars(lastLevelIndex, Score);
    }

    private void ShowScore()
    {
        FindObjectOfType<UIPlayerScore>().PrintScore(Score);

        GameObject.Find("PlayerSpeedText").GetComponent<TextMeshProUGUI>().text = (Mathf.RoundToInt(savedVelocity * 10f) / 10f).ToString();
        GameObject.Find("PlayerTimeText").GetComponent<TextMeshProUGUI>().text = (Mathf.RoundToInt(levelTimer * 10f) / 10f).ToString();
    }
}
