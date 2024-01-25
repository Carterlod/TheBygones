using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByCurve : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float tim = 0;
    [SerializeField] float loopTime = 1;
    Vector3 baseRot;
    private void Start()
    {
        baseRot = transform.localEulerAngles ;
    }
    private void Update()
    {
        Debug.Log("update is running");
        tim += Time.deltaTime;

        Vector3 newRot = baseRot;

        newRot.x += curve.Evaluate(tim / loopTime);
        this.transform.localEulerAngles = newRot;
        if (tim >= loopTime)
        {
            tim = 0;
        }
    }
}
