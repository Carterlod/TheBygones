using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{

    [SerializeField] bool stoveOn = false;
    [SerializeField] GameObject flame;
    [SerializeField] GameObject dial;
    private Vector3 dialStartRot;
    private Vector3 newRot;
    [SerializeField] Interactable interactable;
    private float t = 0;
    [SerializeField] AnimationCurve flameScaleQuiver;
    private Vector3 flameScaleStart;
    [SerializeField] float flameLoopDuration = 0.5f;
    [SerializeField] GameObject fireFill;

    private void Start()
    {
        dialStartRot = dial.transform.rotation.eulerAngles;
        newRot = dialStartRot;
        flameScaleStart = flame.transform.localScale;
    }

    private void Update()
    {
        if (stoveOn)
        {
            t += Time.deltaTime;
            if (t > flameLoopDuration)
            {
                t = 0;
            }
            flame.transform.localScale = flameScaleStart * flameScaleQuiver.Evaluate(t/flameLoopDuration);
        }    
    }

    public void FlipDial()
    {
        if (stoveOn)
        {
            stoveOn = false;
            flame.SetActive(false);
            StartCoroutine(TurnDial(-50, 0, false));
            fireFill.SetActive(false);

        }
        else
        {
            stoveOn = true;
            StartCoroutine(TurnDial(0, -50, true));

        }
    }
    IEnumerator TurnDial(float startRot, float endRot, bool turningOn)
    {
        interactable.gameObject.SetActive(false);
        float t = 0;
        float d = 0.2f;
        while (t < d)
        {
            t += Time.deltaTime;
            if(t > d)
            {
                t = d;
            }
            newRot.x = Mathf.Lerp(startRot, endRot, t / d);
            dial.transform.localEulerAngles = newRot;
            yield return null;
        }
        if (turningOn)
        {
            flame.SetActive(true);
            fireFill.SetActive(true);
        }
        interactable.gameObject.SetActive(true);
        yield return null;
    }
}
