using System;
using UnityEngine;

public class EndGamePad : MonoBehaviour
{
    public bool winnerIsStanding = false;
    public GameDevice setDevice;
    public Material assignedMat;

    private void Awake()
    {
        GetComponent<Renderer>().material = assignedMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                if (player.gameDevice == setDevice)
                {
                    winnerIsStanding = true;
                }
            }
        }
    }
}
