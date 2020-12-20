using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinnn : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    void Start()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        while (true)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + (1 * speed), 0));
            yield return null;
        }
    }
}
