using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSwitcher : MonoBehaviour
{
    public static SetupSwitcher i;
    public int activeSetup;
    [SerializeField] Faucet faucet;

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
        for (int i = 0; i < setups.Length - 1; i++)
        {
            if (setups[i].setupParent.gameObject.activeSelf == true)
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
        }
    }
    public void IncrementSetup()
    {

        Setup oldSet = setups[activeSetup].setupParent;
        if(oldSet.carryIntoNextScene.Count > 0)
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

        activeSetup++;
        if (activeSetup > setups.Length - 1)
        {
            return;
        }
        Setup newSet = setups[activeSetup].setupParent;
        newSet.gameObject.SetActive(true);
        if(oldSet.carryIntoNextScene.Count > 0)
        {
            foreach(Transform tr in oldSet.carryIntoNextScene)
            {
                tr.parent = newSet.gameObject.transform;
            }
        }

    }
}
