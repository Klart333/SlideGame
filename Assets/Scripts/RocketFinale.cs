using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
[RequireComponent(typeof(AudioSource))]
public class RocketFinale : MonoBehaviour
{
    [SerializeField]
    private GameObject[] particleSystems;

    [SerializeField]
    private new CinemachineVirtualCamera camera;

    [SerializeField]
    private GameObject rocket;

    [SerializeField]
    private SimpleAudioEvent rocketSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            particleSystems[0].SetActive(true); // The celebration
            GameManager.Instance.savedVelocity = other.GetComponent<Rigidbody>().velocity.magnitude;
            GameManager.Instance.isOnGoalStretch = true;

            StartCoroutine(DelayThenDoTheMeme());
        }
    }

    private IEnumerator DelayThenDoTheMeme()
    {
        camera.Priority = 100;

        yield return new WaitForSeconds(2);

        rocket.GetComponent<SimpleMovement>().StartCoroutine(rocket.GetComponent<SimpleMovement>().Move());
        rocketSFX.Play(GetComponent<AudioSource>());

        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].SetActive(true);
        }


        StartCoroutine(CompleteGame());
    }

    private IEnumerator CompleteGame()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("ScoreScene");
    }
}
