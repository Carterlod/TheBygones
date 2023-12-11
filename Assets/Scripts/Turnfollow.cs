using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnfollow : MonoBehaviour
{
    [SerializeField] bool flip = false;
    private void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform);
        if (flip)
        {
            transform.forward = -transform.forward;
        }
    }
}
