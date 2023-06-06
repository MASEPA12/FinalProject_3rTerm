using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*this script will move the recollectables and the particle effects 
as well as instantiate some recollectables in random spots */

public class RecollectableMovement : MonoBehaviour
{
    public ParticleSystem recollectableParticles;
    public LayerMask playerLayer;
    public int sphereRadius = 3;

    //Points variables
    [SerializeField] public int points;
    [SerializeField] public int hunger;
    [SerializeField] public int heal = 0;

    void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereRadius, playerLayer)) //if the player is near the recollectable, the recollectable rotates and particles play
        {
            transform.Rotate(Vector3.up);
            //play sound de que hi ha un panesito per recollir

            if (!recollectableParticles.isPlaying) //Play only if not already playing
            {
                recollectableParticles.Play();
            }
        }
        else //if it's not, do not play
        {
            recollectableParticles.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

}
