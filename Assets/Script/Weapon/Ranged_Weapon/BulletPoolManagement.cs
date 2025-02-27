using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManagement : MonoBehaviour
{
    public static BulletPoolManagement Instance { get; private set; }

    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<GameObject> pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        pool = new Queue<GameObject>();
        bulletPrefab = GetComponentInParent<RangedWeapon>().bullet;
    }

    private void Start()
    {

        
    }

    public GameObject GetGameObject(GameObject obj)
    {


        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(bulletPrefab);
        }

        obj.SetActive(true);
        return obj;
    }

    public void ReturnGameObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}