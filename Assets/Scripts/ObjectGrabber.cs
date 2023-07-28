using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] Transform holdParent;
    [SerializeField] private Transform holdTransform;
    [SerializeField] Transform camTransform;
    private Grabbable heldObject;
    private Transform originalParent;
    
    private Quaternion originalRot;
    private Vector3 originalPos;
    private Collider[] cols;
    private bool lettingGo = false;

    public void Grab(Grabbable obj)
    {


        obj.rb.Sleep();
        cols = obj.GetComponents<Collider>();
        foreach(Collider col in cols)
        {
            col.enabled = false;
        }

        heldObject = obj;

        // initialize return transform values
        originalParent = heldObject.transform.parent;
        originalRot = heldObject.originalRot;
        originalPos = obj.originalPos;

        obj.gameObject.transform.SetParent(holdParent);

        // set position and rotation with grab handle offset
        Transform grab = heldObject.grabHandle;
        //heldObject.transform.position = holdTransform.position - grab.localPosition;
        grab.SetParent(holdParent);
        heldObject.transform.SetParent(grab);
        grab.rotation = holdTransform.rotation;
        grab.position = holdTransform.position;
        heldObject.transform.SetParent(holdTransform);
        grab.SetParent(heldObject.transform);


        heldObject.rb.isKinematic = true;
    }
    public void Release()
    {
        StartCoroutine(LerpObjectHome());
    }

    private void LateUpdate()
    {
        // keep held object level
        if(heldObject != null && heldObject.keepLevel)
        {
            holdTransform.localRotation = Quaternion.Euler(-camTransform.localEulerAngles.x, 0, 0);
        }
    }

    IEnumerator LerpObjectHome()
    {
        lettingGo = true;
        heldObject.gameObject.transform.SetParent(originalParent);
        Vector3 startingPos = heldObject.gameObject.transform.position;
        Vector3 originalPosRaised = new Vector3(originalPos.x, originalPos.y + 0.1f, originalPos.z);
        Quaternion startingRot = heldObject.gameObject.transform.rotation;

        float t = 0;
        float d = 0.1f;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            heldObject.gameObject.transform.position = Vector3.Lerp(startingPos, originalPosRaised, t / d);
            heldObject.gameObject.transform.rotation = Quaternion.Lerp(startingRot, originalRot, t / d);
            yield return null;
        }
        foreach(Collider col in cols)
        {
            col.enabled = true;
        }
        heldObject.rb.isKinematic = false;
        cols = null;
        heldObject = null;
        lettingGo = false;

        yield return null;
    }
}
