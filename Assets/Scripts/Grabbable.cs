using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    public Transform grabHandle;
    public string objectKey;
    public bool keepLevel = true;
    [SerializeField] UnityEvent onGrab;
    [SerializeField] UnityEvent onRelease;
    public Vector3 originalPos;
    public Quaternion originalRot;
    public bool grabbed;

    private Rigidbody rb;
    private Collider[] cols;

    private void OnEnable()
    {
        StartCoroutine(Settle());
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cols = gameObject.GetComponentsInChildren<Collider>();
        
    }
    
    IEnumerator Settle()
    {
        yield return new WaitForSeconds(.5f);
        originalPos = gameObject.transform.position;
        originalRot = gameObject.transform.rotation;
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
