using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossManager : MonoBehaviour
{
    public List<GameObject> bossList;
    [SerializeField] private GameObject bossKillObject; //stuff spawn after boss kill
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
        var seed = DungeonManager.Instance.currentSeed;
        Random.InitState(seed.GetHashCode());
        bossKillEvent.AddListener(OnBossKilled);
    }
    #endregion

    private void OnBossKilled()
    {
        bossKillObject.SetActive(true);


        GameManager.Instance.PlayCurrentStageDungeonMusic();
        DataManager.gameData.playerData.furthestStage = Mathf.Max(DataManager.gameData.playerData.furthestStage, GameManager.Instance.currentStage + 1);
    }
}