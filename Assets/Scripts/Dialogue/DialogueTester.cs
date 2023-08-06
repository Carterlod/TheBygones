using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogueTester : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] NPC stu;
    [SerializeField] NPC ted;
    [SerializeField] NPC mick;
    [SerializeField] NPC nigel;

    [Header("Settings")]
    [SerializeField] bool conversationInProgress = false;
    [SerializeField] bool conversationFinished = false;
    [SerializeField] bool autoIncrementConvos = false;
    [SerializeField] int dialogueStep = 0;

    //[Header("Auto-Camera")]
    //public bool highjackCamera = false;
    //[SerializeField] float camPivotDuration = 1;


    [Header("References")]
    [SerializeField] NPC speakingNPC;
    [SerializeField] Transform camLookAtTarget;
    [SerializeField] DialogueBoxes dialogueBox;
    [SerializeField] FirstPersonController player;
    [SerializeField] Camera cam;

    [SerializeField] UnityEvent conversationEndEvent;

    private Coroutine DialoguePromptCountdown;
    public enum Characters { Stu, Ted, Mick, Nigel};

    private ConversationSwitcher convoSwitcher;

    [System.Serializable]
    public class CharacterLine
    {
        [TextArea]
        public string line;
        public Characters character;
        public BaseDialogueAction baseAction;
    }

    public CharacterLine[] lineOfDialogue;


    private void Awake()
    {
        dialogueStep = 0;
        dialogueBox.gameObject.SetActive(false);
    }

    private void Start()
    {
        //cam = player.playerCamera;
        convoSwitcher = GetComponentInParent<ConversationSwitcher>();
    }

   
    public void StartConversation()
    {
        if (!conversationInProgress)
        {
            conversationInProgress = true;
            DialogueAdvanceIcon.i.GetComponent<TextMeshProUGUI>().enabled = false;
            DialoguePromptCountdown = StartCoroutine(DialogueIconCountdown());
            
        }
        AdvanceDialogue();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !conversationFinished && PlayerSettings.i.dialogueAdvanceable)
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
        DialogueAdvanceIcon.i.GetComponent<TextMeshProUGUI>().enabled = false;

        if(dialogueStep > 0 && lineOfDialogue[dialogueStep - 1].baseAction != null)
        {
            lineOfDialogue[dialogueStep - 1].baseAction.OnDialogueEnd();
        }

        //end conversation
        if (dialogueStep >= lineOfDialogue.Length)
        {
           
            conversationInProgress = false;
            conversationFinished = true;
            dialogueStep = 0;
            dialogueBox.gameObject.SetActive(false);
            if (autoIncrementConvos)
            {
                convoSwitcher.IncrementConvo();
            }
            if(conversationEndEvent != null)
            {
                conversationEndEvent.Invoke();
            }
            if (DialoguePromptCountdown != null)
            {
                StopCoroutine(DialoguePromptCountdown);
            }
            
            return;
        }
        
        //assign NPC to this line
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

        
        //dialogueBox.UpdateBoxArt(!lineOfDialogue[dialogueStep].thought);
        if(lineOfDialogue[dialogueStep].baseAction != null)
        {
            lineOfDialogue[dialogueStep].baseAction.OnDialogueStart();
        }

        dialogueBox.gameObject.SetActive(true);

        dialogueStep += 1;

        //if (highjackCamera)
        //{
        //    StartCoroutine(PivotCamTowardCharacter());
        //}

        if(DialoguePromptCountdown != null)
        {
            StopCoroutine(DialoguePromptCountdown);
        }
        DialoguePromptCountdown = StartCoroutine(DialogueIconCountdown());
    }
    IEnumerator DialogueIconCountdown()
    {
        yield return new WaitForSeconds(3);
        DialogueAdvanceIcon.i.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return null;
    }
    /*
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
    */
}
