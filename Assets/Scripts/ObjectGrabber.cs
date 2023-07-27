using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] private Transform holdTransform;
    private Grabbable heldObject;
    private Transform originalParent;
    private Transform originalTransform;
    private Quaternion originalRot;
    private Vector3 originalPos;
    [SerializeField] Transform camTransform;
    private Collider[] cols;

    public void Grab(Grabbable obj)
    {
        heldObject = obj;
        originalParent = heldObject.transform.parent;
        originalTransform = heldObject.gameObject.transform;
        originalRot = originalTransform.rotation;
        originalPos = originalTransform.position;
        obj.gameObject.transform.SetParent(holdTransform);
        //obj.rb.isKinematic = true;
        obj.rb.Sleep();
        cols = obj.GetComponents<Collider>();
        foreach(Collider col in cols)
        {
            col.enabled = false;
        }
    }
    public void Release()
    {
        heldObject.gameObject.transform.position = originalPos;
        heldObject.gameObject.transform.rotation = originalRot;

        heldObject.gameObject.transform.SetParent(originalParent);


        heldObject.rb.WakeUp();
        foreach(Collider col in cols)
        {
            col.enabled = true;
        }
        cols = null;
        heldObject = null;
    }

    private void LateUpdate()
    {
        holdTransform.localRotation = Quaternion.Euler(-camTransform.localEulerAngles.x, 0, 0);
        
        if(heldObject != null)
        {
            //Debug.Log(heldObject.grabHandle.localPosition);
            heldObject.gameObject.transform.position = holdTransform.position - heldObject.grabHandle.localPosition ;
            heldObject.gameObject.transform.rotation = holdTransform.rotation;
        }
    }
}
