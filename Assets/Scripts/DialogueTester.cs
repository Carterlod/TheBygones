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
    [SerializeField] DialogueBoxes dialogueBox;
    [SerializeField] int dialogueStep = 0;
    [SerializeField] bool conversationInProgress = false;
    [SerializeField] bool conversationFinished = false;
    public bool highjackCamera = false;
    [SerializeField] NPC speakingNPC;
    [SerializeField] Transform camLookAtTarget;
    [SerializeField] float camPivotDuration = 1;
    

    [SerializeField] FirstPersonController player;
    [SerializeField] Camera cam;

    private Coroutine DialoguePromptCountdown;
    public enum Characters { Stu, Ted, Mick, Nigel};

    private ConversationSwitcher convoSwitcher;

    [System.Serializable]
    public class CharacterLine
    {
        public Characters character;
        public string line;
        public bool thought = false;
    }

    public CharacterLine[] lineOfDialogue;

    [SerializeField] UnityEvent conversationEndEvent;

    private void Awake()
    {
        dialogueStep = 0;
        dialogueBox.gameObject.SetActive(false);
    }

    private void Start()
    {
        cam = player.playerCamera;
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
        if(DialoguePromptCountdown != null)
        {
            StopCoroutine(DialoguePromptCountdown);
        }
        DialoguePromptCountdown = StartCoroutine(DialogueIconCountdown());
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

    IEnumerator DialogueIconCountdown()
    {
        yield return new WaitForSeconds(3);
        DialogueAdvanceIcon.i.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return null;
    }
}
