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
            Quaternion newRot = gameObject.transform.rotation;
            newRot.y += 180;
            gameObject.transform.rotation = newRot;
        }
    }
}
