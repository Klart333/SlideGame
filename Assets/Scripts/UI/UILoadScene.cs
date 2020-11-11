using UnityEngine;
using UnityEngine.SceneManagement;
public class UILoadScene : MonoBehaviour
{
    [SerializeField]
    public int sceneToLoadIndex = 0;
    public void OnButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
}
