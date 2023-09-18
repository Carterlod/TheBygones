using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] Transform holdParent;
    public Transform holdTransform;
    [SerializeField] Transform camTransform;
    public Grabbable heldObject;
    private Transform originalParent;
    private Quaternion originalRot;
    public bool lettingGo = false;

    public void Grab(Grabbable obj)
    {
        heldObject = obj;

        // initialize return transform values
        originalParent = heldObject.transform.parent;
        originalRot = heldObject.originalRot;
        

        obj.gameObject.transform.SetParent(holdParent);

        // set position and rotation with grab handle offset
        Transform grab = heldObject.grabHandle;
        grab.SetParent(holdParent);
        heldObject.transform.SetParent(grab);
        grab.rotation = holdTransform.rotation;
        grab.position = holdTransform.position;
        heldObject.transform.SetParent(holdTransform);
        grab.SetParent(heldObject.transform);


        heldObject.Grabbed();
    }
    public void Release(Transform dir)
    {
        //Debug.Log("Release() running");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            heldObject.gameObject.transform.SetParent(originalParent);
            heldObject.GetComponent<Rigidbody>().velocity = dir.forward * 2;
            heldObject.Released();
            holdTransform.localRotation = Quaternion.Euler(0, 0, 0);
            PlayerSettings.i.handsFull = false;
            heldObject = null;
        }
        else
        {
            StartCoroutine(LerpObjectHome());
        }
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
        Vector3 originalPos = heldObject.originalPos;
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

        heldObject.Released();
        holdTransform.localRotation = Quaternion.Euler(0,0,0);
        PlayerSettings.i.handsFull = false;
        heldObject = null;
        lettingGo = false;

        yield return null;
    }
}
