using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequence : MonoBehaviour
{
    [SerializeField] DialogueBoxes[] dialogueBoxes;
    private int dialogueStep = 0;
    private bool conversationInProgress = false;

    private void Start()
    {
        foreach (DialogueBoxes boxes in dialogueBoxes)
        {
            boxes.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(!conversationInProgress)
            {
                dialogueStep = 0;
                conversationInProgress = true;
                AdvanceDialogue();
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    public void AdvanceDialogue()
    {
        foreach(DialogueBoxes boxes in dialogueBoxes)
        {
            boxes.gameObject.SetActive(false);
        }
        if (dialogueStep >= dialogueBoxes.Length)
        {
            conversationInProgress = false;
            return;
        }
        dialogueBoxes[dialogueStep].gameObject.SetActive(true);
        dialogueStep += 1;

    }
}
