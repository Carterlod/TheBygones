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
    [SerializeField] float pitchTilt = 10;
    [SerializeField] float yawTilt = 30;
    [SerializeField] float yawDeadzone = 0.1f;
    [SerializeField] float pitchDeadzone = 0.1f;
    [SerializeField] float adjustSpeed = 0.1f;
    [SerializeField] float shutterVisualDuration = 0.1f;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera screenshotCamera;
    [SerializeField] AudioClip takePicture;
    [SerializeField] GameObject picturePrint;
    [SerializeField] Texture2D pictureTexture;
    [SerializeField] GameObject shutter;
    private FirstPersonController player;
    

    private void Start()
    {
        controller = GetComponentInParent<FirstPersonController>();
        player = GetComponentInParent<FirstPersonController>();
        //tilterTarget.rotation = tilter.rotation;
    }

    private void Update()
    {
        

        cameraActive = controller.isZoomed;

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

        tilterTarget.localRotation = Quaternion.Euler(0, 0, 0);

        // Set X
        if (Input.GetAxis("Mouse X") > yawDeadzone)
        {
            pitchTarget = yawTilt ;
        }
        else if (Input.GetAxis("Mouse X") < -yawDeadzone)
        {
            pitchTarget = -yawTilt;
            
        }
        else
        {
            pitchTarget = 0;
        }

        //Set Y
        if (Input.GetAxis("Mouse Y") > pitchDeadzone)
        {
            yawTarget = -pitchTilt;
        }
        else if (Input.GetAxis("Mouse Y") < -pitchDeadzone)
        {
            yawTarget = pitchTilt ;
        }
        else
        {
            yawTarget = 0;
        }

        tilterTarget.localRotation = Quaternion.Euler(yawTarget, pitchTarget, 0);

        

        tilter.localRotation = Quaternion.Lerp(tilter.localRotation, tilterTarget.localRotation, Time.deltaTime * adjustSpeed);
    }

    void SaveCameraView(Camera cam)
    {
        // I was trying to figure out a way to find the oldest screenshot in the foler, but gave up here
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
        picturePrint.GetComponent<Renderer>().material.mainTexture = screenTexture; //applies render to "photo" object in scene


        // This saves the renered screenshot to a folder
        
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        byte[] byteArray = renderedTexture.EncodeToPNG();
        RenderTexture.active = null;
        System.IO.File.WriteAllBytes(Application.dataPath + "/screenshots/" + DateTime.Now.ToString("yyy-MM-dd HH-mm-ss") + ".png", byteArray);
        
        

        UnityEditor.AssetDatabase.Refresh();
        
    }

    IEnumerator ShutterRoutine()
    {
        shutter.SetActive(true);
        yield return new WaitForSeconds(shutterVisualDuration);
        shutter.SetActive(false);
        yield return null;
    }

}
