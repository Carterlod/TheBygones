using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    public Transform grabHandle;
    public string objectKey;
    public bool grabbed;
    public bool keepLevel = true;
    [SerializeField] bool bypassSettling = false;
    [SerializeField] UnityEvent onGrab;
    [SerializeField] UnityEvent onRelease;
    public Vector3 originalPos;
    public Quaternion originalRot;

    private Rigidbody rb;
    private Collider[] cols;

    public bool isPerformingAction = false;

    public Vector3 grabbedPos;
    public Quaternion grabbedRot;

    public bool cannotDrop = false;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        cols = gameObject.GetComponentsInChildren<Collider>();
        StartCoroutine(Settle());
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
