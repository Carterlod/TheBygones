using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventPlaySound : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource speaker;

    public void PlayClip()
    {
        speaker.Play() ;
    }
}
