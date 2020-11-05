using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UISpeedometer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerController playerController;
    private Rigidbody playerRigidBody;


    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        playerController = FindObjectOfType<PlayerController>();

        playerRigidBody = playerController.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        text.text = ((float)Mathf.Round(playerRigidBody.velocity.magnitude * 10f) / 10f).ToString() + "m/s"; // Gets the magintude, multiplies by ten, then rounds it to a integer, then we divide by 10. This is so we have a single decimal
    }
}
