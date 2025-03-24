using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    void Awake()
    {
        var seed = DungeonManager.Instance.currentSeed;
        Random.InitState(seed.GetHashCode());
        var bossIndex = Random.Range(0, BossManager.Instance.bossList.Count);
        var bossObj = Instantiate(BossManager.Instance.bossList[bossIndex], this.transform.position, Quaternion.identity);
    }
}
