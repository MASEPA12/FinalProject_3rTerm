using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private int poolSize; //Initial pool size
    [SerializeField] private List<GameObject> bulletList;

    private static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Start()
    {
        AddBulletToPool(poolSize);
    }

    private void AddBulletToPool(int amount) {
        //Instantiate initial pool number
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false); //set inactive
            bulletList.Add(bullet);
            bullet.transform.parent = transform; //make them sons of bulletPool
        }
    }

    public GameObject RequestBullet() {
       
        for (int i = 0; i < bulletList.Count; i++) {
            //Check if any bullet is not Active
            if (!bulletList[i].activeSelf) {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }

        }
        AddBulletToPool(1); //Create new bullet if all bullet are active
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count-1];
    }
}
