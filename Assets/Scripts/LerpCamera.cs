using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour
{
    // Use: This script was made to make a camera swing into position when it gets turned on. 

    private Camera cam;
    private Quaternion targetQuaternion;
    [SerializeField] Transform startingTransform;
    private Quaternion startingQuaternion;
    [SerializeField] AnimationCurve smoothingCurve;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        targetQuaternion = gameObject.transform.rotation;
        startingQuaternion = startingTransform.rotation;
    }

    public void LerpToward()
    {
        StartCoroutine(LerpRotation(startingQuaternion, targetQuaternion, .3f));
    }

    public void LerpBack()
    {
        StartCoroutine(LerpRotation(targetQuaternion, startingQuaternion, .5f));
    }

    IEnumerator LerpRotation(Quaternion startRot, Quaternion endRot, float duration)
    {
        float t = 0;
        float d = duration;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            gameObject.transform.rotation = Quaternion.Lerp(startRot, endRot, smoothingCurve.Evaluate(t/d));
            yield return null;
        }
        yield return null;
    }
}
