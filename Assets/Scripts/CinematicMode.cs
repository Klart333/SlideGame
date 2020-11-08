using System.Collections;
using UnityEngine;
using Cinemachine;
public class CinematicMode : MonoBehaviour
{
    [SerializeField]
    private float timeToSwitch = 0.5f;

    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer vcamTransposer;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcamTransposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    public IEnumerator CinematicCamera()
    {
        int iterations = 300;
        float xToMove = 20;
        float yToMove = 0;
        float zToMove = 0 - vcamTransposer.m_FollowOffset.z;

        for (int i = 0; i < iterations; i++)
        {
            vcamTransposer.m_FollowOffset += new Vector3(xToMove / (float)iterations, yToMove / (float)iterations, zToMove / (float)iterations);
            yield return new WaitForSeconds(timeToSwitch / (float)iterations);
        }
    }
}
