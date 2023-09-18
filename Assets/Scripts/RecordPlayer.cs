using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public static RecordPlayer i;
    [SerializeField] public AudioSource speaker;
    private bool on = true;
    [SerializeField] RotateObject rotator;
    [SerializeField] GameObject playingRecordMesh;
    private Vector3 initialRotation;
    [SerializeField] float savedTime = 0;
    public ObjectGrabber grabber;
    [SerializeField] Record record;
    [SerializeField] AnimationCurve recordReturnArc;

    //[SerializeField] bool startOn = true;
    private void Update()
    {
        //Debug.Log("speaker.time is " + speaker.time);
        //savedTime = speaker.time;
    }
    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        initialRotation = rotator.angle;
    }
    private void OnEnable()
    {
        if (record != null)
        {
            speaker.clip = record.song;
            speaker.Play();
            speaker.time = savedTime;
            playingRecordMesh.SetActive(true);

        }
        else
        {
            playingRecordMesh.gameObject.SetActive(false);
        }
    }
    
    public void PlaySong()
    {
        StartCoroutine(SwitchRecord());
    }
    public void OnOff()
    {
        if (on)
        { 
            savedTime = speaker.time;
            speaker.enabled = false;
            rotator.angle = new Vector3(0, 0, 0);
            on = false;
        }
        else
        {
            speaker.time = savedTime;
            speaker.enabled = true;
            rotator.angle = initialRotation;
            on = true;
        }
    }
    IEnumerator SwitchRecord()
    {
        yield return new WaitForSeconds(0.1f);
        Interactable i = GetComponentInChildren<Interactable>();

        //play new record
        if (record == null)
        {
            //Debug.Log("recognized new record");
            record = i.objectLastUsed.GetComponent<Record>();
            record.record.gameObject.SetActive(false);
            playingRecordMesh.SetActive(true);
            record.currentlyPlaying = true;
            yield return new WaitForSeconds(0.1f);

            speaker.time = 0;
            speaker.clip = record.song;
            speaker.Play();
            grabber.Release(this.transform); //the transform passed here is meaningless. It's used by Interactor.cs to launch objects occassionally. 
        }

        //return record
        else if (record != null && record == i.objectLastUsed.GetComponent<Record>() )
        {
            //turn record art back on
            //Debug.Log("recognized record to return");
            Record oldRecord = i.objectLastUsed.GetComponent<Record>();
            playingRecordMesh.SetActive(false);
            oldRecord.currentlyPlaying = false;
            oldRecord.record.gameObject.SetActive(true);
            oldRecord.ShowRecord();
            record = null;
            speaker.clip = null;
            speaker.Stop();
        }

        //swap for a new record
        if(record != null && record != i.objectLastUsed.GetComponent<Record>())
        {
            //Debug.Log("Recognized new record when old one wasn't returned");

            //stop the music
            speaker.Stop();

            //return the current record
            record.currentlyPlaying = false;
            record.record.gameObject.SetActive(true);
            
            float t = 0;
            float d = 0.5f;
            while (t < d)
            {
                t += Time.deltaTime;
                if(t > d)
                {
                    t = d;
                }
                playingRecordMesh.transform.position = Vector3.Slerp(rotator.transform.position, record.gameObject.transform.position, t / d);
                Vector3 returnArc = playingRecordMesh.transform.position;
                returnArc.y += recordReturnArc.Evaluate(t / d);
                playingRecordMesh.transform.position = returnArc;
                playingRecordMesh.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, record.gameObject.transform.rotation, t / d);
                yield return null;
            }
            record.HideRecord();
            playingRecordMesh.SetActive(false);
            playingRecordMesh.transform.position = rotator.transform.position;
            playingRecordMesh.transform.rotation = rotator.transform.rotation;

            //play the new record
            record = i.objectLastUsed.GetComponent<Record>();
            record.record.gameObject.SetActive(false);
            playingRecordMesh.SetActive(true);
            record.currentlyPlaying = true;
            yield return new WaitForSeconds(0.1f);

            speaker.time = 0;
            speaker.clip = record.song;
            speaker.Play();
            grabber.Release(this.transform); //The transform passed here is meaningless. (see line 87)
            yield return null;
        }


        yield return null;
    }
}
