using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialObject : MonoBehaviour
{
    public KeyCode targetKey;
    [SerializeField] GameObject text;
    private void Update()
    {

        if (Input.GetKeyDown(targetKey))
        {
            text.SetActive(false);
        }
    }

}
