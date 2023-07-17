using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSwitcher : MonoBehaviour
{
    private int activeSetup;
    [System.Serializable]
    public class SetupSettings
    {
        //public bool armDialogueOnEnable = true;
        public Setup setupParent;
    }
    [SerializeField] SetupSettings[] setups;
    private void Awake()
    {
        activeSetup = 0;
        for (int i = 0; i < setups.Length - 1; i++)
        {
            if (setups[i].setupParent.gameObject.activeSelf == true)
            {
                activeSetup = i;
            }
        }
        
        //setups[activeSetup].setupParent.gameObject.SetActive(true);
        
    }
    public void IncrementSetup()
    {
        setups[activeSetup].setupParent.gameObject.SetActive(false);
        activeSetup++;
        if (activeSetup > setups.Length - 1)
        {
            return;
        }
        setups[activeSetup].setupParent.gameObject.SetActive(true);
    }
}
