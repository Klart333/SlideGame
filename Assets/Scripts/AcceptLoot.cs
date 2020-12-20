using UnityEngine;

public class AcceptLoot : MonoBehaviour
{
    [HideInInspector]
    public GameObject lootbox;

    private CameraManager cameraManager;

    private GameObject lootBoxCanvas;
    private GameObject moveCamera;


    private void Start()
    {
        lootBoxCanvas = GameObject.Find("LootBoxCanvas");
        moveCamera = GameObject.Find("OpenLootBoxProxy");

        cameraManager = FindObjectOfType<CameraManager>();
    }
    private void Update()
    {
        if (Input.anyKeyDown && !cameraManager.onTheMove)
        {
            for (int i = 0; i < lootBoxCanvas.transform.childCount; i++)
            {
                lootBoxCanvas.transform.GetChild(i).gameObject.SetActive(true);
            }

            moveCamera.GetComponent<UIMoveCamera>().OnButton();

            Destroy(lootbox);
            Destroy(gameObject);
        }
    }
}
