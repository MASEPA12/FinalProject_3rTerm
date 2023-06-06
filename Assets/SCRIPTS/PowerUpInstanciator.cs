using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInstanciator : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject[] powerUpArray;
    public bool itHasInstantiateAPowerUp = false;

    public  BoxCollider boxCollidersObject;
    public Vector3 boxColliderBiggerSize;

    private void Start()
    {
        boxColliderBiggerSize = new Vector3(boxCollidersObject.size.x, boxCollidersObject.size.y, boxCollidersObject.size.z * 2);//Store an amplified box collider
        boxCollidersObject.center = Vector3.zero; //center always at 0
    }

    private void Update()
    {
        if (!GameManager.sharedInstance.IsFinished()) {
            if (Physics.CheckBox(transform.position, boxColliderBiggerSize, Quaternion.identity, playerLayer) && itHasInstantiateAPowerUp == false)
            {
                Instantiate(powerUpArray[Random.Range(0, powerUpArray.Length)], RandomPosInZone(boxCollidersObject), Quaternion.identity);
                itHasInstantiateAPowerUp = true;
                MusicManager.sharedInstance.AppearSound();
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, boxColliderBiggerSize);
    }

    //this function calculates a random pos in the box collider
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
