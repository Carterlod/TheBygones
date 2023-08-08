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
    private bool firstBlink = true;
    
    float t = 0;

    private void Start()

    {
        if(countdownRoutine != null)
        {
            StopCoroutine(countdownRoutine);
        }
        countdownRoutine = StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        if (firstBlink)
        {
            yield return new WaitForSeconds(Random.Range(0, delayTilNextBlink));
            firstBlink = false;
        }
        while(t < blinkDuration)
        {
            t += Time.deltaTime;
            if (t > blinkDuration)
            {
                t = blinkDuration;
            }
            eyeRenderer.SetBlendShapeWeight(blinkBlendShapeIndex, curve.Evaluate(t/blinkDuration));
            yield return null;
        }
        float adjustedDelay = delayTilNextBlink + Random.Range(-1f, 1f);
        yield return new WaitForSeconds(adjustedDelay);
        t = 0;
        StartCoroutine(BlinkRoutine());
        yield return null;
    }
}
