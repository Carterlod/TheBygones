using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotDialogueAction : BaseDialogueAction
{
    [SerializeField] AudioSource asource;
    [SerializeField] AudioClip clip;

    public override void OnDialogueStart()
    {
        base.OnDialogueStart();
        asource.PlayOneShot(clip);
    }
}
