using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitSpot : MonoBehaviour
{
    
    private Transform orientation;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private FirstPersonController player;
    [SerializeField] Transform chair;
    private Collider[] chairColliders;

    private void Start()
    {
        orientation = this.transform;
        player = PlayerSettings.i.playerController;
    }
    public void SitDown()
    {
        PlayerSettings.i.isSeated = true;
        PlayerSettings.i.sitSpot = this;
        originalPosition = player.transform.position;
        originalRotation = player.transform.rotation;
        StartCoroutine(MoveToSeatRoutine());
    }

    public void GetUp()
    {
        StartCoroutine(GetOutOfSeatRoutine());
    }
    IEnumerator MoveToSeatRoutine()
    {
        PlayerSettings.i.PausePlayer();
        chairColliders = chair.GetComponentsInChildren<Collider>();
        foreach(Collider col in chairColliders)
        {
            col.enabled = false;
        }
        if (!player.isCrouched)
        {
            player.Crouch();
        }
        float t = 0;
        float d = .5f;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            player.transform.position = Vector3.Lerp(originalPosition, orientation.position, t / d);
            player.transform.rotation = Quaternion.Lerp(originalRotation, orientation.rotation, t / d);
            yield return null;
        }
        PlayerSettings.i.UnpausePlayer(); // the order of this and the next two lines have an effect on letterbox bars
        player.playerCanMove = false;
        yield return null;
    }

    IEnumerator GetOutOfSeatRoutine()
    {
        PlayerSettings.i.PausePlayer();
        player.Crouch();
        float t = 0;
        float d = .5f;
        Transform getUpSpot = new GameObject().transform;
        getUpSpot.position = orientation.position;
        getUpSpot.rotation = orientation.rotation;
        getUpSpot.position += getUpSpot.forward * .4f ;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            player.transform.position = Vector3.Lerp(orientation.position, getUpSpot.position,t / d);
            //player.transform.rotation = Quaternion.Lerp(orientation.rotation, originalRotation, t / d);
            yield return null;
        }
        foreach (Collider col in chairColliders)
        {
            col.enabled = true;
        }
        player.playerCanMove = true; // the order of this line and the next one down has an effect on how the letterbox bars are handled
        PlayerSettings.i.UnpausePlayer();
        PlayerSettings.i.isSeated = false;
        PlayerSettings.i.sitSpot = null;
        yield return null;
    }
}
