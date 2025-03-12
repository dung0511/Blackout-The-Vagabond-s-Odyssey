using System.Collections.Generic;
using UnityEngine;

public class PoolManagement : MonoBehaviour
{
    public static PoolManagement Instance { get; private set; }

    [SerializeField] public List<GameObject> allBulletPrefab;
    private Dictionary<GameObject, Queue<GameObject>> bulletPools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePools();
        }
        
    }

    private void InitializePools()
    {
        bulletPools = new Dictionary<GameObject, Queue<GameObject>>();
        foreach (GameObject bulletPrefab in allBulletPrefab)
        {
            bulletPools[bulletPrefab] = new Queue<GameObject>();
        }
    }

    public GameObject GetBullet(GameObject bulletPrefab)
    {
        if (!bulletPools.ContainsKey(bulletPrefab))
        {
            Debug.LogError("BulletPrefab not found in pool!");
            return null;
        }

        if (bulletPools[bulletPrefab].Count > 0)
        {
            GameObject bullet = bulletPools[bulletPrefab].Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            return newBullet;
        }
    }

    public void ReturnBullet(GameObject bullet, GameObject bulletPrefab)
    {
        bullet.SetActive(false);
        bulletPools[bulletPrefab].Enqueue(bullet);
        
    }
}
