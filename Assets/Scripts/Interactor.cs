using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject interactIcon;
    int layerMaskInteractable;
    int layerMaskNPC;
    int layerMaskGrabbable;
    [SerializeField] TMP_Text npcNameField;
    [SerializeField] bool showNames = false;
    public bool allowed = true;
    [SerializeField] float distance = 2;
    [SerializeField] ObjectGrabber grabber;
    [SerializeField] PlayerSettings playerSettings;
    private bool canUseObject = false;
    
   

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
        npcNameField.gameObject.SetActive(false);

        if (playerSettings.handsFull && !canUseObject)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                grabber.Release();
                playerSettings.handsFull = false;
                return;
            }
        }
        if (playerSettings.cameraActive)
        {
            Debug.Log("camera active");
            return;
        }

        canUseObject = false;

        RaycastHit hit2;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit2, distance) && allowed)
        {
            if (hit2.collider.gameObject.layer == layerMaskInteractable) // INTERACT
            {
                Interactable i = hit2.collider.gameObject.GetComponent<Interactable>();
                if(!i.oneShot || i.oneShot && !i.spent)
                {
                    if (!i.objectRequired)
                    {
                        if (!playerSettings.handsFull)
                        {
                            interactIcon.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                i.Interact();
                            }
                        }
                    }
                    if (i.objectRequired)
                    {
                        Debug.Log("ObjectRequired = true");
                        if(grabber.heldObject != null)
                        {
                            Debug.Log("you have an object");
                            if(grabber.heldObject.objectKey == i.objectKey)
                            {
                                canUseObject = true;
                                Debug.Log("object key validated");
                                interactIcon.SetActive(true);
                                if (Input.GetKeyDown(KeyCode.E))
                                {
                                    i.Interact();
                                }
                            }
                        }
                    }
                }
            }
            if(hit2.collider.gameObject.layer == layerMaskNPC) // SHOW NAME
            {
                string n = hit2.collider.gameObject.GetComponentInParent<NPC>().characterName;
                if (n != null && showNames)
                {
                    npcNameField.gameObject.SetActive(true);
                    npcNameField.text = n;
                }
            }

            if (hit2.collider.gameObject.layer == layerMaskGrabbable && hit2.collider.gameObject.GetComponent<Grabbable>().isActiveAndEnabled) //GRABBABLE
            {
                Grabbable obj = hit2.collider.gameObject.GetComponent<Grabbable>();
                if (!playerSettings.handsFull || canUseObject)
                {
                    interactIcon.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerSettings.handsFull = true;
                    grabber.Grab(obj);
                }
            }
        }

    }
}
