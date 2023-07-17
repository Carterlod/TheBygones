using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSwitcher : MonoBehaviour
{
    [SerializeField] GameObject[] setups;
    private int activeSetup;

    private void Start()
    {
        activeSetup = 0;
        setups[activeSetup].gameObject.SetActive(true);
    }
    public void IncrementSetup()
    {
        setups[activeSetup].gameObject.SetActive(false);
        activeSetup++;
        if (activeSetup > setups.Length - 1)
        {
            return;
        }
        setups[activeSetup].gameObject.SetActive(true);
    }
}
