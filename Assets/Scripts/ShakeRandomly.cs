using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeRandomly : MonoBehaviour
{
    private Vector3 _startPosition;
    [SerializeField] float loopDuration = 1;
    [SerializeField] float shakeAmount = 1;
    private float t = 0;
    [SerializeField] AnimationCurve xCurve;
    [SerializeField] AnimationCurve yCurve;
    [SerializeField] AnimationCurve zCurve;

    private void Start()
    {
        _startPosition = gameObject.transform.localPosition;
    }
    void Update()
    {
        t += Time.deltaTime;
        if(t > loopDuration)
        {
            t = 0;
        }
        float newX = _startPosition.x + xCurve.Evaluate(t / loopDuration) * shakeAmount;
        float newY = _startPosition.y + yCurve.Evaluate(t / loopDuration) * shakeAmount;
        float newZ = _startPosition.z + zCurve.Evaluate(t / loopDuration) * shakeAmount;

        gameObject.transform.localPosition = new Vector3(newX, newY, newZ);
    }
}
