using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public string characterName;
    public Transform characterHead;
    public Animator animator;
    public bool isLookedAt = false;
    public bool showMyName = false;
    private Coroutine countDown;
    public bool countdownFinished = false;
    public bool clearedToShowName = false;
    [SerializeField] bool countdownUnderway = false;
    [SerializeField] bool doSomethingWhenObserved = false;
    private bool eventFired = false;
    [SerializeField] UnityEvent somethingToDoWhenObserved;



    private void Update()
    {
        if (showMyName)
        {
            if (!isLookedAt)
            {
                if (countdownFinished || clearedToShowName)
                {
                    countdownFinished = false;
                    clearedToShowName = false;
                }
                if(countDown != null)
                {
                    StopCoroutine(countDown);
                    countdownUnderway = false;
                }

            }
            if (isLookedAt && !countdownUnderway && clearedToShowName == false)
            {
                StopAllCoroutines();
                countDown = StartCoroutine(countDownRoutine());
            }
            if (isLookedAt && countdownFinished == true && !clearedToShowName)
            {
                clearedToShowName = true;
                if (!eventFired)
                {
                    eventFired = true;
                    somethingToDoWhenObserved.Invoke();
                }
            }
           // isLookedAt = false;
        }
    }
    

    IEnumerator countDownRoutine()
    {
        countdownUnderway = true;
        float t = 0;
        float d = .5f;
        while (t < d)
        {
            t += Time.deltaTime;
            yield return null;

        }
        countdownFinished = true;

        countdownUnderway = false;
        yield return null;
    }
}
