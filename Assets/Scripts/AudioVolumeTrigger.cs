using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeTrigger : MonoBehaviour
{
    [SerializeField] AudioSource speaker;
    [SerializeField] float vol = 0.5f;
    [SerializeField] float fadeDuration = 0.3f;
    [SerializeField] private Transform newSourceTransform;
    private Transform originalSourceTransform;


    private void Start()
    {
        if(newSourceTransform == null)
        {
            newSourceTransform = speaker.gameObject.transform;
        }
        
        //speaker.volume = minMax.x;
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(LerpVolume(vol));
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(LerpVolume(minMax.x));
        }
    }
    */
    IEnumerator LerpVolume(float volTarget)
    {
        originalSourceTransform = speaker.gameObject.transform;
        float t = 0;
        float initialVol = speaker.volume;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            speaker.volume = Mathf.Lerp(initialVol, volTarget, t / fadeDuration);
            speaker.gameObject.transform.position = Vector3.Lerp(originalSourceTransform.position, newSourceTransform.position, t / fadeDuration);
            yield return null;
        }
        
        yield return null;
    }
}
