using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [Header("Conversation")]
    [SerializeField] ConversationSwitcher convoSwitcher;
    [SerializeField] bool fireFirstConvoOnEnable = true;
    [Header("Player")]
    [SerializeField] FirstPersonController playerController;
    private PlayerSettings playerSettings;
    [SerializeField] bool startCrouched = false;
    public bool allowPlayerMovement = true;

    [System.Serializable]
    public class LightSwitchPreset
    {
        public LightSwitch lightSwitch;
        public bool startOn = true;
    }
    [SerializeField] LightSwitchPreset[] lightSwitches;
    
    public List<Transform> carryIntoNextScene;
    

    private void Start()
    {
        //Debug.Log("Setup Start() was called on " + this.name) ;
        playerSettings = playerController.GetComponent<PlayerSettings>();

        if (fireFirstConvoOnEnable)
        {
            convoSwitcher.BeginFirstConversation();
        }
        if (startCrouched)
        {
            if (playerController.isCrouched)
            {
                return;
            }
            else
            {
                playerController.Crouch();
            }
            
        }
        else
        {
            if (playerController.isCrouched)
            {
                playerController.Crouch();
            }
            else
            {
                return;
            }
        }
        if (allowPlayerMovement)
        {
            playerController.playerCanMove = true;
        }
        else
        {
            playerController.playerCanMove = false;
        }
        playerSettings.UnpausePlayer();
    }

    private void OnEnable()
    {
        foreach (LightSwitchPreset lsp in lightSwitches)
        {
            if (lsp.startOn)
            {
                lsp.lightSwitch.TurnOn(false);
            }
            else
            {
                lsp.lightSwitch.TurnOff(false);
            }
        }
        playerController.isZoomed = false;
    }
}
