using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConversationSwitcher : MonoBehaviour
{
    [SerializeField] DialogueTester[] converstaions;
    private int activeConvo;
    

    private void Start()
    {
        activeConvo = 0;
    }
    public void BeginFirstConversation()
    {
        converstaions[activeConvo].gameObject.SetActive(true);
        converstaions[activeConvo].StartConversation();
    }
    public void IncrementConvo()
    {
        converstaions[activeConvo].gameObject.SetActive(false);
        activeConvo++;
        if(activeConvo > converstaions.Length - 1)
        {
            return;
        }
        converstaions[activeConvo].gameObject.SetActive(true);
    }
    public void StartNextConvo()
    {
        converstaions[activeConvo].StartConversation();
    }

    public void IncrementAndStartNextConvo()
    {
        converstaions[activeConvo].gameObject.SetActive(false);
        activeConvo++;
        if (activeConvo > converstaions.Length - 1)
        {
            return;
        }
        converstaions[activeConvo].gameObject.SetActive(true);
        converstaions[activeConvo].StartConversation();
    }
}
