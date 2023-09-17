using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    private bool spent = false;
    [SerializeField] bool oneShot;
    [SerializeField] UnityEvent enterEvent;
    [SerializeField] UnityEvent exitEvent;

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
