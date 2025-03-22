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
    public int MaxStage = 3;
    public int levelPerStage = 3;
    public string rootSeed;
    public Stack<string> levelSeeds = new(); //all level seeds pregenerated
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
    public static GameManager Instance {get; private set;} //singleton
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
        GameSceneManager.Instance.LoadScene("Dungeon");
        Instantiate(playerCharacter.dungeon, Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {

        currentLevel++;
        if(currentLevel > levelPerStage)
        {
            currentLevel = 0;
            GameSceneManager.Instance.LoadBossStageScene(currentStage++);
        } else 
        {
            GameSceneManager.Instance.ReloadScene();
        }
        //long
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
        for(int i = 0; i < MaxStage * levelPerStage; i++)
        {
            levelSeeds.Push(Utility.GenerateRandomSeed(10));
        }
    }

}
