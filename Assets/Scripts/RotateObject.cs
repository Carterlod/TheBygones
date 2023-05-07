using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //[SerializeField] float speed = 1;
    [SerializeField] Vector3 angle;

    private void Update()
    {
        transform.Rotate(angle.x, angle.y, angle.z);
    }
}
