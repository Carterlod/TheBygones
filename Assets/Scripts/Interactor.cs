using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject interactIcon;
    int layerMask;
    

    private void Start()
    {
        interactIcon.SetActive(false);
        layerMask = LayerMask.NameToLayer("Interactable");
    }

    private void Update()
    {
        interactIcon.SetActive(false);

        
        
        RaycastHit hit2;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit2, 2f))
        {
            if (hit2.collider.gameObject.layer == layerMask)
            {
                interactIcon.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit2.collider.gameObject.GetComponent<Interactable>().Interact();
                }
            }
        }
    }
}
