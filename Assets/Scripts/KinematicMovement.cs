using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class KinematicMovement : MonoBehaviour
{
    [SerializeField]
    private Axis axis;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private bool triggered;

    [SerializeField]
    private float startDelay;

    [SerializeField]
    private bool repeating;

    [SerializeField]
    private float repeatDelay = 0;

    private new Rigidbody rigidbody;

    private float time = 0;
    private bool done = false;
    private void Start()
    {
        time -= startDelay - repeatDelay;

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= repeatDelay && !done && !triggered)
        {
            time = 0;
            done = !repeating;

            if (axis == Axis.x)
            {
                rigidbody.velocity = Vector3.right * speed;
            }
            else if (axis == Axis.y)
            {
                rigidbody.velocity = Vector3.up * speed;
            }
            else if (axis == Axis.z)
            {
                rigidbody.velocity = Vector3.forward * speed;
            }

            // Switch
            speed = -speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (triggered && collision.gameObject.GetComponent<PlayerController>())
        {
            triggered = false;
        }
    }
}
