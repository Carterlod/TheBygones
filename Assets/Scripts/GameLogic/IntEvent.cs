using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEvent : MonoBehaviour
{
    public int i;
    [SerializeField] int targetValue = 4;
    [SerializeField] UnityEvent when;
    [SerializeField] bool delayEvent = false;
    [SerializeField] float delay = 1;

    public void IncrementValue()
    {
        i += 1;
        if(i == targetValue)
        {
            if (delayEvent)
            {
                StartCoroutine(delayedEvent());
            }
            else
            {
                when.Invoke();
            }
        }
    }

    IEnumerator delayedEvent()
    {
        yield return new WaitForSeconds(delay);
        when.Invoke();
        yield return null;
    }
}
