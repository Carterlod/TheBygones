using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    private Coroutine countdownRoutine;
    [SerializeField] SkinnedMeshRenderer eyeRenderer;

    [SerializeField] float blinkDuration = 1f;
    [SerializeField] float delayTilNextBlink = 2;
    [SerializeField] int blinkBlendShapeIndex = 0;
    [SerializeField] AnimationCurve curve;
    
    float t = 0;

    private void Start()

    {
        //eyeRenderer = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(BlinkRoutine());

    }

    /*
    private void Update()
    {
        t += Time.deltaTime;
        if(t > blinkDuration)
        {
            t = 0;
        }

        eyeRenderer.SetBlendShapeWeight(blinkBlendShapeIndex, curve.Evaluate(t));
        Debug.Log("t = " + t);
    }
    */

    IEnumerator BlinkRoutine()
    {
        Debug.Log("Starting new routine");
        while(t < blinkDuration)
        {
            t += Time.deltaTime;
            Debug.Log("t = " + t);
            eyeRenderer.SetBlendShapeWeight(blinkBlendShapeIndex, curve.Evaluate(t/blinkDuration));
            yield return null;
        }

        yield return new WaitForSeconds(delayTilNextBlink);


        t = 0;
        StartCoroutine(BlinkRoutine());

        yield return null;
    }
    
}
