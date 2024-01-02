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

    private bool okayToToggleViewfinder = true;
    private bool viewfinderButtonReleased = true;
    private float viewfinderToggleTimer = 0;

    private void Start()
    {
        eyePosition = eyeTransform.localPosition;
        meshRestingPos = mesh.transform.localPosition;
        grabbable = GetComponent<Grabbable>();
    }
    private void Update()
    {
        if(!viewfinderButtonReleased && Input.GetAxis("ObjectFunction") == 0)
        {
            viewfinderButtonReleased = true;
        }
        if(grabbable.grabbed && !grabbable.isPerformingAction && !grabber.lettingGo)
        {
            if(Input.GetButtonDown("ObjectFunction"))
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
            if (Input.GetAxis("ObjectFunction") > .1f && okayToToggleViewfinder && viewfinderButtonReleased)
            {
                viewfinderButtonReleased = false;
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
    }

    IEnumerator LockOnToggleTimerRoutine()
    {
        okayToToggleViewfinder = false;
        viewfinderToggleTimer = 0;
        while (viewfinderToggleTimer < 1)
        {
            viewfinderToggleTimer += Time.deltaTime;
            yield return null;
        }
        okayToToggleViewfinder = true;
        yield return null;
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
