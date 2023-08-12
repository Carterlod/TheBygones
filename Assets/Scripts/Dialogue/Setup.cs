using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [System.Serializable]
    public class LightSwitchPreset
    {
        public LightSwitch lightSwitch;
        public bool startOn = true;
    }
    [SerializeField] LightSwitchPreset[] lightSwitches;
    [SerializeField] ConversationSwitcher convoSwitcher;
    [SerializeField] bool fireFirstConvoOnEnable = true;
    [SerializeField] bool startCrouched = false;
    [SerializeField] FirstPersonController playerController;
    

    private void Start()
    {
        if (fireFirstConvoOnEnable)
        {
            convoSwitcher.BeginFirstConversation();
        }
    }

    private void OnEnable()
    {
        if (startCrouched)
        {
            playerController.Crouch();
        }
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
        playerController.isCrouched = false;
    }
    
}
