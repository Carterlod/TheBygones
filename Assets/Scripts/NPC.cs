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
    public Transform eyelineTransform;
    public Transform lookAtTarget; 
    public bool isLookedAt = false;
    public bool showMyName = false;
    private Coroutine countDown;
    public bool countdownFinished = false;
    public bool clearedToShowName = false;
    private bool countdownUnderway = false;
    private bool doSomethingWhenObserved = false;
    private bool eventFired = false;
    [SerializeField] UnityEvent somethingToDoWhenObserved;
    [SerializeField] GameObject headGoal;
    [SerializeField] GameObject prevFramesHead;
    [SerializeField] float headTurnDuration = 1;
    [SerializeField] float headTurnTime = 0;

    

    private void Start()
    {
        headGoal = new GameObject("headGoal");
        prevFramesHead = new GameObject("lastFramesHead");
        prevFramesHead.transform.rotation = characterHead.rotation;
    }
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
        if(lookAtTarget == null)
        {
            headGoal.transform.rotation = characterHead.transform.rotation;
        }
    }
    public void SetLookAtTarget(Transform target)
    {
        lookAtTarget = target;
        headTurnTime = 0;
    }

    public void ClearLookAtTarget()
    {
        lookAtTarget = null;
        headTurnTime = 0;
    }

    private void LateUpdate()
    {
        headTurnTime += Time.deltaTime;
        if(headTurnTime > headTurnDuration)
        {
            headTurnTime = headTurnDuration;
        }

        headGoal.transform.position = characterHead.transform.position;
        prevFramesHead.transform.position = characterHead.transform.position;

        if (lookAtTarget != null)
        {
            headGoal.transform.LookAt(lookAtTarget);
        }
        

        characterHead.rotation = Quaternion.Lerp(prevFramesHead.transform.rotation, headGoal.transform.rotation, headTurnTime/headTurnDuration);
        prevFramesHead.transform.rotation = characterHead.rotation;
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
