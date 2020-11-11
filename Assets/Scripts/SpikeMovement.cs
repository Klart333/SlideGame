using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour, IHittable
{
    [SerializeField]
    private float movementSpeed = 5f;

    private float hitSpeed = 100;

    private new Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rigidbody.velocity = new Vector3(Mathf.Sin(Time.time), 0, 0) * movementSpeed;
    }

    public void Hit(Transform hitBy)
    {
        rigidbody.velocity = -hitBy.forward * hitSpeed;
    }
}
