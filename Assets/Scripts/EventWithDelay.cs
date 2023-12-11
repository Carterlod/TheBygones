using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithDelay : MonoBehaviour
{
    [SerializeField] float delayBeforeEvent = 1;
    [SerializeField] UnityEvent delayedEvent;
    private Coroutine delayedEventRoutine;
    [SerializeField] bool startOnEnabled = false;
    private void Start()
    {
        if (startOnEnabled) { FireDelayedEvent(); }
    }
    public void FireDelayedEvent()
    {
        Debug.Log("EventWithDelayed fired function 'FireDelayedEvent()'");
        if(delayedEventRoutine != null)
        {
            StopCoroutine(delayedEventRoutine);
        }
        delayedEventRoutine = StartCoroutine(DelayedEventRoutine());
    }

    IEnumerator DelayedEventRoutine()
    {
        Debug.Log("EventWithDelayed fired coroutine 'DelayedEventRoutine'");
        yield return new WaitForSeconds(delayBeforeEvent);
        delayedEvent.Invoke();
        yield return null;
    }
}
