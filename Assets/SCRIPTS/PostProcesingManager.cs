using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcesingManager : MonoBehaviour
{
    private Volume volumen;
    private Vignette vignette;

    //Scripts Connections
    private PlayerLife playerLife;

    private void Awake()
    {
        volumen = GetComponent<Volume>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        volumen.profile.TryGet(out vignette);
    }

    private void LateUpdate()
    {
        if (!GameManager.sharedInstance.isGameOver || !GameManager.sharedInstance.isWin) {

            if (playerLife.lives <= 3 && playerLife.lives > 0)
            {
                VignetteOn(1f / playerLife.lives, Color.red);
            }
            else if (playerLife.lives == 0) {
                VignetteOn(1f, Color.red);
            }
            else
            {
                vignette.active = false;
            }
        }   
    }

    public void VignetteOn(float value, Color color) {
        vignette.active = true;
        vignette.intensity.value = value;
        vignette.color.value = color;
    }

    
}
