using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings i;
    [SerializeField] FirstPersonController playerController;
    [SerializeField] Interactor interactor;
    public bool dialogueAdvanceable = true;
    public bool handsFull = false;
    public bool cameraActive = false;

    public void OnEnable()
    {
        i = this;
    }
    public  void PausePlayer()
    {
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;
        interactor.playerPaused = false;
        dialogueAdvanceable = false;
        
    }
    public  void UnpausePlayer()
    {
        playerController.playerCanMove = true;
        playerController.cameraCanMove = true;
        interactor.playerPaused = true;
        dialogueAdvanceable = true;
    }
}
