using UnityEngine;
using Cinemachine;

public class CameraFindPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer vcamTransposer;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcamTransposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        vcamTransposer.m_FollowOffset = new Vector3(0, 4.5f, -5);
    }

    private void Start()
    {
        vcam.Follow = FindObjectOfType<PlayerController>().transform;
        vcam.LookAt = FindObjectOfType<PlayerController>().transform;
    }
    /*private void OnEnable()
    {
        print("Borde vara före hej");
        //GameManager.Instance.OnplayerInitiated += Instance_OnplayerInitiated;
    }

    private void Instance_OnplayerInitiated(GameObject player)
    {
        vcam.Follow = player.transform;
        vcam.LookAt = player.transform;
    }*/
}
