using UnityEngine;

public class AddColliderOnPlay : MonoBehaviour
{
    [SerializeField]
    private PhysicMaterial physicMaterial;

    [SerializeField]
    private bool addManually = true;

    void Start()
    {
        if (!addManually)
        {
            AddCollider();
        }
        
    }

    public void AddCollider()
    {
        Collider collider = gameObject.AddComponent<MeshCollider>();
        collider.material = physicMaterial;
    }
    
}
