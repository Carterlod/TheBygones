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
    [SerializeField] TMP_Text npcNameField;
    [SerializeField] bool showNames = false;
    public bool allowed = true;
    [SerializeField] float distance = 2;
    

    

    private void Start()
    {
        interactIcon.SetActive(false);
        layerMask = LayerMask.NameToLayer("Interactable");
        layerMask2 = LayerMask.NameToLayer("NPC");
    }

    private void Update()
    {
        interactIcon.SetActive(false);
        npcNameField.gameObject.SetActive(false);

        RaycastHit hit2;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit2, distance) && allowed)
        {
            if (hit2.collider.gameObject.layer == layerMask) // INTERACT
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
        }
    }
}
