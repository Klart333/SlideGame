using System.Collections;
using UnityEngine;
using Cinemachine;
public class CinematicMode : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.5f;

    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer vcamTransposer;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcamTransposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    public IEnumerator CinematicCamera()
    {
        float t = 0;
        Vector3 originalPosition = vcamTransposer.m_FollowOffset;
        Vector3 targetPosition = originalPosition + new Vector3(30, 0, -originalPosition.z);

        while (t <= 1)
        {
            vcamTransposer.m_FollowOffset = Vector3.Lerp(originalPosition, targetPosition, t);
            t += Time.deltaTime * speed;

            yield return null;
        }
    }
}
