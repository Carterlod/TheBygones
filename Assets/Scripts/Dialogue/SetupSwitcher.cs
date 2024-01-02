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

    private bool cooldownActive = false;

    private void Update()
    {
        if (Input.GetButtonDown("Skip") && !cooldownActive)
        {
            Debug.Log("skipping scene");
            StartCoroutine(skipButtonCoolDownRoutine());
            IncrementSetup();
            if(currentConvo!= null)
            {
                currentConvo.EndDialogue();
            }
        }
    }

    IEnumerator skipButtonCoolDownRoutine()
    {
        cooldownActive = true;
        float t = 0;
        while (t < .25f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        cooldownActive = false;
        yield return null;
    }
    public void IncrementSetup()
    {
        Debug.Log("incrementing setup");
        Setup oldSet = setups[activeSetup].setupParent;
        
        activeSetup++; // iterate setups
        if (activeSetup == setups.Length)
        {
            Debug.Log("setting setup int back to 0");
            //activeSetup = activeSetup - 1;
            //return;
            activeSetup = 0;
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
