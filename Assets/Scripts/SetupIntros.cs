using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupIntros : MonoBehaviour
{
    [SerializeField] Camera auxCamera;
    [SerializeField] FirstPersonController playerController;
    [SerializeField] GameObject toggleOnObject;
    [SerializeField] GameObject toggleOffObject;
    [SerializeField] Interactable interactableToTurnOffWhenFinished;

    private bool routineRunning = false;

    [SerializeField] GameObject reticle;
    [SerializeField] GameObject interactIcon;

    private LerpCamera camLerp;

    private void Start()
    {
        camLerp = auxCamera.GetComponent<LerpCamera>();
    }

    public void CutToClub()
    {
        StartCoroutine(CutToClubRoutine());
    }

    private void LateUpdate()
    {
        if (routineRunning)
        {
            reticle.SetActive(false);
            interactIcon.SetActive(false);
        }
    }

    IEnumerator CutToClubRoutine()
    {
        routineRunning = true;
        toggleOnObject.SetActive(true);
        toggleOffObject.SetActive(false);
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;
        auxCamera.enabled = true ;
        camLerp.LerpToward();
        yield return new WaitForSeconds(3);
        auxCamera.enabled = false ;
        toggleOnObject.SetActive(false);
        toggleOffObject.SetActive(true);
        playerController.playerCanMove = true;
        playerController.cameraCanMove = true;
        reticle.SetActive(true);
        interactableToTurnOffWhenFinished.gameObject.SetActive(false);
        this.enabled = false;
        yield return null;
    }
}
