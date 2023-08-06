using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithDelay : MonoBehaviour
{
    [SerializeField] float delayBeforeEvent = 1;
    [SerializeField] UnityEvent delayedEvent;
    private Coroutine delayedEventRoutine;

    public void FireDelayedEvent()
    {
        if(delayedEventRoutine != null)
        {
            StopCoroutine(delayedEventRoutine);
        }
        StartCoroutine(DelayedEventRoutine());
    }

    IEnumerator DelayedEventRoutine()
    {
        yield return new WaitForSeconds(delayBeforeEvent);
        delayedEvent.Invoke();
    }
}
