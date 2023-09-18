using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] Interactable interactable;
    private ObjectGrabber grabber;
    [SerializeField] AnimationCurve shakeCurve;

    private void Start()
    {
        grabber = PlayerSettings.i.objectGrabber;
    }
    public void ThrowAway()
    {
        StartCoroutine(ThrowAwayRoutine());
        StartCoroutine(ShakeCan());
    }
    IEnumerator ThrowAwayRoutine()
    {
        Grabbable keyObj = interactable.objectLastUsed;
        keyObj.SetSettleSpot(this.transform);
        grabber.Release(this.transform);
        yield return new WaitForSeconds(.1f);
        keyObj.gameObject.SetActive(false);

        yield return null;
    }
    IEnumerator ShakeCan()
    {
        Vector3 originalRot = this.transform.localEulerAngles;
        float t = 0;
        float d = 0.5f;
        while (t < d)
        {
            t += Time.deltaTime;
            if (t > d)
            {
                t = d;
            }
            Vector3 newRot = originalRot;
            newRot.x += shakeCurve.Evaluate(t / d);
            this.transform.localEulerAngles = newRot;
            yield return null;
        }
        yield return null;
    }
}
