using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject interactIcon;
    int layerMask;
    int layerMask2;
    int layerMask3;
    [SerializeField] TMP_Text npcNameField;
    [SerializeField] bool showNames = false;
    public bool allowed = true;
    [SerializeField] float distance = 2;
    [SerializeField] ObjectGrabber grabber;
    [SerializeField] PlayerSettings playerSettings;
   
    

    

    private void Start()
    {
        interactIcon.SetActive(false);
        layerMask = LayerMask.NameToLayer("Interactable");
        layerMask2 = LayerMask.NameToLayer("NPC");
        layerMask3 = LayerMask.NameToLayer("Grabbable");
    }

    private void Update()
    {
        interactIcon.SetActive(false);
        npcNameField.gameObject.SetActive(false);

        if (playerSettings.handsFull)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                grabber.Release();
                playerSettings.handsFull = false;
            }
            return;
        }
        if (playerSettings.cameraActive)
        {
            return;
        }
        
        RaycastHit hit2;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit2, distance) && allowed)
        {
            if (hit2.collider.gameObject.layer == layerMask && !playerSettings.handsFull) // INTERACT
            {
                interactIcon.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit2.collider.gameObject.GetComponent<Interactable>().Interact();
                }
            }
            if(hit2.collider.gameObject.layer == layerMask2) // SHOW NAME
            {
                string n = hit2.collider.gameObject.GetComponentInParent<NPC>().characterName;
                if (n != null && showNames)
                {
                    npcNameField.gameObject.SetActive(true);
                    npcNameField.text = n;
                }
            }

            if (hit2.collider.gameObject.layer == layerMask3 && hit2.collider.gameObject.GetComponent<Grabbable>().isActiveAndEnabled) //GRABBABLE
            {
                Grabbable obj = hit2.collider.gameObject.GetComponent<Grabbable>();
                interactIcon.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerSettings.handsFull = true;
                    grabber.Grab(obj);
                }
            }
        }

    }
}
