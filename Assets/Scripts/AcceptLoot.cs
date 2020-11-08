using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptLoot : MonoBehaviour
{
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

            Destroy(gameObject);
        }
    }
}
