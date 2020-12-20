using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        Vector3 ogScale = transform.localScale;
        Vector3 targetScale = new Vector3(1, 1, 1);

        float t = 0;
        float speed = 1;

        while (t < 1)
        {
            t += Time.deltaTime * speed;
            transform.localScale = Vector3.Lerp(ogScale, targetScale, t);
            yield return null;
        }
        transform.localScale = Vector3.Lerp(ogScale, targetScale, 1);
    }
}
