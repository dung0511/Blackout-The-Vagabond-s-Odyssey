using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxStage = 3;
    public int levelsPerStage = 3;
    public int currentStage = 1;
    public int currentLevel = 1;
    public string rootSeed;
    public Queue<string> levelSeeds = new(); //all level seeds pregenerated
    [SerializeField] private List<string> seedList = new(); //For editor view

    public void NextLevel()
    {
        currentLevel++;
        if(currentLevel > levelsPerStage)
        {
            currentLevel = 0;
            GameSceneManager.Instance.LoadStageBossScene(currentStage++);
        } else 
        {
            GameSceneManager.Instance.ReloadScene();
        }
    }

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
        seedList = new List<string>(levelSeeds); //For editor view
    }
    #endregion

    public void GenerateLevelSeeds()
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
        seedList = new List<string>(levelSeeds);
    }

}
