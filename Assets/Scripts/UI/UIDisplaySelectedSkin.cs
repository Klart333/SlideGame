using UnityEngine;
using UnityEngine.UI;
public class UIDisplaySelectedSkin : MonoBehaviour
{
    private Image currentPanel;

    public void SetSelected(Image panel)
    {
        if (currentPanel != null)
        {
            currentPanel.color = Color.white;
        }

        panel.color = Color.green;

        currentPanel = panel;
    }
}
