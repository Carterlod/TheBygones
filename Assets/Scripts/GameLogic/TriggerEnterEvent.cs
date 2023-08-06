using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    [SerializeField] UnityEvent enterEvent;
    [SerializeField] UnityEvent exitEvent;
    [SerializeField] bool oneShot;
    private bool spent = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !spent)
        {
            enterEvent.Invoke();
            if (oneShot)
            {
                spent = true;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && !spent)
        {
            exitEvent.Invoke();
            if (oneShot)
            {
                spent = true;
            }
        }
    }
}
