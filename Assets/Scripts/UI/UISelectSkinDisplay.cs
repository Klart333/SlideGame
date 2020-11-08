using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectSkinDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject skinPanel;

    private Vector2 anchorMin;
    private Vector2 anchorMax;

    public void GenerateMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        anchorMin = new Vector2(0.02f, 0.05f);
        anchorMax = new Vector2(0.252f, 0.95f);

        foreach (string skin in GetSkin.GetAllUnlockedSkins())
        {
            GameObject gmSkinPanel = Instantiate(skinPanel, transform);

            gmSkinPanel.GetComponent<RectTransform>().anchorMin = anchorMin;
            gmSkinPanel.GetComponent<RectTransform>().anchorMax = anchorMax;
            gmSkinPanel.GetComponent<UICreateSkinPanel>().SetSelectSkinDisplay(skin);

            anchorMin += new Vector2(0.27f, 0);
            anchorMax += new Vector2(0.27f, 0);
        }
    }
}
