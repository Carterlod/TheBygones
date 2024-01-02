using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    public Transform grabHandle;
    public string objectKey;
    [System.NonSerialized] public bool grabbed;
    public bool keepLevel = true;
    [SerializeField] bool bypassSettling = false;
    [System.NonSerialized] public bool isPerformingAction = false;
    [System.NonSerialized] public bool cannotDrop = false;
    //public bool dropOnLetGo = false;

    [SerializeField] UnityEvent onGrab;
    [SerializeField] UnityEvent onRelease;
    [System.NonSerialized] public Vector3 originalPos;
    [System.NonSerialized] public Quaternion originalRot;

    private Rigidbody rb;
    private Collider[] cols;


    [System.NonSerialized] public Vector3 grabbedPos;
    [System.NonSerialized] public Quaternion grabbedRot;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        cols = gameObject.GetComponentsInChildren<Collider>();
        StartCoroutine(Settle());
    }
    public void Resettle()
    {
        StartCoroutine(ResettleRoutine());
    }
    IEnumerator ResettleRoutine()
    {
        yield return new WaitForSeconds(.5f);
        originalPos = gameObject.transform.position;
        originalRot = gameObject.transform.rotation;
        yield return null;
    }

    public void SetSettleSpot(Transform spot)
    {
        originalPos = spot.position;
        originalRot = spot.rotation;
    }
    IEnumerator Settle()
    {
        if(bypassSettling)
        {
            originalPos = transform.position;
            originalRot = transform.rotation;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            originalPos = gameObject.transform.position;
            originalRot = gameObject.transform.rotation;
        }
        yield return null;
    }

    
    
    

    public void Grabbed()
    {

        grabbed = true;
        onGrab.Invoke();
        rb.isKinematic = true;
        foreach(Collider col in cols)
        {
            col.enabled = false; ;
        }
        grabbedPos = gameObject.transform.localPosition;
        grabbedRot = gameObject.transform.localRotation;
    }

    public void Released()
    {
        grabbed = false;
        onRelease.Invoke();
        rb.isKinematic = false;
        foreach (Collider col in cols)
        {
            col.enabled = true ;
        }
    }
}
