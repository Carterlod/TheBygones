using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialObject : MonoBehaviour
{
    public KeyCode targetKey;
    [SerializeField] GameObject text;
    [SerializeField] string inputName;
    private void Update()
    {

        if (Input.GetKeyDown(targetKey))
        {
            text.SetActive(false);
        }
        if (Input.GetButtonDown(inputName))
        {
            text.SetActive(false);
        }
        if (Input.GetAxis(inputName) > .1f)
        {
            text.SetActive(false);
        }
    }

}
