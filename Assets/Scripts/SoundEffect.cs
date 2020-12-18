using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour
{
    [SerializeField]
    private SimpleAudioEvent simpleAudioEvent;

    [SerializeField]
    private bool playOnAwake;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (playOnAwake)
        {
            PlaySoundEffext();
        }
    }

    public void PlaySoundEffext()
    {
        simpleAudioEvent.Play(audioSource);
    }
}
