using System.Collections;
using UnityEngine;

public class UIMoveCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPosition;

    [SerializeField]
    private Vector3 targetRotation;

    [SerializeField]
    private float speed = 1;

    private new GameObject camera;
    private CameraManager cameraManager;

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
        Vector3 originalPosition = camera.transform.position;

        Quaternion originalRotation = camera.transform.rotation;

        float t = 0;

        while (t <= 1)
        {
            camera.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            camera.transform.rotation = Quaternion.Lerp(originalRotation, Quaternion.Euler(targetRotation), t);

            t += Time.deltaTime * speed;

            yield return null;
        }
        camera.transform.position = Vector3.Lerp(originalPosition, targetPosition, 1);
        camera.transform.rotation = Quaternion.Lerp(originalRotation, Quaternion.Euler(targetRotation), 1);

        cameraManager.onTheMove = false;
    }
}
