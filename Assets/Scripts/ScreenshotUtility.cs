using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenshotUtility : MonoBehaviour
{
    [Tooltip("The filepath to the save location for the screenshots")]
    //public string _folderPath;
    //[Tooltip("Scales the image by the amount specified (0 for no scaling)")]
    public int _screenshotResolutionScaleFactor = 0;
    
    [Space]

    public KeyCode _toggleAutoScreenshots = KeyCode.K;
    public float _timeBetweenAutoScreenshots = 3.0f;

    [Space]

    public KeyCode _pressForSingleScreenshot = KeyCode.L;

    //private int _screenshotNumber;
    private bool _autoScreenshotOn;

    void Start()
    {
        //_screenshotNumber = 0;
        _autoScreenshotOn = false;
        StartCoroutine(AutomaticScreenshot());
    }

    void Update()
    {
        if(Input.GetKeyDown(_toggleAutoScreenshots))
        {
            _autoScreenshotOn = !_autoScreenshotOn;
        }
        if (Input.GetKeyDown(_pressForSingleScreenshot))
        {
            TakeScreenshot();
        }
    }

    IEnumerator AutomaticScreenshot()
    {
        while (true)
        {
            if(_autoScreenshotOn)
            {
                TakeScreenshot();
                yield return new WaitForSeconds(_timeBetweenAutoScreenshots);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    //For reference http://docs.unity3d.com/ScriptReference/Application.CaptureScreenshot.html
    private void TakeScreenshot()
    {
        string screenshotName = "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
        string _folderPath = "Assets/Screenshots/";
        if (!System.IO.Directory.Exists(_folderPath)) // if this path does not exist yet
            System.IO.Directory.CreateDirectory(_folderPath);  // it will get created
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(_folderPath, screenshotName), _screenshotResolutionScaleFactor);
    }
}
