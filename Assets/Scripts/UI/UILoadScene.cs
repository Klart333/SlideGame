using UnityEngine;
using UnityEngine.SceneManagement;
public class UILoadScene : MonoBehaviour
{
    [SerializeField]
    private int sceneToLoadIndex = 0;
    public void OnButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
}
