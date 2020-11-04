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

        print(Grounded());
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
