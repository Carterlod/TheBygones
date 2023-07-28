using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Rigidbody rb;
    public Transform grabHandle;
    public Vector3 originalPos;
    public Quaternion originalRot;
    public bool keepLevel = true;

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
}
