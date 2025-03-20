using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MaxStage = 3;
    public int levelPerStage = 3;
    public int currentStage = 1;
    public int currentLevel = 1;
    public Stack<string> levelSeeds = new(); //all level seeds pregenerated
    [SerializeField] private List<string> seedList = new(); //For editor view

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
        seedList = new List<string>(levelSeeds);
    }
    #endregion

    public void GenerateLevelSeeds()
    {
        for(int i = 0; i < MaxStage * levelPerStage + MaxStage; i++)
        {
            var seed = Utility.GenerateRandomSeed(10);
            levelSeeds.Push(seed);
        }
    }

}
