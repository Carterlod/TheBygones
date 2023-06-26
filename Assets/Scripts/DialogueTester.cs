using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] NPC stu;
    [SerializeField] NPC ted;
    [SerializeField] NPC mick;
    [SerializeField] NPC nigel;
    [SerializeField] DialogueBoxes dialogueBox;
    private int dialogueStep = 0;
    private bool conversationInProgress = false;
    private bool conversationFinished = false;

    public enum Characters { Stu, Ted, Mick, Nigel};

    [System.Serializable]
    public class CharacterLine
    {
        public string line;
        public Characters character;
    }

    public CharacterLine[] lineOfDialogue;

    private void Start()
    {
        dialogueStep = 0;
        dialogueBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !conversationFinished)
        {
            if (!conversationInProgress)
            {
                conversationInProgress = true;
            }
            AdvanceDialogue();
        }
    }
    public void AdvanceDialogue()
    {
        dialogueBox.gameObject.SetActive(false);

        if (dialogueStep >= lineOfDialogue.Length)
        {
            conversationInProgress = false;
            //conversationFinished = true;
            dialogueStep = 0;
            dialogueBox.gameObject.SetActive(false);
            return;
        }

        NPC speakingNPC;
        Characters speakingCharacter = lineOfDialogue[dialogueStep].character;

        if (speakingCharacter == Characters.Stu)
        {
            speakingNPC = stu;
        }
        else if (speakingCharacter == Characters.Ted)
        {
            speakingNPC = ted;
        }
        else if (speakingCharacter == Characters.Mick)
        {
            speakingNPC = mick;
        }
        else 
        {
            speakingNPC = nigel;
        }
        dialogueBox.npc = speakingNPC;
        

        dialogueBox.nameSlot.text = speakingNPC.characterName;
        dialogueBox.dialogueSlot.text = lineOfDialogue[dialogueStep].line;

        dialogueStep += 1;

        dialogueBox.gameObject.SetActive(true);



    }


}
