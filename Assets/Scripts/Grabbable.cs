using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Rigidbody rb;
    public Transform grabHandle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
