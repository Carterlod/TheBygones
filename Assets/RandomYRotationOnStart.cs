using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomYRotationOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 newRot = transform.localRotation.eulerAngles;
        newRot.y = Random.Range(0, 360);
        transform.localEulerAngles = newRot;
    }

    
}
