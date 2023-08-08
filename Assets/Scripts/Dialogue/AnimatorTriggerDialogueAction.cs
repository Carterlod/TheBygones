using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTriggerDialogueAction : BaseDialogueAction
{
    [SerializeField] Animator animator;
    [SerializeField] string trigger;

    public override void OnDialogueStart()
    {
        base.OnDialogueStart();
        animator.SetTrigger(trigger);
       
    }
}
