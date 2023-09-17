using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGameplay : MonoBehaviour
{
    [SerializeField] FirstPersonController controller;
    [SerializeField] GameObject mesh;
    public Transform eyeTransform;
    private Vector3 meshRestingPos;
    private Vector3 eyePosition;
    private Grabbable grabbable;
    private bool camActivated = false;
    [SerializeField] ObjectGrabber grabber;

    [Header("Audio")]
    [SerializeField] AudioSource asrc;
    [SerializeField] AudioClip readyClip;
    [SerializeField] AudioClip putAwayClip;

    private void Start()
    {
        eyePosition = eyeTransform.localPosition;
        meshRestingPos = mesh.transform.localPosition;
        grabbable = GetComponent<Grabbable>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && grabbable.grabbed && !grabbable.isPerformingAction && !grabber.lettingGo)
        {
            if (!camActivated)
            {
                CameraActivate();
            }
            else
            {
                CameraDeactivate();
            }
        }
       
    }

    public void CameraActivate()
    {
        grabbable.isPerformingAction = true;
        
        camActivated = true;
        StartCoroutine(MoveCamera(true));
    }

    public void CameraDeactivate()
    {
        grabbable.isPerformingAction = true;
        camActivated = false;
        StartCoroutine(MoveCamera(false));
    }
    IEnumerator MoveCamera(bool entering)
    {
        if (entering)
        {
            grabbable.cannotDrop = true;
            asrc.PlayOneShot(readyClip);
        }
        if(!entering)
        {
            controller.ManualZoomEnable();
            mesh.gameObject.SetActive(true);
            asrc.PlayOneShot(putAwayClip);
        }
        float t = 0;
        float d = .1f;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            if (entering)
            {
                mesh.transform.localPosition = Vector3.Lerp(meshRestingPos, eyePosition, t / d);
            }
            else
            {
                mesh.transform.localPosition = Vector3.Lerp(eyePosition, meshRestingPos, t / d);
            }
            yield return null;
        }
        if (entering)
        {
            controller.ManualZoomEnable();
            mesh.gameObject.SetActive(false);
        }
        if(!entering)
        {
            grabbable.cannotDrop = false;
        }

        grabbable.isPerformingAction = false;
        yield return null;
    }
}
