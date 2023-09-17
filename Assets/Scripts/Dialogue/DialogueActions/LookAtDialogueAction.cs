using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDialogueAction : BaseDialogueAction
{
    private DialogueTester dialogue;

    [SerializeField] bool clearAllFirst = true;
    public enum AttentionTarget { Stu, Ted, Mick, Nigel };
    public AttentionTarget objectOfAttention;
    private Transform targetEyeline;
    [SerializeField] bool stu = false;
    [SerializeField] bool mick = false;
    [SerializeField] bool nigel = false;
    [SerializeField] bool ted = false;
    [SerializeField] float turnSpeed = 0.5f;
    [SerializeField] Transform overrideTarget;

    private void Awake()
    {
        dialogue = GetComponentInParent<DialogueTester>();
        if(overrideTarget != null)
        {
            targetEyeline = overrideTarget;
            return;
        }
        if (objectOfAttention == AttentionTarget.Stu)
        {
            targetEyeline = dialogue.stu.eyelineTransform;
        }
        if (objectOfAttention == AttentionTarget.Mick)
        {
            targetEyeline = dialogue.mick.eyelineTransform;
        }
        if (objectOfAttention == AttentionTarget.Nigel)
        {
            targetEyeline = dialogue.nigel.eyelineTransform;
        }
        if (objectOfAttention == AttentionTarget.Ted)
        {
            targetEyeline = dialogue.ted.eyelineTransform;
        }
        
    }
    public override void OnDialogueStart()
    {
        base.OnDialogueStart();

        if (clearAllFirst)
        {
            dialogue.stu.ClearLookAtTarget(turnSpeed);
            dialogue.mick.ClearLookAtTarget(turnSpeed);
            dialogue.nigel.ClearLookAtTarget(turnSpeed);
            dialogue.ted.ClearLookAtTarget(turnSpeed);
        }
        StartCoroutine(LookAtRoutine());
    }

    IEnumerator LookAtRoutine()
    {

        if (stu)
        {
            dialogue.stu.SetLookAtTarget(targetEyeline, turnSpeed);
            yield return new WaitForSeconds(Random.Range(.2f, .6f));
        }
        if (mick)
        {
            dialogue.mick.SetLookAtTarget(targetEyeline, turnSpeed);
            yield return new WaitForSeconds(Random.Range(.2f, .6f));
        }
        if (nigel)
        {
            dialogue.nigel.SetLookAtTarget(targetEyeline, turnSpeed);
            yield return new WaitForSeconds(Random.Range(.2f, .6f));
        }
        if (ted)
        {
            dialogue.ted.SetLookAtTarget(targetEyeline, turnSpeed);
        }

        yield return null;

    }
}
