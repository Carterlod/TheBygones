using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class AutoFocus : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] VolumeProfile profile;
    private DepthOfField dof;
    public LayerMask maskInteractables = 1 << 6 & 1 << 2;
  

    private void Start()
    {
        profile.TryGet<DepthOfField>(out dof);
        maskInteractables = ~maskInteractables;
    }
    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Mathf.Infinity, maskInteractables))
        {
            dof.focusDistance.value = hit.distance;
        }
    }
}
