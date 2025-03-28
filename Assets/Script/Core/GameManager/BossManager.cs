using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossManager : MonoBehaviour
{
    public List<GameObject> bossList;
    public UnityEvent bossKillEvent; 

    #region Singleton
    public static BossManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

}