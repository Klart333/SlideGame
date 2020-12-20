using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHonk : SoundEffect
{
    [SerializeField]
    private AudioSource betterAudioSource;

    [SerializeField]
    private float distanceToHonk = 50;

    [SerializeField]
    private float honkCooldown = 20;

    private GameObject player;

    private float timer = 0;
    private bool coolingDown = false;

    private void Awake()
    {
        GameManager.Instance.OnplayerInitiated += Instance_OnplayerInitiated;
    }

    private void Instance_OnplayerInitiated(GameObject obj)
    {
        player = obj;
    }

    private void Update()
    {
        if (coolingDown)
        {
            timer += Time.deltaTime;

            if (timer >= honkCooldown)
            {
                coolingDown = false;
                timer = 0;
            }
            else
            {
                return;
            }
        }

        if (player != null)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < distanceToHonk)
            {
                soundEffect.Play(betterAudioSource);
                coolingDown = true;
            }
        }
    }
}
