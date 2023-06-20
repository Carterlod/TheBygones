using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpObjectTransform : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private Transform transformClosed;
    [SerializeField] private Transform transformOpen;
    [SerializeField] private float duration = 1f;
    [SerializeField] private AudioSource speaker;
    [SerializeField] private AudioClip clipOpen;
    [SerializeField] private AudioClip clipClose;
     private bool open = false;
     private bool moving = false;
    [SerializeField] private bool disableCollidersOnOpen = true;
    [SerializeField] private bool startOpen = false;

    private void Start()
    {
        if (!startOpen)
        {
            obj.position = transformClosed.position;
            obj.rotation = transformClosed.rotation;
            obj.localScale = transformClosed.localScale;

        }
        else
        {
            obj.position = transformOpen.position;
            obj.rotation = transformOpen.rotation;
            obj.localScale = transformOpen.localScale;
            open = true;
        }
    }
    public void MoveThing()
    {
        if (moving)
        {
            return;
        }
        else
        {
            if (open)
            {
                StartCoroutine(TransformLerpRoutine(transformOpen, transformClosed));
            }
            else
            {
                StartCoroutine(TransformLerpRoutine(transformClosed, transformOpen));
            }
        }
    }

    IEnumerator TransformLerpRoutine(Transform startTrans, Transform targetTrans)
    {
        moving = true;
        if (disableCollidersOnOpen && !open) //turns off colliders when opening
        {
            foreach (Collider col in obj.gameObject.GetComponentsInChildren<Collider>())
            {
                col.enabled = false; 
            }
        }

        open = !open;
        float t = 0;
        //Transform startingTransform = 
        while (t < duration)
        {
            t += Time.deltaTime;
            obj.position = Vector3.Lerp(startTrans.position, targetTrans.position, t / duration);
            obj.rotation = Quaternion.Lerp(startTrans.rotation, targetTrans.rotation, t / duration);
            obj.localScale = Vector3.Lerp(startTrans.localScale, targetTrans.localScale, t / duration);
            yield return null;
        }

        if(disableCollidersOnOpen && open)
        {
            foreach (Collider col in obj.gameObject.GetComponentsInChildren<Collider>())
            {
                col.enabled = true;
            }
        }

        moving = false;
        yield return null;
    }

}
