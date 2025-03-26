using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterVariantSO playerCharacter;
    public int currentStage = 1;
    public int currentLevel = 1;
    public int maxStage = 3;
    public int levelsPerStage = 3;
    public string rootSeed;
    public Queue<string> levelSeeds = new(); //all level seeds pregenerated
    [SerializeField] private List<string> seedList = new(); //For editor view

    //long
    public int EnemyKilled = 0;
    public float TimePlayed = 0f;
    public void UpdateEnemyKilled()
    {
        EnemyKilled++;
    }

    private void OnApplicationQuit()
    {
        TimePlayed = Time.time - TimePlayed;
        FirebaseDatabaseManager.Instance.UpdatePlayTimeAndEnemiesKilled(
            FirebaseDatabaseManager.Instance.GetOrCreatePlayerId(),
            currentLevel,
            currentStage,
            TimePlayed,
            EnemyKilled
        );
    }
    // long


    #region Singleton
    public static GameManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        GenerateLevelSeeds();
        seedList = new List<string>(levelSeeds);
    }
    #endregion

    public void SetPlayerCharacter(CharacterVariantSO player)
    {
        playerCharacter = player;
    }

    public void StartDungeon()
    {
        var p = Instantiate(playerCharacter.dungeon, Vector3.zero, Quaternion.identity);
        var ps = p.GetComponentsInChildren<SpriteRenderer>();
        foreach(var s in ps) s.enabled = false;
        GameSceneManager.Instance.LoadScene("Dungeon");
    }

    public void NextLevel()
    {

        currentLevel++;
        if(currentLevel > levelsPerStage)
        {
            currentLevel = 0;
            GameSceneManager.Instance.LoadBossStageScene(currentStage++);
        } else 
        {
            GameSceneManager.Instance.LoadScene("Dungeon");
        }
        //long //behind load scence, never reach
        TimePlayed = Time.time-TimePlayed;
        FirebaseDatabaseManager.Instance.UpdatePlayTimeAndEnemiesKilled(FirebaseDatabaseManager.Instance.GetOrCreatePlayerId(), currentLevel, currentStage, TimePlayed, EnemyKilled);
        //long
    }

    private void GenerateLevelSeeds()
    {
        if(String.IsNullOrWhiteSpace(rootSeed))
        {
            rootSeed = Utility.GenerateRandomSeed(10);
        } 
        UnityEngine.Random.InitState(rootSeed.GetHashCode());
        for(int i = 0; i < maxStage * levelsPerStage + maxStage; i++)
        {
            levelSeeds.Enqueue(Utility.GenerateRandomSeed(10));
        }
    }

}
