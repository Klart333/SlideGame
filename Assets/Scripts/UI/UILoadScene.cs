using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class UILoadScene : MonoBehaviour
{
    [Header("Pick One")]
    [SerializeField]
    public int sceneToLoadIndex = 0;

    [SerializeField]
    private string sceneToLoadName = "";

    public void OnButtonClicked()
    {
        FindObjectOfType<FadePanel>().StartCoroutine(FindObjectOfType<FadePanel>().FadeOut(this));
    }

    public void ChangeScene()
    {
        if (sceneToLoadName == "")
        {
            SceneManager.LoadScene(sceneToLoadIndex);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoadName);
        }
    }
}
