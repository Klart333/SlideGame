using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour, IDestructable
{
    [SerializeField]
    private float movementSpeed = 5f;

    private new Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rigidbody.velocity = new Vector3(Mathf.Sin(Time.time), 0, 0) * movementSpeed;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
