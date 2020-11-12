using UnityEngine;
using UnityEngine.SceneManagement;
public class UILoadScene : MonoBehaviour
{
    [SerializeField]
    public int sceneToLoadIndex = 0;

    [SerializeField]
    [Header("Optional")]
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
