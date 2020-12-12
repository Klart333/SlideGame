using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class WallCreator : MonoBehaviour 
{
    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    [Range(0.05f, 1.5f)]
    private float spacing = 1;
    [SerializeField]
    private float roadWidth = 1;
    [SerializeField]
    private float tiling = 1;

    [SerializeField]
    private float wallHeight = 1;

    public bool autoUpdate;

    private void Start()
    {
        UpdateRoad();
        meshFilter.gameObject.GetComponent<AddColliderOnPlay>().AddCollider();
    }

    public void UpdateRoad()
    {
        Path path = FindObjectOfType<PathCreator>().path;
        Vector2[] points = path.CalculateEvenlySpacedPoints(spacing); // The points on the bezier curve

        meshFilter.sharedMesh = CreateWallMesh(points).CreateMesh();

        // Changes the tiling of the texture already on the mesh
        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * 0.05f);
        meshRenderer.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);

    }

    MeshData CreateWallMesh(Vector2[] points)
    {
        Vector3[] verts = new Vector3[points.Length * 4];
        Vector2[] uvs = new Vector2[verts.Length];
        int numTris = 12 * (points.Length - 1);
        int[] tris = new int[numTris * 3]; // Holds the index position of the triangles of the verts array

        MeshData meshData = new MeshData(verts, uvs, tris);

        int vertIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 forward = Vector2.zero;

            if (i < points.Length - 1)
            {
                forward += points[(i + 1) % points.Length] - points[i];
            }
            if (i > 0)
            {
                forward += points[i] - points[(i - 1 + points.Length) % points.Length];
            }
            forward.Normalize();

            Vector2 left = new Vector2(-forward.y, forward.x);

            verts[vertIndex] = new Vector3(points[i].x - left.x * roadWidth * 0.5f, transform.position.y, points[i].y - left.y * roadWidth * 0.5f);
            verts[vertIndex + 2] = new Vector3(points[i].x - left.x * roadWidth * 0.5f, transform.position.y + wallHeight, points[i].y - left.y * roadWidth * 0.5f);
            verts[vertIndex + 1] = new Vector3(points[i].x + left.x * roadWidth * 0.5f, transform.position.y, points[i].y + left.y * roadWidth * 0.5f);
            verts[vertIndex + 3] = new Vector3(points[i].x + left.x * roadWidth * 0.5f, transform.position.y + wallHeight, points[i].y + left.y * roadWidth * 0.5f);

            float completionPercent = i / (float)(points.Length - 1);
            float v = 1 - Mathf.Abs(2 * completionPercent - 1);
            uvs[vertIndex] = new Vector2(0, v);
            uvs[vertIndex + 1] = new Vector2(1, v);

            if (i < points.Length - 1) // As long as were not on the last point, Because we add the tris from this point to the next one
            {
                // Clockwise and Counter-Clockwise
                meshData.AddTriangle(vertIndex, vertIndex + 4, vertIndex + 2);
                meshData.AddTriangle(vertIndex, vertIndex + 2, vertIndex + 4);
                meshData.AddTriangle(vertIndex + 2, vertIndex + 6, vertIndex + 4);
                meshData.AddTriangle(vertIndex + 2, vertIndex + 4, vertIndex + 6);

                meshData.AddTriangle(vertIndex + 1, vertIndex + 5, vertIndex + 3);
                meshData.AddTriangle(vertIndex + 1, vertIndex + 3, vertIndex + 5);
                meshData.AddTriangle(vertIndex + 3, vertIndex + 5, vertIndex + 7);
                meshData.AddTriangle(vertIndex + 3, vertIndex + 7, vertIndex + 5);
            }

            vertIndex += 4;
        }

        meshData.uvs = uvs;
        meshData.vertices = verts;

        return meshData;
    }
}
