using UnityEngine;
using UnityEngine.EventSystems;
public class UISkinPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIStatPanelHandler generateStatPanel;

    private void Start()
    {
        generateStatPanel = FindObjectOfType<UIStatPanelHandler>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        generateStatPanel.SetPanel(this.gameObject, GetComponent<UISetActiveSkin>().skin);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        generateStatPanel.DisablePanel();
    }
}
