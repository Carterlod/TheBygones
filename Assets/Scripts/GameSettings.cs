using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSettings : MonoBehaviour
{
    public static GameSettings i;
    public bool autoPlayDialogue = false;
    [SerializeField] GameObject autoplayIcon;

    private void Start()
    {
        if (!autoPlayDialogue)
        {
            autoplayIcon.SetActive(false);
        }
        else
        {
            autoplayIcon.SetActive(true);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && !PlayerSettings.i.playerPaused)
        {

            if (!autoPlayDialogue)
            {
                autoPlayDialogue = true;
                autoplayIcon.SetActive(true);
            }
            else
            {
                autoPlayDialogue = false;
                autoplayIcon.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        i = this;
    }


}
