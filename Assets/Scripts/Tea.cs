using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea : MonoBehaviour
{
    [SerializeField] GameObject liquidSurface;
    [SerializeField] FirstPersonController controller;
    [SerializeField] AnimationCurve settleWiggle;
    [SerializeField] ParticleSystem steam;
    [SerializeField] Transform teaLevel3;
    [SerializeField] Transform teaLevel2;
    [SerializeField] Transform teaLevel1;
    [SerializeField] Transform teaLevel0;
    public Interactable interactable;
    private Grabbable grabbable;
    private Coroutine steeping;
    private Coroutine waterCooling;

    private bool isSipping = false;

    // Water wiggle settings
    private float wiggleDamper = 0;
    private float liquidWiggleAttack = 2;
    private float liquidWiggleRelease = 0.5f;
    private float t = 0;
    private float mouseCoolDownTime = 0;


    [Header("Settings")]
    [SerializeField] bool empty = true;
    public bool full = false;
    private int teaLevel = 3;
    [SerializeField] Color clearWater = Color.white;
    [SerializeField] Color steepedColor = Color.black;
    [SerializeField] float timeToSteep = 10;
    [SerializeField] float steepTime = 0;
    [SerializeField] GameObject teaBagObj;
    private Vector3 teaBagStartPos;
    private Renderer liquidRenderer;

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
            full = true;
            liquidSurface.GetComponent<Renderer>().enabled = true;
            liquidSurface.transform.position = teaLevel3.transform.position;
            liquidSurface.transform.localScale = teaLevel3.transform.localScale;
            steam.Play();
        }
        teaBagStartPos = teaBagObj.transform.localPosition;
        liquidRenderer = liquidSurface.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (grabbable.grabbed && !empty)
        {
            if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0.5f) 
            {
                mouseCoolDownTime = .1f;
            }
            mouseCoolDownTime -= Time.deltaTime;
        
            if (controller.GetComponent<Rigidbody>().velocity.magnitude > 1 || mouseCoolDownTime > 0)
            {
                wiggleDamper += Time.deltaTime * liquidWiggleAttack;
                if(wiggleDamper > 1)
                {
                    wiggleDamper = 1;
                }
            }

            wiggleDamper -= Time.deltaTime * liquidWiggleRelease;
            if(wiggleDamper < 0)
            {
                wiggleDamper = 0;
            }

            Vector3 targetRotation = new Vector3(0, 0, 0);
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
            }

            targetRotation.z += settleWiggle.Evaluate(t) * wiggleDamper;
            liquidSurface.transform.localEulerAngles = targetRotation;

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
        if(full && steeping != null)
        {
            StopCoroutine(steeping);
            steeping = null;
            StartCoroutine(LiftBagRoutine());
            
        }
        else
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
            steam.Stop();
        }
        else 
        {
            targetLevel = teaLevel0;
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
    
    IEnumerator FillCupRoutine()
    {
        empty = false;
        float t = 0;
        float d = 1;
        Vector3 startingPos = liquidSurface.transform.position;
        Vector3 startingScale = liquidSurface.transform.localScale;
        liquidRenderer.material.SetColor("_BaseColor", clearWater);
        liquidRenderer.enabled = true;
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
        teaBagObj.transform.localPosition = teaBagStartPos;
        teaBagObj.SetActive(true);

        steeping = StartCoroutine(SteepRoutine());
        
        yield return null;
    }

    IEnumerator SteepRoutine()
    {
        Debug.Log("steep routine started");
        float t = 0;
        float d = timeToSteep;
        while (t < d)
        {
            t += Time.deltaTime;
            liquidRenderer.material.SetColor("_BaseColor", Color.Lerp(clearWater, steepedColor, t / d));
            yield return null;
        }

        yield return null;
    }

    IEnumerator LiftBagRoutine()
    {
        float t = 0;
        float d = 0.25f;
        while (t < d)
        {
            t += Time.deltaTime;
            teaBagObj.transform.position += new Vector3(0, 0.5f, 0) * Time.deltaTime;
            yield return null;
        }
        teaBagObj.SetActive(false);
        yield return null;
    }
}
