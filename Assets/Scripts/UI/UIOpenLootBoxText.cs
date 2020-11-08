using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIOpenLootBoxText : MonoBehaviour
{
    [SerializeField]
    private string fillerText;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    private void Start()
    {
        FindObjectOfType<SpawnLootBox>().OnLootBoxOpened += UIOpenLootBoxText_OnLootBoxOpened;
    }

    private void UIOpenLootBoxText_OnLootBoxOpened()
    {
        UpdateText();
    }
    private void UpdateText()
    {
        text.text = string.Format("{0} ({1})", fillerText, LootBoxAmount.GetLootBoxAmount());
    }

}
