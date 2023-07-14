using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewFinder : MonoBehaviour
{
    [SerializeField] GameObject viewFinderObject;
    private FirstPersonController controller;
    [SerializeField] RectTransform tilter;
    [SerializeField] Rigidbody rb;
    [SerializeField] private RectTransform tilterTarget;
    [SerializeField] float adjustSpeed = .1f;
    [SerializeField] float yTiltAngle = 10;
    [SerializeField] float xTiltAngle = 30;
    [SerializeField] float xDeadzone = 0.1f;
    [SerializeField] float yDeadzone = 0.1f;
    

    private void Start()
    {
        controller = GetComponentInParent<FirstPersonController>();
        //tilterTarget.rotation = tilter.rotation;
    }

    private void Update()
    {
        float xTarget = 0;
        float yTarget = 0;

        if (controller.isZoomed)
        {
            viewFinderObject.SetActive(true);
        }
        else
        {
            viewFinderObject.SetActive(false);
        }

        Debug.Log("mouse X = " + Input.GetAxis("Mouse X") + "mouse y = " + Input.GetAxis("Mouse Y"));

        tilterTarget.rotation = Quaternion.Euler(0, 0, 0);

        // Set X
        if (Input.GetAxis("Mouse X") > xDeadzone)
        {
            yTarget = yTiltAngle ;
        }
        else if (Input.GetAxis("Mouse X") < -xDeadzone)
        {
            yTarget = -yTiltAngle;
            
        }
        else
        {
            yTarget = 0;
        }

        //Set Y
        if (Input.GetAxis("Mouse Y") > yDeadzone)
        {
            xTarget = -xTiltAngle;
        }
        else if (Input.GetAxis("Mouse Y") < -yDeadzone)
        {
            xTarget = xTiltAngle;
        }
        else
        {
            xTarget = 0;
        }

        tilterTarget.rotation = Quaternion.Euler(xTarget, yTarget, 0);

        tilter.rotation = Quaternion.Lerp(tilter.rotation, tilterTarget.rotation, Time.deltaTime * adjustSpeed);
    }
   
}
