using UnityEngine;
using UnityEngine.SceneManagement;
public class UILoadScene : MonoBehaviour
{
    [Header("Pick One")]
    [SerializeField]
    public int sceneToLoadIndex = 0;

    [SerializeField]
    private string sceneToLoadName = "";
    public void OnButtonClicked()
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
