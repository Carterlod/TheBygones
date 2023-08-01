using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    public Rigidbody rb;
    public Transform grabHandle;
    public Vector3 originalPos;
    public Quaternion originalRot;
    public bool keepLevel = true;
    public string objectKey;
    [SerializeField] UnityEvent onGrab;
    [SerializeField] UnityEvent onRelease;

    private void OnEnable()
    {
        StartCoroutine(Settle());
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    IEnumerator Settle()
    {
        yield return new WaitForSeconds(.5f);
        originalPos = gameObject.transform.position;
        originalRot = gameObject.transform.rotation;
    }

    public void Grabbed()
    {
        onGrab.Invoke();
    }

    public void Released()
    {
        onRelease.Invoke();
    }
}
