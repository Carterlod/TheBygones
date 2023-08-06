using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabOnEnabled : MonoBehaviour
{
    [SerializeField] ObjectGrabber grabber;
    [SerializeField] Grabbable obj;

    private void Start()
    {
        PlayerSettings.i.handsFull = true;
        grabber.Grab(obj);
    }
}