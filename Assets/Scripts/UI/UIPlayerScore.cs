using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIPlayerScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    public void PrintScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
