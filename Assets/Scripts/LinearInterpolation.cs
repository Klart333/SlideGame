using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    [SerializeField]
    Vector3 goalPos;

    [SerializeField]
    private float curveAmount;

    [SerializeField]
    private float speed;

    private float time = 0;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        Vector3 curvePos = new Vector3(goalPos.x * curveAmount, goalPos.y, (startPos.z + (goalPos.z - startPos.z) / 2));

        time += (Time.deltaTime / 10) * speed;
        time = Mathf.Clamp01(time);
        transform.position = Mathf.Pow(1 - time, 2) * startPos + 2 * (1 - time) * time * curvePos + Mathf.Pow(time, 2) * goalPos;
    }
}
