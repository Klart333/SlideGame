using UnityEngine;

public class MeshData
{
    public Vector3[] vertices;
    public Vector2[] uvs;
    public int[] triangleVertices;

    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangleVertices = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }
    public MeshData(Vector3[] vertices, Vector2[] uvs, int[] triangleVertices)
    {
        this.vertices = vertices;
        this.uvs = uvs;
        this.triangleVertices = triangleVertices;
    }

    public void AddTriangle(int vertexA, int vertexB, int vertexC)
    {
        triangleVertices[triangleIndex] = vertexA;
        triangleVertices[triangleIndex + 1] = vertexB;
        triangleVertices[triangleIndex + 2] = vertexC;

        triangleIndex += 3;
    }

    public Mesh CreateMesh() // Creates the shape of the mesh, the texture is set independently
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangleVertices;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }
}