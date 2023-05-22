using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPool : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private int poolSize = 3; //Initial pool size
    [SerializeField] private List<GameObject> bulletList;

    private static FireballPool instance;
    public static FireballPool Instance { get { return instance; } }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Start()
    {
        AddToPool(poolSize);
    }

    private void AddToPool(int amount)
    {
        //Instantiate initial pool number
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(fireballPrefab);
            bullet.SetActive(false); //set inactive
            bulletList.Add(bullet);
            bullet.transform.parent = transform; //make them sons of bulletPool
        }
    }

    public GameObject Request()
    {

        for (int i = 0; i < bulletList.Count; i++)
        {
            //Check if any bullet is not Active
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }

        }
        AddToPool(1); //Create new bullet if all bullet are active
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count - 1];
    }
}
