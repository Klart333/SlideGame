using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 0.1f;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float airSpeed = 2.5f;
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

        if (Input.GetKey(KeyCode.W))
        {
            if (Grounded())
            {
                MovePlayer(slideSpeed);
            }
            else if (!Grounded())
            {
                if (transform.rotation.x < 15f)
                {
                    //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(15, transform.rotation.y, transform.rotation.x), 0.1f);
                }

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
        return Physics.Raycast(transform.position, Vector3.down, rayDistance, layerMask);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<IDestructable>() != null)
        {
            collision.collider.GetComponent<IDestructable>().Destroy();
        }
    }
}
