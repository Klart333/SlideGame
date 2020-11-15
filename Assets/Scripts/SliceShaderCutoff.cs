using System.Collections.Generic;
using UnityEngine;

public class SliceShaderCutoff : MonoBehaviour
{
    public List<GameObject> graphics;

    private List<Material> materials = new List<Material>();

    public void UpdateMaterials()
    {
        foreach (var graphic in graphics)
        {
            foreach (var material in GetMaterials(graphic))
            {
                materials.Add(material);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetVector("sliceCentre", transform.position);
            materials[i].SetVector("sliceNormal", transform.forward);
        }
    }

    private List<Material> GetMaterials(GameObject graphic)
    {
        List<MeshRenderer> renderers = new List<MeshRenderer>();

        if (graphic.GetComponent<MeshRenderer>() != null)
        {
            renderers.Add(graphic.GetComponent<MeshRenderer>());
        }

        foreach (var meshR in graphic.GetComponentsInChildren<MeshRenderer>())
        {
            renderers.Add(meshR);
        }

        List<Material> matList = new List<Material>();

        foreach (var renderer in renderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat != null)
                {
                    matList.Add(mat);
                }
            }
        }

        return matList;
    }
}
