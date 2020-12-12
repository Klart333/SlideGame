using UnityEngine;
using UnityEngine.UI;

public class UIStatPanelHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject statPanel;

    private UIFillBar slideBar;
    private UIFillBar airBar;
    private UIFillBar reverseBar;

    private void Start()
    {
        slideBar = statPanel.transform.GetChild(1).GetComponent<UIFillBar>();
        airBar = statPanel.transform.GetChild(3).GetComponent<UIFillBar>();
        reverseBar = statPanel.transform.GetChild(5).GetComponent<UIFillBar>();

        statPanel.SetActive(false);
    }

    public void SetPanel(GameObject gameObject, Skin skin)
    {
        statPanel.SetActive(true);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        Vector3 pos = statPanel.GetComponent<RectTransform>().localPosition;
        float skinPanelX = rectTransform.localPosition.x;
        statPanel.GetComponent<RectTransform>().localPosition = new Vector3(skinPanelX, pos.y, pos.z);

        slideBar.FillBarToTarget((float)skin.slideSpeed / (float)GetSkin.maxSlideSpeed); // Can never be too cautious right?
        airBar.FillBarToTarget((float)skin.airSpeed / (float)GetSkin.maxAirSpeed);
        reverseBar.FillBarToTarget((float)skin.reverseSpeed / (float)GetSkin.maxReverseSpeed);
    }

    public void DisablePanel()
    {
        Image[] fillbars = statPanel.GetComponentsInChildren<Image>();

        for (int i = 0; i < fillbars.Length; i++)
        {
            fillbars[i].fillAmount = 0;
        }

        statPanel.SetActive(false);
    }
}
