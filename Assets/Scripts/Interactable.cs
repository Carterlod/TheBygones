using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent interactEvent;
    public bool objectRequired = false;
    public string objectKey;
    public bool oneShot = false;
    public bool spent = false;

    public void Interact()
    {

        interactEvent.Invoke();
        if (oneShot)
        {
            spent = true;
        }
    }
}
