using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterboxBars : MonoBehaviour
{
    [SerializeField] RectTransform upperBar;
    [SerializeField] RectTransform lowerBar;
    [SerializeField] float appearDuration = 1;
    [SerializeField] float disappearDuration = 1;

    public void ShowBars()
    {
        StartCoroutine(MoveBars(true));
        Debug.Log("show bars");
    }

    public void HideBars()
    {
        StartCoroutine(MoveBars(false));
        Debug.Log("hide bars");
    }

    IEnumerator MoveBars(bool turnBarsOn)
    {
        float t = 0;
        float d;
        float lowerStartY;
        float lowerTargetY;
        float upperStartY;
        float upperTargetY;
        if (turnBarsOn)
        {
            lowerStartY = -360;
            lowerTargetY = -320;
            upperStartY = 360;
            upperTargetY = 320;
            d = appearDuration;

        }
        else
        {
            lowerStartY = -320;
            lowerTargetY = -360;
            upperStartY = 320;
            upperTargetY = 360;
            d = disappearDuration;
        }

        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }

            Vector3 newPosUpper = upperBar.transform.localPosition;
            newPosUpper.y = Mathf.SmoothStep(upperStartY, upperTargetY, t / d);
            upperBar.transform.localPosition = newPosUpper;

            Vector3 newPosLower = lowerBar.transform.localPosition;
            newPosLower.y = Mathf.SmoothStep(lowerStartY, lowerTargetY, t / d);
            lowerBar.transform.localPosition = newPosLower;
            yield return null;
        }
        yield return null;
    }
}
