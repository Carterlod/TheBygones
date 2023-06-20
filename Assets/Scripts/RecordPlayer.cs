using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public static RecordPlayer i;
    [SerializeField] public AudioSource speaker;
    private bool on = true;
    [SerializeField] RotateObject rotator;
    private Vector3 initialRotation;
    private float savedTime;

    private void Awake()
    {
        i = this;
        
    }
    private void Start()
    {
        initialRotation = rotator.angle;
    }
    public void OnOff()
    {
        if (on)
        { 
            savedTime = speaker.time;
            speaker.enabled = false;
            rotator.angle = new Vector3(0, 0, 0);
            on = false;
        }
        else
        {
            speaker.time = savedTime;
            speaker.enabled = true;
            rotator.angle = initialRotation;
            on = true;
        }
    }
}
