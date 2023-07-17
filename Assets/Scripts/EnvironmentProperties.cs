using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProperties : MonoBehaviour
{
    [SerializeField] Color ambientColor;
    private void OnEnable()
    {
        RenderSettings.ambientLight = ambientColor;
    }
}
