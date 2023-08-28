using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomClip : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;
    [SerializeField] GameObject parent;
    private AudioSource[] sourceList;
    private AudioClip selectedClip;

    private void OnEnable()
    {
        selectedClip = clip[Random.Range(0, clip.Length)];
        sourceList = parent.GetComponentsInChildren<AudioSource>();
        foreach(AudioSource source in sourceList)
        {
            source.clip = selectedClip;
            source.Play();
        }
    }
}
