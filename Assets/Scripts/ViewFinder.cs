using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewFinder : MonoBehaviour
{
    [SerializeField] GameObject viewFinderObject;
    [SerializeField] private bool cameraActive = false;
    private FirstPersonController controller;
    [SerializeField] RectTransform tilter;
    [SerializeField] Rigidbody rb;
    [SerializeField] private RectTransform tilterTarget;
    [SerializeField] float adjustSpeed = .1f;
    [SerializeField] float yawTiltAngle = 10;
    [SerializeField] float pitchTiltAngle = 30;
    [SerializeField] float yawDeadzone = 0.1f;
    [SerializeField] float pitchDeadzone = 0.1f;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera screenshotCamera;
    [SerializeField] AudioClip takePicture;
    [SerializeField] GameObject picturePrint;
    [SerializeField] Texture2D pictureTexture;
    [SerializeField] GameObject shutter;
    

    private void Start()
    {
        controller = GetComponentInParent<FirstPersonController>();
        //tilterTarget.rotation = tilter.rotation;
    }

    private void Update()
    {
        cameraActive = controller.isZoomed;

        //if(cameraActive && Input.GetMouseButtonDown(0))
        if (cameraActive && Input.GetMouseButtonDown(0))
        {
            SaveCameraView(screenshotCamera);
        }

        if (cameraActive)
        {
            viewFinderObject.SetActive(true);
            
        }
        else
        {
            viewFinderObject.SetActive(false);
        }

        //Tilt Viewfinder
        float yawTarget = 0;
        float pitchTarget = 0;

        //Debug.Log("mouse X = " + Input.GetAxis("Mouse X") + "mouse y = " + Input.GetAxis("Mouse Y"));

        tilterTarget.rotation = Quaternion.Euler(0, 0, 0);

        // Set X
        if (Input.GetAxis("Mouse X") > yawDeadzone)
        {
            pitchTarget = pitchTiltAngle ;
        }
        else if (Input.GetAxis("Mouse X") < -yawDeadzone)
        {
            pitchTarget = -pitchTiltAngle;
            
        }
        else
        {
            pitchTarget = 0;
        }

        //Set Y
        if (Input.GetAxis("Mouse Y") > pitchDeadzone)
        {
            yawTarget = -yawTiltAngle;
        }
        else if (Input.GetAxis("Mouse Y") < -pitchDeadzone)
        {
            yawTarget = yawTiltAngle;
        }
        else
        {
            yawTarget = 0;
        }

        tilterTarget.rotation = Quaternion.Euler(yawTarget, pitchTarget, 0);

        tilter.rotation = Quaternion.Lerp(tilter.rotation, tilterTarget.rotation, Time.deltaTime * adjustSpeed);
    }

    void SaveCameraView(Camera cam)
    {
        /*

        string dir = Application.dataPath + "/screenshots/";
        var files = new System.IO.DirectoryInfo(dir).GetFiles("*.png");
        foreach (var file in files)
        {
            //if(file.CreationTime < )
            if(file)
            {
                files[99].Delete();
            }
        }
        
  
        */

        StartCoroutine(ShutterRoutine());
        AudioSource asrc = GetComponent<AudioSource>();
        asrc.PlayOneShot(takePicture);
        
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        picturePrint.GetComponent<Renderer>().material.mainTexture = screenTexture;

        /*
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        byte[] byteArray = renderedTexture.EncodeToPNG();
        RenderTexture.active = null;
        System.IO.File.WriteAllBytes(Application.dataPath + "/screenshots/" + DateTime.Now.ToString("yyy-MM-dd HH-mm-ss") + ".png", byteArray);
        //renderedTexture.wrapMode = TextureWrapMode.Clamp;
        

        UnityEditor.AssetDatabase.Refresh();
        */
    }

    IEnumerator ShutterRoutine()
    {
        shutter.SetActive(true);
        yield return new WaitForSeconds(.1f);
        shutter.SetActive(false);
        yield return null;
    }

}
