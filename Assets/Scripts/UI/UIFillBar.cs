using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFillBar : MonoBehaviour
{
    private Image bar;

    private float fillSpeed = 1f;

    private void Awake()
    {
        bar = transform.GetChild(1).GetComponent<Image>(); // Will always be the latter child becuase that has to render above the empty
    }

    public void FillBarToTarget(float targetPercentage)
    {
        StartCoroutine(FillBar(targetPercentage));
    }

    private IEnumerator FillBar(float targetPercentage)
    {
        float increment = (float)0.01f * (float)fillSpeed * (float)(bar.fillAmount < targetPercentage? 1f : -1f); // The FLOATS kept round to 1
        while (bar.fillAmount < targetPercentage)
        {
            bar.fillAmount += increment;
            yield return null;
        }
    }
}
