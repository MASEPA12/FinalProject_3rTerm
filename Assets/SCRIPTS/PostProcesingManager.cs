using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcesingManager : MonoBehaviour
{
    private Volume volumen;
    private Vignette vignette;

    private void Awake()
    {
        volumen = GetComponent<Volume>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        volumen.profile.TryGet(out vignette);
    }

    public void VignetteOn(float value, Color color) {
        vignette.active = true;
        vignette.intensity.value = value;
        vignette.color.value = color;
    }

    
}
