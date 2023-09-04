using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform targetTransform;

    [SerializeField] bool rotation = false;

    private void Update()
    {
        this.transform.position = targetTransform.position;
        if (rotation)
        {
            this.transform.rotation = targetTransform.rotation;
        }
    }
}
