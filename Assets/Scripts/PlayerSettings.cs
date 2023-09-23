using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings i;
    public FirstPersonController playerController;
    public Interactor interactor;
    public ObjectGrabber objectGrabber;
    [SerializeField] LetterboxBars stationaryCameraUI;
    public bool dialogueAdvanceable = true;
    public bool handsFull = false;
    public bool cameraActive = false;
    public bool playerPaused = false;
    public bool isHoldingCamera = false;
    public bool isSeated = false;
    public bool playerIsTargettingSitSpot = false;
    public bool targettedInteractableIsHandsFullOK = false;
    public SitSpot sitSpot;
     

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

   

    public void UpdatePlayerUI()
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
