using UnityEngine;

public class EnterGoalStretch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            var cinematicMode = FindObjectOfType<CinematicMode>();
            cinematicMode.StartCoroutine("CinematicCamera");

            GameManager.Instance.isOnGoalStretch = true;
        }
    }
}
