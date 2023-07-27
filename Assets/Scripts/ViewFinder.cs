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
    private int screenShotFOV = 20;
    [SerializeField] Renderer pictureRendererH;
    [SerializeField] Renderer pictureRendererV;
    private Material pictureMat;
    [SerializeField] Texture2D pictureTexture;
    [SerializeField] GameObject shutter;
    private FirstPersonController player;
    [SerializeField] bool savePictures = false;
    [SerializeField] Transform rotator;
    private bool vertical = false;
    private bool flipping = false;
    private Quaternion rotationHorizontal;
    private Quaternion rotationVertical;

    [Header("Audio")]
    [SerializeField] AudioSource asrc;
    [SerializeField] AudioClip takePicture;
    [SerializeField] AudioClip sfxTakeOut;
    [SerializeField] AudioClip sfxPutAway;
    [SerializeField] AudioClip sfxFlip1;
    [SerializeField] AudioClip sfxFlip2;




    private void Start()
    {
        controller = GetComponentInParent<FirstPersonController>();
        player = GetComponentInParent<FirstPersonController>();
        rotationHorizontal = rotator.transform.localRotation;
        rotationVertical = Quaternion.Euler(rotationHorizontal.x, rotationHorizontal.y, rotationHorizontal.z + 90);
        
        //tilterTarget.rotation = tilter.rotation;
    }

    private void ToggleViewfinder()
    {

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
            if (Input.GetKeyDown(KeyCode.F) && !flipping)
            {
                StartCoroutine(FlipCamera());
            }
        }
        else
        {
            viewFinderObject.SetActive(false);
        }

        //hacky sfx
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (cameraActive)
            {
                asrc.PlayOneShot(sfxTakeOut);
            }
            else
            {
                asrc.PlayOneShot(sfxPutAway);
            }
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

        // This renders a screenshot and sends it to the in-world picture print
        StartCoroutine(ShutterRoutine());
        //AudioSource asrc = GetComponent<AudioSource>();
        asrc.PlayOneShot(takePicture);
        int screenWidth;
        int screenHeight;
        if (!vertical)
        {
            screenWidth = Screen.width - 100;
            screenHeight = Screen.height;
        }
        else
        {
            screenWidth = Screen.height;
            screenHeight = Screen.width - 100;
        }
        RenderTexture screenTexture = new RenderTexture(screenWidth, screenHeight, 16);
        if (!vertical)
        {
            cam.fieldOfView = 22;
        }
        else
        {
            cam.fieldOfView = 35;
        }
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        if (!vertical)
        {
            pictureMat = pictureRendererH.GetComponent<Renderer>().material;
            pictureMat.mainTexture = screenTexture; //applies render to "photo" object in scene
        }
        else
        {
            pictureMat = pictureRendererV.GetComponent<Renderer>().material;
            pictureMat.mainTexture = screenTexture; //applies render to "photo" object in scene
        }

        /*
        // This saves the renered screenshot to a folder
        if (savePictures)
        {
            Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
            renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            byte[] byteArray = renderedTexture.EncodeToPNG();
            RenderTexture.active = null;
            System.IO.File.WriteAllBytes(Application.dataPath + "/screenshots/" + DateTime.Now.ToString("yyy-MM-dd HH-mm-ss") + ".png", byteArray);
        
            UnityEditor.AssetDatabase.Refresh();
        }
        */
    }

    IEnumerator ShutterRoutine()
    {
        
        shutter.SetActive(true);
        yield return new WaitForSeconds(shutterVisualDuration);
        shutter.SetActive(false);
        yield return null;
    }

    IEnumerator FlipCamera()
    {
        Quaternion targetRot;
        Quaternion startingRot = rotator.localRotation;
        flipping = true;
        if (vertical)
        {
            vertical = false;
            targetRot = rotationHorizontal;
            asrc.PlayOneShot(sfxFlip1);
        }
        else
        {
            vertical = true;
            targetRot = rotationVertical;
            asrc.PlayOneShot(sfxFlip2);
        }
        float t = 0;
        float d = 0.08f;
        while(t < d)
        {
            t += Time.deltaTime;
            if(t > d)
            {
                t = d;
            }
            rotator.localRotation = Quaternion.Lerp(startingRot, targetRot, t / d);
            yield return null;
        } 

        flipping = false;
        yield return null;
    }

}
