using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*this script will move the recollectables and the particle effects 
as well as instantiate some recollectables in random spots */

public class recollectableMovement : MonoBehaviour
{
    public ParticleSystem breadParticles;
    public ParticleSystem meetParticles;
    public ParticleSystem appleRedParticles;
    public ParticleSystem appleParticles;


    void Start()
    {
        if (/*recollectable is in player view (sphere) &&*/ gameObject.CompareTag("bread"))
        {
            breadParticles.Play();
        }
    }

    void Update()
    {
        
    }
}
