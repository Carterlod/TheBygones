using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    
    public bool oneShot = false;
    public bool spent = false;
    [SerializeField] UnityEvent interactEvent;

    [Header("Key Object")]
    public bool objectRequired = false;
    public string objectKey;
    public Grabbable objectLastUsed;

    [Header("Name")]
    public bool hasName = false;
    public string displayedName;



    public void Interact()
    {
        interactEvent.Invoke();
        if (oneShot)
        {
            spent = true;
        }
    }

    public void InteractWithObject(Grabbable grabbedObj)
    {
        interactEvent.Invoke();
        objectLastUsed = grabbedObj;
        if (oneShot)
        {
            spent = true;
        }
    }
}
