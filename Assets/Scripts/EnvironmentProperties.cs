using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProperties : MonoBehaviour
{
    [SerializeField] Color ambientColor;
    [SerializeField] GameObject probeParent;
    [SerializeField] float reflectionProbeIntensity;
    private void OnEnable()
    {
        RenderSettings.ambientLight = ambientColor;
        ReflectionProbe[] probes = probeParent.GetComponentsInChildren<ReflectionProbe>();
        foreach(ReflectionProbe probe in probes)
        {
            probe.intensity = reflectionProbeIntensity;
        }
    }
}
