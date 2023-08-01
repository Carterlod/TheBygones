using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{

    [SerializeField] ParticleSystem ps;
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
            
            if (withSFX)
            {
                speaker.PlayOneShot(clipOff);
                ps.Stop();
            }
        }
        else
        {
            on = true;
           
            if (withSFX)
            {
                speaker.PlayOneShot(clipOn);
                ps.Play();
            }
        }
        updateArt(on);
    }

    public void TurnOn(bool withSFX)
    {
        on = true;
      
        if (withSFX)
        {
            speaker.PlayOneShot(clipOn);
            ps.Play();
        }
        updateArt(on);
    }

    public void TurnOff(bool withSFX)
    {
        on = false;
       
        if (withSFX)
        {
            speaker.PlayOneShot(clipOff);
            ps.Stop();
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
