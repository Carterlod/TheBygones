using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLookAtTargetDialogueAction : BaseDialogueAction
{
    [SerializeField] bool stu = false;
    [SerializeField] bool mick = false;
    [SerializeField] bool nigel = false;
    [SerializeField] bool ted = false;

    [SerializeField] float turnSpeed = 0.5f;

    private DialogueTester dialogue;

    private void Awake()
    {
        dialogue = GetComponentInParent<DialogueTester>();
    }
    public override void OnDialogueStart()
    {
        base.OnDialogueStart();

        if (stu)
        {
            dialogue.stu.ClearLookAtTarget(turnSpeed);
        }
        if (mick)
        {
            dialogue.mick.ClearLookAtTarget(turnSpeed);
        }
        if (nigel)
        {
            dialogue.nigel.ClearLookAtTarget(turnSpeed);
        }
        if (ted)
        {
            dialogue.ted.ClearLookAtTarget(turnSpeed);
        }
    }
}
