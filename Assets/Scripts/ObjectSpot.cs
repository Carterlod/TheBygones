using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpot : MonoBehaviour
{
    public Transform setDownSpot;
    private ObjectGrabber grabber;
    public bool overrideTransform = false;
    private void Start()
    {
        grabber = PlayerSettings.i.objectGrabber;
    }
    public void SetDown()
    {
        grabber.heldObject.SetSettleSpot(setDownSpot);
        grabber.Release(this.transform);
    }
}
