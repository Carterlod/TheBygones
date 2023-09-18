using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vessel : MonoBehaviour
{
    [SerializeField] string myFillerKey;
    [SerializeField] float fillThreshold = 1;
    public float fillLevel = 0;
    [SerializeField] UnityEvent thresholdEvent;
    private bool full = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<VesselFiller>() && !full)
        {
            VesselFiller filler = other.GetComponent<VesselFiller>();
            if(filler.fillKey == myFillerKey)
            {
                fillLevel += Time.deltaTime * filler.fillSpeed;
            }
            if(fillLevel >= fillThreshold)
            {
                full = true;
                thresholdEvent.Invoke();
            }
        }
    }
    public void ResetFill()
    {
        fillLevel = 0;
        full = false;
    }
}
