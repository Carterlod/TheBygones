using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventGroup : MonoBehaviour
{
    [SerializeField] UnityEvent[] events;

    public void PlayEvents()
    {
        foreach (UnityEvent e in events)
        {
            e.Invoke();
        }
    }
}
