using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*this script will move the recollectables and the particle effects 
as well as instantiate some recollectables in random spots */

public class recollectableMovement : MonoBehaviour
{
    public ParticleSystem recollectableParticles;
    public PlayerMovement playerMovement;
   

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); 
    }

    void Update()
    {
        if (playerMovement.isInTheSphere == true) //if the player is near the recollectable, particles play
        {
            transform.Rotate(Vector3.up);

            if (gameObject.CompareTag("bread"))
            {
                /*això no funciona :( lo que volia fer és que si s'objecte és meet
                 * es color de ses partícles sigui red, si és una poma de colorines o algo
                 * recollectableParticles.main.startColor = Color.red*/
            }
            //això tampoc funciona :(( sa sphere per lo menos sí que funciona
            recollectableParticles.Play();
        }
        else //if it's not, do not play
        {
            recollectableParticles.Stop();
        }
    }
}
