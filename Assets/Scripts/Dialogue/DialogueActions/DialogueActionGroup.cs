using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActionGroup : BaseDialogueAction
{
    [SerializeField] List<BaseDialogueAction> actions;

    public override void OnDialogueStart()
    {
        base.OnDialogueStart();
        foreach (BaseDialogueAction action in actions)
        {
            action.OnDialogueStart();
        }
    }
}
