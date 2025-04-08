using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CharacterVariantSO playerCharacter;
    public int currentStage = 1;
    public int currentLevel = 1;
    public int maxStage = 3;
    public int levelsPerStage = 3;
    public string rootSeed;
    public Queue<string> levelSeeds = new(); //pre generate levels seed
    [SerializeField] private List<string> seedList = new(); //for editor view

    //long
    public int EnemyKilled = 0;
    public float TimePlayed = 0f;
    public void UpdateEnemyKilled()
    {
        EnemyKilled++;
    }

    private void OnApplicationQuit()
    {
        DataManager.Save();
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

        DataManager.Load();
    }
    #endregion

    

    public void SetPlayerCharacter(CharacterVariantSO player)
    {
        playerCharacter = player;
        DataManager.gameData.playerData.characterId = player.id;
    }

    public void StartDungeon()
    {
        var p = Instantiate(playerCharacter.dungeon, Vector3.zero, Quaternion.identity);  //hide player
        var ps = p.GetComponentsInChildren<SpriteRenderer>();
        foreach(var s in ps) s.enabled = false;

        GenerateLevelSeeds();   //dungeon seed
        seedList = new List<string>(levelSeeds);

        SceneManager.LoadScene("Dungeon");
    }

    public void NextLevel()
    {
        if(currentLevel > levelsPerStage) //next stage
        {
            currentLevel = 1;
            currentStage++;
            if(currentStage > DataManager.gameData.playerData.furthestStage)
            {
                DataManager.gameData.playerData.furthestStage = currentStage;
            }
        } else
        {
            currentLevel++;
        } 
        Debug.Log("Load Level: " + currentLevel + " Stage: " + currentStage);
        if(currentStage > maxStage) //game final boss level
        {
            SceneManager.LoadScene("Final_Boss");
        } else 
        if(currentLevel > levelsPerStage) //boss level at end of stage (level = num levels per stage + 1)
        {
            SceneManager.LoadScene("Boss_Stage"+ currentStage);
        } else //normal dungeon level
        {
            SceneManager.LoadScene("Dungeon");
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
        for(int i = 0; i < maxStage * levelsPerStage + maxStage +2; i++)  //+2 for safety
        {
            levelSeeds.Enqueue(Utility.GenerateRandomSeed(10));
        }
    }

    public void UpdatePlayerData()
    {
        
    }

}
