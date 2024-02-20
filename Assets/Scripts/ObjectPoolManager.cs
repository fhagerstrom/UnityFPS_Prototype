using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public GameObject bulletObject;
    public int bulletPoolSize = 10;

    public GameObject enemyObject;
    public int enemyPoolSize = 15;

    private List<GameObject> bulletPool;
    private List<GameObject> enemyPool;

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
        enemyPool = new List<GameObject>();

        // Instantiate bullets
        for(int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletObject);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        // Instantiate enemies
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyObject);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            bullet.SetActive(true);
            return bullet;
        }

        // If no inactive bullets found, create new ones
        GameObject newBullet = Instantiate(bulletObject);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        // If no inactive enemies found, create new ones
        GameObject newEnemy = Instantiate(enemyObject);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }
}
