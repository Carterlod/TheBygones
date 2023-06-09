using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] Light light;
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
        on = true ? light.enabled = true : light.enabled = false;
        
    }
    public void Interact()
    {
        
        if (on)
        {
            on = false;
            light.enabled = false;
            speaker.PlayOneShot(clipOff);
        }
        else
        {
            on = true;
            light.enabled = true;
            speaker.PlayOneShot(clipOn);
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
