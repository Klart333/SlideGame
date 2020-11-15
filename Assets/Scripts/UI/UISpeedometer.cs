using UnityEngine;
using TMPro;
public class UISpeedometer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Rigidbody playerRigidBody;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        playerRigidBody = FindObjectOfType<PlayerController>().GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerRigidBody != null)
        {
            text.text = ((float)Mathf.Round(playerRigidBody.velocity.magnitude * 10f) / 10f).ToString() + "m/s"; // Gets the magintude, multiplies by ten, then rounds it to a integer, then we divide by 10. This is so we have a single decimal
        }
    }
}
