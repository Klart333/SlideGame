using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGoalStretch : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) // Här kan du implementera polymorphism
    {
        if (collision.GetComponent<PlayerController>())
        {
            var cinematicMode = FindObjectOfType<CinematicMode>();
            cinematicMode.StartCoroutine("CinematicCamera");

            GameManager.Instance.isOnGoalStretch = true;
        }
    }
}
