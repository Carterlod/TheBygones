using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentProperties : MonoBehaviour
{
    [SerializeField] Color ambientColor;
    [SerializeField] Color fogColor = Color.white;
    [SerializeField] Color skyColor = Color.white;
    [SerializeField] Color toonRampTint = new Color(0.2f, 0.2f, 0.2f);
    [SerializeField] GameObject probeParent;
    [SerializeField] float reflectionProbeIntensity;
    
    private void OnValidate()
    {
        UpdateLighting();
    }
    private void OnEnable()
    {
        UpdateLighting();
    }

    private void UpdateLighting()
    {
        RenderSettings.ambientLight = ambientColor;
        RenderSettings.fogColor = fogColor;
        RenderSettings.skybox.SetColor("_Tint", skyColor);

        Shader.SetGlobalColor("GLOBAL_ToonRampTint", toonRampTint);
        
        if(probeParent != null)
        {
            ReflectionProbe[] probes = probeParent.GetComponentsInChildren<ReflectionProbe>();
            foreach (ReflectionProbe probe in probes)
            {
                probe.intensity = reflectionProbeIntensity;
            }
        }

    }
}
