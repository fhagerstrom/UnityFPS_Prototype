using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public GameObject bulletObject;
    public int bulletPoolSize = 10;

    private List<GameObject> bulletPool;

    // Awake is called before the first frame update
    void Awake()
    {
        // Singleton check
        if (instance == null)
        {
            instance = this;
            InitializePool(); // Initialize the object pool
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializePool()
    {
        bulletPool = new List<GameObject>();

        // Instantiate bullets
        for(int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletObject);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeSelf)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        // If no inactive bullets found, create new ones
        GameObject newBullet = Instantiate(bulletObject);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}
