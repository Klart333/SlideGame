﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UISetLevelDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject starPanel;

    [SerializeField]
    private Sprite starFilled;

    [SerializeField]
    private Image lootBoxImage;

    [SerializeField]
    private Sprite lootBoxFilled;

    [SerializeField]
    private GameObject lockPanel;

    private int levelIndex;

    private void Awake()
    {
        levelIndex = int.Parse(GetComponentInChildren<TextMeshProUGUI>().text);

        LevelInfo levelInfo = LevelData.GetLevelData(levelIndex);
        if (levelInfo.hasChest == true)
        {
            lootBoxImage.sprite = lootBoxFilled;
        }

        for (int i = 0; i < levelInfo.starCount; i++)
        {
            LightUpStar(i);
        }

        IsLocked();
    }

    private void LightUpStar(int index)
    {
        starPanel.transform.GetChild(index).GetComponent<Image>().sprite = starFilled;
    }

    private void IsLocked()
    {
        if (levelIndex > UnlockedLevels.GetHighestUnlockedLevel())
        {
            GetComponent<Button>().interactable = false;
            lockPanel.SetActive(true);
        }
    }
}
