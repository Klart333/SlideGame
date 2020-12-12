using UnityEngine;
using UnityEngine.UI;
public class UISetActiveSkin : MonoBehaviour
{
    public Skin skin;

    public void SetActiveSkin()
    {
        SaveSkin.SetActiveSkin(skin);
        GetComponentInParent<UIDisplaySelectedSkin>().SetSelected(GetComponent<Image>());
    }
}
