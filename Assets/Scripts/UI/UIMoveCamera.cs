using System.Collections;
using UnityEngine;

public class UIMoveCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraPosition;

    [SerializeField]
    private Vector3 cameraRotation;

    private new GameObject camera;
    private CameraManager cameraManager;

    private float time = 1.5f;
    private int iterations = 100;

    private void OnEnable()
    {
        camera = Camera.main.gameObject;
        cameraManager = FindObjectOfType<CameraManager>();
    }

    public void OnButton()
    {
        if (!cameraManager.onTheMove)
        {
            cameraManager.onTheMove = true;
            StartCoroutine("MoveCamera");
        }        
    }

    private IEnumerator MoveCamera()
    {
        Vector3 positionDelta = cameraPosition - camera.transform.position;

        for (int i = 0; i < iterations; i++)
        {
            camera.transform.position += positionDelta / iterations;
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, Quaternion.Euler(cameraRotation), time * 2.5f / iterations); // Jag fattar inte hur tiden funkar, skulle uppskatta om du kunde förklara :P

            yield return new WaitForSeconds(time / iterations);
        }

        cameraManager.onTheMove = false;
    }
}
