using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] Light[] lights;
    //[SerializeField] 
    [SerializeField] bool on = true;
    AudioSource speaker;
    [SerializeField] AudioClip clipOff;
    [SerializeField] AudioClip clipOn;
    [SerializeField] GameObject[] onArt;
    [SerializeField] GameObject[] offArt;

    private void Start()
    {
        speaker = GetComponent<AudioSource>();
        updateArt(on);
        //on = true ? light.enabled = true : light.enabled = false; //this is now handled on Setup.cs
        
    }
    public void Interact(bool withSFX)
    {
        
        if (on)
        {
            on = false;
            foreach(Light l in lights)
            {
                l.enabled = false;
            }
            if (withSFX)
            {
                speaker.PlayOneShot(clipOff);
                 
            }
        }
        else
        {
            on = true;
            foreach(Light l in lights)
            {
                l.enabled = true;
            }
            if (withSFX)
            {
                speaker.PlayOneShot(clipOn);
            }
        }
        updateArt(on);
    }

    public void TurnOn(bool withSFX)
    {
        on = true;
        foreach (Light l in lights)
        {
            l.enabled = true;
        }
        if (withSFX)
        {
            speaker.PlayOneShot(clipOn);
        }
        updateArt(on);
    }

    public void TurnOff(bool withSFX)
    {
        on = false;
        foreach (Light l in lights)
        {
            l.enabled = false;
        }
        if (withSFX)
        {
            speaker.PlayOneShot(clipOff);
        }
        updateArt(on);
    }
    private void updateArt(bool on)
    {
        foreach (GameObject obj in onArt)
        {
            obj.SetActive(on);
        }
        foreach (GameObject obj in offArt)
        {
            obj.SetActive(!on);
        }
    }
    
}
