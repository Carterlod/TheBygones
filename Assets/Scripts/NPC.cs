using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public string characterName;
    public Animator animator;
    public Transform characterHead;
    public SkinnedMeshRenderer mouth;

    [Header("Looking At Each Other")]
    public Transform eyelineTransform;
    public Transform lookAtTarget; 
    [SerializeField] float headTurnDuration = 1;
    private GameObject headGoal;
    private GameObject prevFramesHead;
    private float headTurnTime = 0;
    public float headTurnSpeed = 1;
    public bool showDotDebug = false;
    private bool skippedInitialHeadTurn = false;
    public bool headControlled = false;

    [Header("Player Looking At Me")]
    public bool isLookedAt = false;
    public bool showMyName = false;
    private Coroutine countDown;
    public bool countdownFinished = false;
    public bool clearedToShowName = false;
    private bool countdownUnderway = false;
    private bool doSomethingWhenObserved = false;
    private bool eventFired = false;
    [SerializeField] UnityEvent somethingToDoWhenObserved;

    private void Start()
    {
        headGoal = new GameObject("headGoal");
        headGoal.transform.parent = this.transform;
        prevFramesHead = new GameObject("lastFramesHead");
        prevFramesHead.transform.parent = this.transform;
        
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
        } 
    }

    public void SetLookAtTarget(Transform target, float speed)
    {
        headControlled = true;
        lookAtTarget = target;
        headTurnTime = 0;
        headTurnSpeed = speed;
    }

    public void ClearLookAtTarget(float speed)
    {
        lookAtTarget = null;
        headTurnTime = 0;
        headTurnSpeed = speed;
        headControlled = false;
    }

    private void LateUpdate()
    {
        headTurnTime += Time.deltaTime * headTurnSpeed;
        if(headTurnTime > headTurnDuration)
        {
            headTurnTime = headTurnDuration;
        }

        headGoal.transform.position = characterHead.transform.position;
        headGoal.transform.rotation = characterHead.transform.rotation;

        if (lookAtTarget != null)
        {
            headGoal.transform.LookAt(lookAtTarget);
        }
        if (!skippedInitialHeadTurn)
        {
            skippedInitialHeadTurn = true;
        }
        else
        {
            characterHead.rotation = Quaternion.Lerp(
                prevFramesHead.transform.rotation, 
                headGoal.transform.rotation, 
                headTurnTime/headTurnDuration
                );
        }

        prevFramesHead.transform.position = characterHead.position;
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
