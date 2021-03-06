﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UICreateSkinPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skins;

    private TextMeshProUGUI skinText;
    private void Awake()
    {
        skinText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetSelectSkinDisplay(Skin skin)
    {
        GetComponent<UISetActiveSkin>().skin = skin;

        GameObject prefab = null;

        foreach (GameObject item in skins)
        {
            if (skin.name == item.name)
            {
                prefab = item;
                break;
            }
        }

        GameObject gmSkin = Instantiate(prefab, transform);
        gmSkin.transform.localPosition = new Vector3(0, 175, -50);
        gmSkin.transform.localScale = new Vector3(3000, 1500, 3000);

        skinText.text = skin.name;
        skinText.color = new Color(skin.color.r, skin.color.g, skin.color.b, skin.color.a);

        if (skin.name == GetSkin.GetActiveSkin().name)
        {
            GetComponentInParent<UIDisplaySelectedSkin>().SetSelected(GetComponent<Image>());
        }

        FindObjectOfType<SliceShaderCutoff>().graphics.Add(gmSkin);
        FindObjectOfType<SliceShaderCutoff>().UpdateMaterials();
    }
}
