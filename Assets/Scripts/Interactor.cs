using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject interactIcon;
    [SerializeField] GameObject interactIconChair;
    int layerMaskInteractable;
    int layerMaskNPC;
    int layerMaskGrabbable;
    [SerializeField] TMP_Text npcNameField;
    [SerializeField] bool showNames = false;
    [SerializeField] float shortDistance = 2;
    [SerializeField] float longDistance = 4;
    [SerializeField] ObjectGrabber grabber;
    private bool canUseObject = false;
    

    [SerializeField] NPC storedNPC;

    
   

    private void Start()
    {
        interactIcon.SetActive(false);
        layerMaskInteractable = LayerMask.NameToLayer("Interactable");
        layerMaskNPC = LayerMask.NameToLayer("NPC");
        layerMaskGrabbable = LayerMask.NameToLayer("Grabbable");
    }

    private void Update()
    {
        interactIcon.SetActive(false);
        interactIconChair.SetActive(false);
        PlayerSettings.i.playerIsTargettingSitSpot = false;
        npcNameField.gameObject.SetActive(false);

        if (PlayerSettings.i.handsFull && !canUseObject && !grabber.heldObject.cannotDrop && !grabber.heldObject.isPerformingAction)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!PlayerSettings.i.targettedInteractableIsHandsFullOK)
                {
                    grabber.Release(this.transform);
                    PlayerSettings.i.handsFull = false;
                    return;
                }
            }
        }
        PlayerSettings.i.targettedInteractableIsHandsFullOK = false;
        if (PlayerSettings.i.cameraActive)
        {
            //Debug.Log("camera active");
            return;
        }

        canUseObject = false;

        

        RaycastHit hit; 
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, longDistance) && !PlayerSettings.i.playerPaused)
        {

        // INTERACT / INTERACT NAME

            if (hit.collider.gameObject.layer == layerMaskInteractable) 
            {
                Interactable i = hit.collider.gameObject.GetComponent<Interactable>();

                if(!i.oneShot || i.oneShot && !i.spent)
                {
                    if (!i.objectRequired)
                    {
                        if(i.GetComponent<SitSpot>() != null)
                        {
                            interactIconChair.SetActive(true);
                            PlayerSettings.i.playerIsTargettingSitSpot = true;
                            if (Input.GetKeyDown(PlayerSettings.i.playerController.crouchKey))
                            {
                                i.GetComponent<SitSpot>().SitDown();
                            }
                        }
                        else if (!PlayerSettings.i.handsFull || i.handsFullOK)
                        {
                            if (i.handsFullOK)
                            {
                                PlayerSettings.i.targettedInteractableIsHandsFullOK = true;
                            }
                            interactIcon.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                i.Interact();
                            }
                        }
                    }
                    if (i.objectRequired)
                    {
                        if(grabber.heldObject != null)
                        {
                            if(grabber.heldObject.objectKey == i.objectKey)
                            {
                                canUseObject = true;
                                interactIcon.SetActive(true);
                                if (Input.GetKeyDown(KeyCode.E))
                                {
                                    i.InteractWithObject(grabber.heldObject);
                                }
                            }
                        }
                    }
                    if (i.hasName)
                    {
                        npcNameField.gameObject.SetActive(true);
                        npcNameField.text = i.displayedName;
                    }
                }
            }

        // SHOW NAME NPC

            if (hit.collider.gameObject.layer == layerMaskNPC) 
            {
                NPC npc = hit.collider.gameObject.GetComponentInParent<NPC>();
                if(storedNPC == null)
                {
                    storedNPC = npc;
                }
                else if(npc == storedNPC)
                {
                    storedNPC.isLookedAt = false;
                    storedNPC = npc;
                }

                string n = npc.characterName;
                npc.isLookedAt = true;
                if (n != null && npc.clearedToShowName)
                {
                    npcNameField.gameObject.SetActive(true);
                    npcNameField.text = n;
                }
            }
            else
            {
                if(storedNPC != null)
                {
                    storedNPC.isLookedAt = false;
                    storedNPC = null;
                }
            }


        //GRABBABLE

            if (hit.collider.gameObject.layer == layerMaskGrabbable && hit.collider.gameObject.GetComponent<Grabbable>().isActiveAndEnabled) 
            {
                Grabbable obj = hit.collider.gameObject.GetComponent<Grabbable>();
                if (!PlayerSettings.i.handsFull || canUseObject)
                {
                    interactIcon.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayerSettings.i.handsFull = true;
                    grabber.Grab(obj);
                }
            }
        }
    }
}
