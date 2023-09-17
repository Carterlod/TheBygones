using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea : MonoBehaviour
{
    [SerializeField] GameObject liquidSurface;
    [SerializeField] FirstPersonController controller;
    [SerializeField] AnimationCurve settleWiggle;
    [SerializeField] ParticleSystem steam;
    private bool playerRecentlyMoved = false;
    private bool playerIsMoving = false;
    private Coroutine wiggleRoutineX;
    private Coroutine wiggleRoutineZ;
    private Grabbable grabbable;
    [SerializeField] Transform teaLevel3;
    [SerializeField] Transform teaLevel2;
    [SerializeField] Transform teaLevel1;
    [SerializeField] Transform teaLevel0;

    [SerializeField] float magnitude = 0;
    [SerializeField] float mouseX = 0;
    private float mouseCoolDownTime = 0;
    private bool isSipping = false;

    [Header("Settings")]
    [SerializeField] bool empty = true;
    [SerializeField] bool full = false;
    [SerializeField] int teaLevel = 3;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
        if (empty)
        {
            liquidSurface.GetComponent<Renderer>().enabled = false;
            liquidSurface.transform.position = teaLevel0.transform.position;
            liquidSurface.transform.localScale = teaLevel0.transform.localScale;
        }
        else
        {
            liquidSurface.GetComponent<Renderer>().enabled = true;
            liquidSurface.transform.position = teaLevel3.transform.position;
            liquidSurface.transform.localScale = teaLevel3.transform.localScale;
            steam.Play();
        }
    }

    private void Update()
    {
        if (grabbable.grabbed && !empty)
        {
            magnitude = controller.GetComponent<Rigidbody>().velocity.magnitude;
            mouseX = Input.GetAxis("Mouse X");
            if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0.5f)
            {
                mouseCoolDownTime = .1f;
            }
            mouseCoolDownTime -= Time.deltaTime;
        

            if (controller.GetComponent<Rigidbody>().velocity.magnitude > 1 || mouseCoolDownTime > 0)
            {
                playerIsMoving = true;
                playerRecentlyMoved = true;
            }
            else
            {
                playerIsMoving = false;
            }
            if(!playerIsMoving && playerRecentlyMoved)
            {
                playerRecentlyMoved = false;
                if(wiggleRoutineX != null)
                {
                    StopCoroutine(wiggleRoutineX);
                }
                wiggleRoutineX = StartCoroutine(SettleRoutineX());
            }
            if (Input.GetMouseButtonDown(0))
            {
                Sip();
            }
        }
    }

    public void FillCup()
    {
        if (full)
        {
            return;
        }
        StartCoroutine(FillCupRoutine());
    }
    public void Sip()
    {
        full = false;
        if (isSipping)
        {
            return;
        }
        if (teaLevel <= 0)
        {
            teaLevel = 0;
            empty = true;
            liquidSurface.GetComponent<Renderer>().enabled = false;
            return;
        }
        teaLevel -= 1;
        StartCoroutine(SipTeaRoutine(teaLevel));
    }
    IEnumerator SipTeaRoutine(int lvl)
    {
        isSipping = true;
        float t = 0;
        float d = .5f;
        Vector3 startingPos = liquidSurface.transform.localPosition;
        Vector3 startingScale = liquidSurface.transform.localScale;
        Transform targetLevel;

        if (lvl == 2)
        {
            targetLevel = teaLevel2;
        }
        else if (lvl == 1)
        {
            targetLevel = teaLevel1;
        }
        else 
        {
            targetLevel = teaLevel0;
            steam.Stop();
        }

        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            liquidSurface.transform.localPosition = Vector3.Lerp(startingPos, targetLevel.transform.localPosition, t / d);
            liquidSurface.transform.localScale = Vector3.Lerp(startingScale, targetLevel.localScale, t / d);
            yield return null;
        }
        isSipping = false;
        yield return null;
    }
    IEnumerator SettleRoutineX()
    {
        float t = 0;
        float d = 1;
        
        while (t < d)
        {
            t += Time.deltaTime;            
            if (t > d)
            {
                t = d;
            }
            Vector3 targetRotation = new Vector3(0,0,0);
            targetRotation.z += settleWiggle.Evaluate(t/d);
            liquidSurface.transform.localEulerAngles = targetRotation;
            yield return null;
        }
        yield return null;
    }
    IEnumerator SettleRoutineZ()
    {
        Debug.Log("coroutine begun");
        float t = 0;
        float d = 1;

        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            Vector3 targetRotation = new Vector3(0, 0, 0);
            targetRotation.z += settleWiggle.Evaluate(t / d);
            liquidSurface.transform.localEulerAngles = targetRotation;
            yield return null;
        }
        yield return null;
    }

    IEnumerator FillCupRoutine()
    {
        empty = false;
        float t = 0;
        float d = 1;
        Vector3 startingPos = liquidSurface.transform.position;
        Vector3 startingScale = liquidSurface.transform.localScale;
        liquidSurface.GetComponent<Renderer>().enabled = true;
        steam.Play();
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            liquidSurface.transform.position = Vector3.Lerp(startingPos, teaLevel3.transform.position, t / d);
            liquidSurface.transform.localScale = Vector3.Lerp(startingScale, teaLevel3.localScale, t / d);
            yield return null;
        }
        full = true;
        teaLevel = 3;        
        yield return null;
    }
}
