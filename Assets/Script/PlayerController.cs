using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 0.1f;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float airSpeed = 5f;
    [SerializeField]
    private float slideSpeed = 10f;
    [SerializeField]
    private float reverseSpeed = -5f;
    [SerializeField]
    private float rotationSpeed = 1;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.Instance.isOnGoalStretch)
        {
            rigidbody.drag = 0;
            return;
        }

        if (!Grounded())
        {
            if (transform.eulerAngles.x <= 45f)
            {
                print("Rotating");
                transform.rotation = Quaternion.Euler(transform.rotation.x + (1f * rotationSpeed), transform.rotation.y, transform.rotation.z);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Grounded())
            {
                MovePlayer(slideSpeed);
            }
            else
            {
                MovePlayer(airSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(reverseSpeed);
        }
    }

    private void MovePlayer(float speed)
    {
        rigidbody.velocity += transform.forward * speed;
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position - new Vector3(0, transform.localScale.y / 2, 0), Vector3.down, rayDistance, layerMask);
    }
}
