using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{

    public bool on = true;
    [SerializeField] GameObject nozzleMesh;
    [SerializeField] ParticleSystem ps;
    AudioSource speaker;
    [SerializeField] AudioClip clipOff;
    [SerializeField] AudioClip clipOn;
    [SerializeField] GameObject[] onArt;
    [SerializeField] GameObject[] offArt;
    private Vector3 newRot;

    private void Start()
    {
        speaker = GetComponent<AudioSource>();
        updateArt(on);
        newRot = nozzleMesh.transform.localEulerAngles;
        //on = true ? light.enabled = true : light.enabled = false; //this is now handled on Setup.cs
    }
    public void Interact(bool withSFX)
    {
        if (on)
        {
            on = false;
            StartCoroutine(TurnNozzle(-150, 0));            
            if (withSFX)
            {
                speaker.PlayOneShot(clipOff);
                ps.Stop();
            }
        }
        else
        {
            on = true;           
            StartCoroutine(TurnNozzle(0, -150));
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
    IEnumerator TurnNozzle(float startRot, float endRot)
    {
        
        float t = 0;
        float d = 0.2f;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            newRot.y = Mathf.Lerp(startRot, endRot, t / d);
            nozzleMesh.transform.localEulerAngles = newRot;
            yield return null;
        }
        yield return null;
    }
}
