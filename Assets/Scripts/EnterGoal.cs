using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterGoal : MonoBehaviour
{
    [SerializeField]
    private GameObject celebrationParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            GameManager.Instance.savedVelocity = other.GetComponent<Rigidbody>().velocity.z;
            celebrationParticles.SetActive(true);

            StartCoroutine("WaitBeforeChangeScene");
        }
    }


    private IEnumerator WaitBeforeChangeScene()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(2);
    }

}
