using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapSound : MonoBehaviour
{
    [SerializeField] int swapTo;
    [SerializeField] int swapFrom;

    private PlayerControllerDungeon playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playerController == null)
            {
                playerController = other.GetComponent<PlayerControllerDungeon>();
            }

            playerController.SwapSoundFootstepSound(swapTo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.SwapSoundFootstepSound(swapFrom);
    }

    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.SwapSoundFootstepSound(swapFrom);
        }
    }
}
