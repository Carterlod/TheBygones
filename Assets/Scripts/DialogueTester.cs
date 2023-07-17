using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public bool highjackCamera = false;
    [SerializeField] NPC speakingNPC;
    [SerializeField] Transform camLookAtTarget;
    [SerializeField] float camPivotDuration = 1;

    [SerializeField] FirstPersonController player;
    [SerializeField] Camera cam;
    public enum Characters { Stu, Ted, Mick, Nigel};

    private ConversationSwitcher convoSwitcher;

    [System.Serializable]
    public class CharacterLine
    {
        public string line;
        public Characters character;
        public bool thought = false;
    }

    public CharacterLine[] lineOfDialogue;

    [SerializeField] UnityEvent conversationEndEvent;

    private void Start()
    {
        dialogueStep = 0;
        dialogueBox.gameObject.SetActive(false);
        cam = player.playerCamera;
        convoSwitcher = GetComponentInParent<ConversationSwitcher>();
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
        if(speakingNPC != null)
        {
            //cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camLookAtTarget.rotation, Time.deltaTime * camPivotDuration) ;
            camLookAtTarget.LookAt(speakingNPC.characterHead.transform.position);
        }
    }
    public void AdvanceDialogue()
    {
        dialogueBox.gameObject.SetActive(false);

        if (dialogueStep >= lineOfDialogue.Length)
        {
            conversationInProgress = false;
            conversationFinished = true;
            dialogueStep = 0;
            dialogueBox.gameObject.SetActive(false);
            convoSwitcher.IncrementConvo();
            if(conversationEndEvent != null)
            {
                conversationEndEvent.Invoke();
            }
            
            return;
        }

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
        dialogueBox.UpdateBoxArt(!lineOfDialogue[dialogueStep].thought);

        dialogueBox.gameObject.SetActive(true);

        dialogueStep += 1;

        if (highjackCamera)
        {
            StartCoroutine(PivotCamTowardCharacter());
        }
    }

    IEnumerator PivotCamTowardCharacter()
    {

        player.cameraCanMove = false;
        float t = 0;
        Quaternion initialRot = cam.transform.rotation;
        while(t < camPivotDuration)
        {
            t += Time.deltaTime;
            camLookAtTarget.LookAt(speakingNPC.characterHead.transform.position);
            cam.transform.rotation = Quaternion.Lerp(initialRot, camLookAtTarget.rotation, t/camPivotDuration);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        player.cameraCanMove = true;
        yield return null;
    }


}
