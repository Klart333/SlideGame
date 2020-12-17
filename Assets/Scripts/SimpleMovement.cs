using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPosition;

    [SerializeField]
    private Vector3 targetRotaton;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float startDelay;

    [SerializeField]
    public bool repeating;

    [HideInInspector]
    public float repeatDelay = 0;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private float time = 0;
    private bool done = false;
    private bool moving = false;
    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        time -= startDelay - repeatDelay; // Say we don't want to repeat but start with a delay of 3: time = (-3 - 0) = -3, then after 3 seconds time = 0 and the coroutine will start
    }

    private void Update()
    {
        if (!moving)
        {
            time += Time.deltaTime;
        }

        if (time >= repeatDelay && !done)
        {
            done = !repeating;
            time = 0;

            StartCoroutine(Move());
        }
    }

    public IEnumerator Move()
    {
        Quaternion realTargetRotation = Quaternion.Euler(targetRotaton);

        float t = 0;

        moving = true;
        while (t <= 1)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            transform.rotation = Quaternion.Lerp(originalRotation, realTargetRotation, t);

            t += Time.deltaTime * speed;

            yield return null;
        }
        transform.position = Vector3.Lerp(originalPosition, targetPosition, 1);
        transform.rotation = Quaternion.Lerp(originalRotation, realTargetRotation, 1);

        moving = false;

        // Now we switch
        Vector3 savedPosition = originalPosition;
        originalPosition = targetPosition;
        targetPosition = savedPosition;

        Quaternion savedRotation = originalRotation;
        originalRotation = Quaternion.Euler(targetRotaton);
        targetRotaton = savedRotation.eulerAngles;
    }
}
 

