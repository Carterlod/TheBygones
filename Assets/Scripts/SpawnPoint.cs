using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] FirstPersonController playerController;

    [SerializeField] bool spawnOnEnable = true;

    private void OnEnable()
    {
        if (spawnOnEnable)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        playerController.transform.position = gameObject.transform.position;
        playerController.transform.rotation = gameObject.transform.rotation;

    }
}
