using UnityEngine;
using UnityEngine.SceneManagement;
public class UIRetry : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(1);
    }
}
