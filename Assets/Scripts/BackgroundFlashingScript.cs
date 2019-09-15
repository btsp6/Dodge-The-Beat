using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlashingScript : MonoBehaviour
{
    public const float SCALE = 0.5f;
    public Color glowColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // The base is at the end
        float value = MusicConversionScript.reducedData[BarControllerScript.NUMBER_OF_BARS-1] * SCALE;

        var displayedColor = new Color(glowColor.r * value, glowColor.g * value, glowColor.b * value);

        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", displayedColor);
    }
}
