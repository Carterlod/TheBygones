using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSwitcher : MonoBehaviour
{
    public static SetupSwitcher i;
    public int activeSetup;
    [SerializeField] Faucet faucet;
    public NPC speakingNPC;
    public DialogueTester currentConvo;
    

    [System.Serializable]
    public class SetupSettings
    {
        public Setup setupParent;
    }
    public SetupSettings[] setups;

    private void Awake()
    {
        Debug.Log("awake");
        i = this;
        activeSetup = 0;
        for (int i = 0; i < setups.Length; i++)
        {

            if (setups[i].setupParent.gameObject.activeInHierarchy == true)
            {
                activeSetup = i;
            }
        }       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            IncrementSetup();
            if(currentConvo!= null)
            {
                currentConvo.EndDialogue();
            }
        }
    }
    public void IncrementSetup()
    {
        
        Setup oldSet = setups[activeSetup].setupParent;
        
        activeSetup++; // iterate setups
        if (activeSetup == setups.Length)
        {
            activeSetup = activeSetup - 1;
            return;
        }

        if(oldSet.carryIntoNextScene.Count > 0) // carry specified objects into next scene
        {
            foreach (Transform tr in oldSet.carryIntoNextScene)
            {
                tr.parent = this.transform;
            }
        }
        if (PlayerSettings.i.handsFull && !oldSet.keepHeldObject) 
        {
            PlayerSettings.i.objectGrabber.Release(this.transform); //The transform passed here is meaningless. 
        }
        oldSet.gameObject.SetActive(false);

        //clear clean dishes
        if (faucet.on)
        {
            faucet.TurnOff(true);
        }
        DirtyDishes.i.ClearCleanDishes();

        Setup newSet = setups[activeSetup].setupParent;
        newSet.gameObject.SetActive(true);
        if(oldSet.carryIntoNextScene.Count > 0) // return old carried through objects back to their original scene?
        {
            foreach(Transform tr in oldSet.carryIntoNextScene)
            {
                tr.parent = newSet.gameObject.transform;
            }
        }

    }
}
