using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings i;
    [SerializeField] FirstPersonController playerController;
    public Interactor interactor;
    public ObjectGrabber objectGrabber;
    [SerializeField] LetterboxBars stationaryCameraUI;
    public bool dialogueAdvanceable = true;
    public bool handsFull = false;
    public bool cameraActive = false;
    public bool playerPaused = false;
    public bool isHoldingCamera = false;

    public void OnEnable()
    {
        i = this;
    }
    public  void PausePlayer()
    {
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;
        playerPaused = true;
        dialogueAdvanceable = false;
        UpdatePlayerUI();
        
    }
    public  void UnpausePlayer()
    {
        Setup setup = SetupSwitcher.i.setups[SetupSwitcher.i.activeSetup].setupParent;
        if (setup.allowPlayerMovement)
        {
            playerController.playerCanMove = true; 
        }
        playerController.cameraCanMove = true;
        playerPaused = false;
        dialogueAdvanceable = true;
        UpdatePlayerUI();
    }

   

    private void UpdatePlayerUI()
    {
        if (playerController.playerCanMove)
        {
            stationaryCameraUI.HideBars();

        }
        else
        {
            stationaryCameraUI.ShowBars();
        }
    }


    public void HoldingCameraOn()
    {
        isHoldingCamera = true;
    }
    public void HoldingCameraOff()
    {
        isHoldingCamera = false;
    }
}
