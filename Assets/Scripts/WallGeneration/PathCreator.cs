using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    public Color anchorColor = Color.red;
    public Color controlColor = Color.white;
    public Color segmentColor = Color.green;
    public Color selectedSegmentColor = Color.yellow;

    public float anchorDiameter = 0.1f;
    public float controlDiameter = 0.075f;

    public bool displayControlPoints = true;

    public bool autoGeneratePath = true;
    private void Awake()
    {
        CreatePath();
    }
    public void CreatePath()
    {
        path = new Path(transform.position);

        if (autoGeneratePath)
        {
            path.GenerateRandomPath();
        }
    }
}
