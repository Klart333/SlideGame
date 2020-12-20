using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class UILoadScene : MonoBehaviour
{
    [Header("Pick One")]
    [SerializeField]
    public int sceneToLoadIndex = 0;

    [SerializeField]
    private string sceneToLoadName = "";

    [SerializeField]
    private SimpleAudioEvent simpleAudioEvent;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClicked()
    {
        simpleAudioEvent.Play(audioSource);
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
