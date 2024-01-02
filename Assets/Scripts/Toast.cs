using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    [SerializeField] Grabbable grabbable;
    [SerializeField] GameObject[] toastMeshes;
    [SerializeField] int toastStep = 0;
    private bool toastEaten = false;
    private void Update()
    {
        if (grabbable.grabbed && !toastEaten)
        {
            if (Input.GetButtonDown("Use"))
            {
                TakeBite();
            }
        }
    }
    private void TakeBite()
    {
        toastStep += 1;
        if(toastStep> toastMeshes.Length - 1)
        {
            ToastDone();
        }
        else
        {
            foreach(GameObject toastVersion in toastMeshes)
            {
                toastVersion.SetActive(false);
            }
            toastMeshes[toastStep].SetActive(true);
        }
    }
    private void ToastDone()
    {
        toastEaten = true;
        PlayerSettings.i.objectGrabber.Release(transform);
        this.gameObject.SetActive(false);
    }
}
