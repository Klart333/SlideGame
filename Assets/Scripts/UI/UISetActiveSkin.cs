using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UISetActiveSkin : MonoBehaviour
{
    public string skin;

    public void SetActiveSkin()
    {
        SaveSkin.SetActiveSkin(skin);
    }
}
