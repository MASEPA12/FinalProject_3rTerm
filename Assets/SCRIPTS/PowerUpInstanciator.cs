using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInstanciator : MonoBehaviour
{
    private int squareRadius;
    public LayerMask playerLayer;
    public GameObject[] powerUpArray;
    public GameManager gameManagerScript;
    public bool itHasInstantiateAPowerUp = false;

    public  BoxCollider boxCollidersObject;
    public Vector3 boxColliderBiggerSize;

    private void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        boxColliderBiggerSize = new Vector3(boxCollidersObject.size.x, boxCollidersObject.size.y, boxCollidersObject.size.z * 2);//guardam una mida ampliada de es box collider
        boxCollidersObject.center = new Vector3(0, 0, 0); //posam que es centre sigui sempre 0, així serà de sa mateix size
    }

    private void Update()
    {
        if (!GameManager.sharedInstance.IsFinished()) {
            if (Physics.CheckBox(transform.position, boxColliderBiggerSize, Quaternion.identity, playerLayer) && itHasInstantiateAPowerUp == false)
            {
                Instantiate(powerUpArray[Random.Range(0, powerUpArray.Length)], RandomPosInZone(boxCollidersObject), Quaternion.identity);
                MusicManager.sharedInstance.AppearSound();
                itHasInstantiateAPowerUp = true;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, boxColliderBiggerSize);
    }

    //this function calculates a rondom pos in the box collider
    public Vector3 RandomPosInZone(BoxCollider box)
    {
        //asign the size of the box
        float boxSizeX = box.size.x;
        float boxSizeY = box.size.y;
        float boxSizeZ = box.size.z;

        float randomPosX = Random.Range(0, boxSizeX);
        float randomPosY = Random.Range(0, boxSizeY);
        float randomPosZ = Random.Range(0, boxSizeZ);

        Vector3 randomPosA = new Vector3(randomPosX, randomPosY, randomPosZ);
        Vector3 randomPosB = box.transform.position + randomPosA;

        return randomPosB;
    }
}
