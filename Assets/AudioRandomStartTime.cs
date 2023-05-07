using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomStartTime : MonoBehaviour
{
    private AudioSource src;
    private AudioClip clip;
    private float maxVol;

    private void OnEnable()
    {
        src = gameObject.GetComponent<AudioSource>();
        clip = src.clip;
        maxVol = src.volume;
    }

    private void Start()
    {
        src.time = (Random.Range(0, clip.length));
        StartCoroutine(FadeUp());
    }
    IEnumerator FadeUp()
    {
        src.volume = 0;
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            src.volume = Mathf.Lerp(0, maxVol, t);
            yield return null;
        }
        yield return null;
    }
}
