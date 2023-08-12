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
        StartCoroutine(SetAndResetTrigger());
       
    }
    IEnumerator SetAndResetTrigger()
    {
        animator.SetTrigger(trigger);
        yield return new WaitForEndOfFrame();
        animator.ResetTrigger(trigger);
        yield return null;
    }
}
