using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionFlicker : MonoBehaviour
{
    public Material flickeringMaterial;
    public float flickerSpeed = 5.0f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    private float currentIntensity;

    void Start()
    {
        currentIntensity = flickeringMaterial.GetColor("_EmissionColor").r;
    }

    void Update()
    {
        // Calculate the flicker intensity using a sine wave
        float flickerIntensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1.0f));

        // Set the emission color with the calculated intensity
        Color newColor = new Color(flickerIntensity, flickerIntensity, flickerIntensity);
        flickeringMaterial.SetColor("_EmissionColor", newColor);

        // Ensure changes are applied
        flickeringMaterial.EnableKeyword("_EMISSION");
    }
}
