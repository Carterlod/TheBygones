using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings i;
    [SerializeField] FirstPersonController playerController;
    [SerializeField] Interactor interactor;
    public bool dialogueAdvanceable = true;

    public void Start()
    {
        i = this;
    }
    public  void PausePlayer()
    {
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;
        interactor.allowed = false;
        dialogueAdvanceable = false;
        
    }
    public  void UnpausePlayer()
    {
        playerController.playerCanMove = true;
        playerController.cameraCanMove = true;
        interactor.allowed = true;
        dialogueAdvanceable = true;
    }
}
