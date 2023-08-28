using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDialogueAction : BaseDialogueAction
{
    [SerializeField] UnityEvent dialogueEvent;
    public override void OnDialogueStart()
    {
        dialogueEvent.Invoke();
    }

}
