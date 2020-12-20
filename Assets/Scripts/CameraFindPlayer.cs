using UnityEngine;
using Cinemachine;

public class CameraFindPlayer : MonoBehaviour
{
    [SerializeField]
    private bool isCustomOffset = false;

    [SerializeField]
    private Vector3 customOffset = new Vector3(0, 4.5f, -5);

    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer vcamTransposer;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcamTransposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        if (isCustomOffset)
        {
            vcamTransposer.m_FollowOffset = customOffset;
        }
        else
        {
            vcamTransposer.m_FollowOffset = new Vector3(0, 4.5f, -5);
        }
    }

    private void Start()
    {
        vcam.Follow = FindObjectOfType<PlayerController>().transform;
        vcam.LookAt = FindObjectOfType<PlayerController>().transform;
    }
}
