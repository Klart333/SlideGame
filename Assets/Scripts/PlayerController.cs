using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float rayDistance = 0.5f;

    [SerializeField]
    private float airSpeed = 6f;
    [SerializeField]
    private float slideSpeed = 50f;
    [SerializeField]
    private float reverseSpeed = -15f;

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
        bool rayHit = Physics.Raycast(transform.position, Vector3.down + transform.forward, rayDistance, layerMask);
        return rayHit;
    }
}
