using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private Interactor interactor;
    public void PausePlayer()
    {
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;
        interactor.allowed = false;
    }
    public void UnpausePlayer()
    {
        playerController.playerCanMove = true;
        playerController.cameraCanMove = true;
        interactor.allowed = true;
    }
}
