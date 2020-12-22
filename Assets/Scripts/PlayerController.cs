using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float rayDistance = 0.5f;

    private float slideSpeed;
    private float airSpeed;
    private float reverseSpeed;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        slideSpeed = GetSkin.GetActiveSkin().slideSpeed;
        airSpeed = GetSkin.GetActiveSkin().airSpeed;
        reverseSpeed = GetSkin.GetActiveSkin().reverseSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.isOnGoalStretch)
        {
            rigidbody.drag = 0;
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.acceleration.z < -0.1f)
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
        else if (Input.GetKey(KeyCode.S) || Input.acceleration.z > 0.1f)
        {
            MovePlayer(reverseSpeed);
        }
    }

    private void MovePlayer(float speed)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            rigidbody.velocity += transform.forward *  Mathf.Abs(Mathf.Clamp(Input.acceleration.z * 1.5f, -1, 1)) * speed * Time.deltaTime;
        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            rigidbody.velocity += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            Debug.LogError("Shit outta luck");
        }
    }

    private bool Grounded()
    {
        bool rayHit = Physics.Raycast(transform.position, Vector3.down + transform.forward, rayDistance, layerMask);
        return rayHit;
    }
}
