using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupIntros : MonoBehaviour
{
    [SerializeField] Camera auxCamera;
    [SerializeField] FirstPersonController playerController;
    [SerializeField] GameObject toggleOnObject;
    [SerializeField] GameObject toggleOffObject;

    public void CutToClub()
    {
        StartCoroutine(CutToClubRoutine());
    }
    IEnumerator CutToClubRoutine()
    {
        toggleOnObject.SetActive(true);
        toggleOffObject.SetActive(false);
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;
        auxCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        auxCamera.gameObject.SetActive(false);
        toggleOnObject.SetActive(false);
        toggleOffObject.SetActive(true);
        playerController.playerCanMove = true;
        playerController.cameraCanMove = true;
        yield return null;
    }
}
