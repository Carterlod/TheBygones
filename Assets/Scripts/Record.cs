using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    //private RecordPlayer player;
    private Grabbable grabbable;
    public AudioClip song;
    [SerializeField] Transform sleave;
    public Transform record;
    private Vector3 recordPos1;
    private Vector3 recordPos2;
    private Coroutine showRecordRoutine;
    public bool currentlyPlaying = false;

    private void Start()
    {
        recordPos1 = new Vector3(0,0,0);
        recordPos2 = new Vector3(0.1f, 0,0);
        grabbable = GetComponent<Grabbable>();
        if (currentlyPlaying)
        {
            record.gameObject.SetActive(false);
        }
    }
    
    public void ShowRecord()
    {
        if(showRecordRoutine != null)
        {
            StopCoroutine(showRecordRoutine);
        }
        showRecordRoutine = StartCoroutine(LerpRecordPosition(sleave.position, recordPos2));
    }

    public void HideRecord()
    {
        if(showRecordRoutine != null)
        {
            StopCoroutine(showRecordRoutine);
        }
        showRecordRoutine = StartCoroutine(LerpRecordPosition(recordPos2, sleave.position));
    }

    IEnumerator LerpRecordPosition(Vector3 startPos, Vector3 endPos)
    {
        float t = 0;
        float d = 01f;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t> d)
            {
                t = d;
            }
            record.position = Vector3.Lerp(startPos, endPos, t / d);
            yield return null;
        }
        yield return null;
    }
    

}
