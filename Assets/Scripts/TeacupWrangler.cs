using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacupWrangler : MonoBehaviour
{
    public Tea[] teacups;
    private void Start()
    {
        teacups = GetComponentsInChildren<Tea>();
    }

    public void ActivateAllInteractables()
    {
        //teacups = GetComponentsInChildren<Tea>();
        Vessel kettle = PlayerSettings.i.objectGrabber.heldObject.GetComponent<Vessel>();
        if(kettle.fillLevel >= kettle.fillThreshold)
        {
            foreach(Tea teacup in teacups)
            {
                if (!teacup.full)
                {
                    teacup.interactable.gameObject.SetActive(true) ;
                }
            }
        }
    }

    public void DeactivateAllInteractables()
    {
        foreach(Tea teacup in teacups)
        {
            teacup.interactable.gameObject.SetActive(false);
        }
    }
}