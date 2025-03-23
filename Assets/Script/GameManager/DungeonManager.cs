using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public string currentSeed; 

    #region Singleton
    public static DungeonManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentSeed = GameManager.Instance.levelSeeds.Dequeue();
    }
    #endregion


    public void OpenRoomBarrier(Room room)
    {
        Destroy(room.barrierReference);
    }    
}