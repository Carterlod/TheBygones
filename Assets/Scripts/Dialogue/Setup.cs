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
    
    public List<Transform> carryIntoNextScene;
    

    private void Start()
    {
        Debug.Log("Setup Start() was called on " + this.name) ;
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
