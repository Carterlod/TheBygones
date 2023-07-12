using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewFinder : MonoBehaviour
{
    [SerializeField] GameObject viewFinderObject;
    private FirstPersonController controller;

    private void Start()
    {
        controller = GetComponentInParent<FirstPersonController>();
    }

    private void Update()
    {
        if (controller.isZoomed)
        {
            viewFinderObject.SetActive(true);
        }
        else
        {
            viewFinderObject.SetActive(false);
        }
    }
}
