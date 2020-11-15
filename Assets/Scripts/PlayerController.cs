﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    private float rayDistance = 0.4f;
    private float airSpeed = 6f;
    private float slideSpeed = 50f;
    private float reverseSpeed = -20f;

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

        if (Input.GetKey(KeyCode.W))
        {
            if (Grounded())
            {
                MovePlayer(slideSpeed);
            }
            else if (!Grounded())
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
        rigidbody.velocity += transform.forward * speed * Time.deltaTime;
    }

    private bool Grounded()
    {
        bool ray = Physics.Raycast(transform.position, Vector3.down + transform.forward, rayDistance, layerMask);
        Debug.DrawRay(transform.position, Vector3.down + transform.forward, new Color(1, 0, 0, 1), 100);
        return ray;
    }
}
