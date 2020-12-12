using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectOscillation : MonoBehaviour
{
    [SerializeField]
    private Axis axis;

    [SerializeField]
    private float movementSpeed = 0.5f;

    float time = 0;

    private new Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        StartCoroutine("Oscillate", axis);
    }
    private void Update()
    {
        time += Time.deltaTime;
    }

    private IEnumerator Oscillate(Axis axis)
    {
        while (true)
        {
            if (axis == Axis.x)
            {
                rigidbody.velocity = new Vector3(Mathf.Sin(time), 0, 0) * movementSpeed;
            }
            else if (axis == Axis.y)
            {
                rigidbody.velocity = new Vector3(0, Mathf.Sin(time), 0) * movementSpeed;
            }
            else if (axis == Axis.z)
            {
                rigidbody.velocity = new Vector3(0, 0, Mathf.Sin(time)) * movementSpeed;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}

enum Axis
{
    x, 
    y,
    z
}