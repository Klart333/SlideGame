using UnityEngine;
public class UISetActiveSkin : MonoBehaviour
{
    public Skin skin;

    public void SetActiveSkin()
    {
        SaveSkin.SetActiveSkin(skin);
    }
}
