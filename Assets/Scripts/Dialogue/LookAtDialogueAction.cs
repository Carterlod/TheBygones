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

    private void Awake()
    {
        dialogue = GetComponentInParent<DialogueTester>();
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
            dialogue.stu.ClearLookAtTarget();
            dialogue.mick.ClearLookAtTarget();
            dialogue.nigel.ClearLookAtTarget();
            dialogue.ted.ClearLookAtTarget();
        }
        if (stu)
        {
            dialogue.stu.SetLookAtTarget(targetEyeline);
        }
        if (mick)
        {
            dialogue.mick.SetLookAtTarget(targetEyeline);
        }
        if (nigel)
        {
            dialogue.nigel.SetLookAtTarget(targetEyeline);
        }
        if (ted)
        {
            dialogue.ted.SetLookAtTarget(targetEyeline);
        }
    }
}
