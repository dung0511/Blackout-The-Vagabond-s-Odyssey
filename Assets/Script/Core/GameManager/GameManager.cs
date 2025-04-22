using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CharacterVariantSO playerCharacter;
    public int currentStage = 0;
    public int currentLevel = 0;
    public int maxStage = 3;
    public int levelsPerStage = 3;
    public string gameId;
    public string rootSeed;
    public Queue<string> levelSeeds = new(); //pre generate levels seed
    [SerializeField] private List<string> seedList = new(); //for editor view

    //long
    public string playerId;
    public string characterPlayed;
    public int EnemyKilled = 0;
    public int BossKilled = 0;
    public float TimePlayed = 0f;
    public bool win = false;

    public Dictionary<string, int> weaponuUsed = new Dictionary<string, int>();
    public Dictionary<string, int> normalSkill = new Dictionary<string, int>
    {
        {"usageCount", 0 },
        {"enemiesKilled",0 }

    };
    public Dictionary<string, int> ultimateSkill = new Dictionary<string, int>
    {
        {"usageCount", 0 },
        {"enemiesKilled",0 }
    };
    public string weaponUsing;


    [SerializeField] private MusicLibrarySO musicLibrary;
    public MusicLibrarySO MusicLibrary => musicLibrary;


    private void Start()
    {
        playerId = FirebaseDatabaseManager.Instance.GetOrCreatePlayerId();
    }

    public void UpdateWeaponUsed()
    {
        if (weaponuUsed.ContainsKey(weaponUsing))
        {
            weaponuUsed[weaponUsing]++;
        }
        else
        {
            weaponuUsed.Add(weaponUsing, 1);
        }
    }
    public void SetWeaponUsing(string weapon)
    {
        weaponUsing = weapon;
    }

    public void UpdateEnemyKilled()
    {
        EnemyKilled++;
    }

    public void UpdateNormalSkillUsed()
    {
        normalSkill["usageCount"]++;
    }

    public void UpdateEnemyKilledByNormalSkill()
    {
        normalSkill["enemiesKilled"]++;
    }

    public void UpdateUltimateSkillUsed()
    {
        ultimateSkill["usageCount"]++;
    }
    public void UpdateEnemyKilledByUltimateSkill()
    {
        ultimateSkill["enemiesKilled"]++;
    }
    public void UpdateBossKill()
    {
        BossKilled++;
    }
    private void OnApplicationQuit()
    {
        DataManager.Save();
        TimePlayed = Time.time - TimePlayed;
        if (!gameId.IsEmpty())
        {
            FirebaseDatabaseManager.Instance.UpdatePlayTimeAndEnemiesKilled(
            playerId,
            gameId,
            characterPlayed,
            currentLevel,
            currentStage,
            TimePlayed,
            EnemyKilled,
            BossKilled,
            win,
            normalSkill,
            ultimateSkill,
            weaponuUsed
        );
        }
        
    }
    // long

    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    public void SetPlayerCharacter(CharacterVariantSO player)
    {
        playerCharacter = player;
        DataManager.gameData.playerData.characterId = player.id;
        characterPlayed = player.characterName;
    }

    public void StartDungeon()
    {
        currentLevel = 1;
        currentStage = 1;

        string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

        gameId = $"{timestamp}_{Guid.NewGuid().ToString()}";

        weaponUsing = "Piston";
        var p = Instantiate(playerCharacter.dungeon, Vector3.zero, Quaternion.identity);  //hide player
        var ps = p.GetComponentsInChildren<SpriteRenderer>();
        foreach (var s in ps) s.enabled = false;

        GenerateLevelSeeds();   //dungeon seed
        seedList = new List<string>(levelSeeds);

        TimePlayed = Time.time - TimePlayed;
        FirebaseDatabaseManager.Instance.WriteDatabase(playerId, gameId, characterPlayed, currentLevel, currentStage, TimePlayed, EnemyKilled, BossKilled, win, normalSkill, ultimateSkill, weaponuUsed);

        SceneManager.LoadScene("Stage1");
        HUD.Instance.ShowHUD();
        SkillCooldownUI.Instance.InitializeSkillIcons();
        //ShopManager.Instance.ResetCoins();
    }

    public void NextLevel()
    {
        if (currentLevel > levelsPerStage) //next stage
        {
            currentLevel = 1;
            currentStage++;
            if (currentStage > DataManager.gameData.playerData.furthestStage)
            {
                DataManager.gameData.playerData.furthestStage = currentStage;
            }
        }
        else
        {
            currentLevel++;
        }
        Debug.Log("Load Level: " + currentLevel + " Stage: " + currentStage);

        //long
        TimePlayed = Time.time - TimePlayed;
        FirebaseDatabaseManager.Instance.UpdatePlayTimeAndEnemiesKilled(playerId, gameId, characterPlayed, currentLevel, currentStage, TimePlayed, EnemyKilled, BossKilled, win, normalSkill, ultimateSkill, weaponuUsed);
        //long

        if (currentStage > maxStage) //game final boss level
        {
            SceneManager.LoadScene("Final_Boss");
        }
        else
        if (currentLevel > levelsPerStage) //boss level at end of stage (level = num levels per stage + 1)
        {
            SceneManager.LoadScene("Boss_Stage" + currentStage);
        }
        else //normal dungeon level
        {
            if(currentStage > 2) SceneManager.LoadScene("Stage1");
            else SceneManager.LoadScene("Stage" + currentStage);
            
        }

    }

    private void GenerateLevelSeeds()
    {
        if (String.IsNullOrWhiteSpace(rootSeed))
        {
            rootSeed = Utility.GenerateRandomString(10);
        }
        UnityEngine.Random.InitState(rootSeed.GetHashCode());
        for (int i = 0; i < maxStage * levelsPerStage + maxStage + 2; i++)  //+2 for safety
        {
            levelSeeds.Enqueue(Utility.GenerateRandomString(10));
        }
    }

    #region Music
    public void PlayCurrentStageDungeonMusic()
    {
        try
        {
            if (currentStage > maxStage) // Final boss scene
            {
                var finalBossstageMusic = musicLibrary.GetStageMusic(currentStage - 2);  // lay nhac stage 3 tam 
                if (finalBossstageMusic != null)
                {
                    var clip = finalBossstageMusic.GetRandomDungeonMusic();
                    if (clip != null)
                        MusicManager.Instance?.PlayMusic(clip, 1f);
                }
                return;
            }

            var stageMusic = musicLibrary.GetStageMusic(currentStage - 1);
            if (stageMusic != null)
            {
                var clip = stageMusic.GetRandomDungeonMusic();
                if (clip != null)
                    MusicManager.Instance?.PlayMusic(clip, 1f);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"PlayCurrentStageDungeonMusic failed: {e.Message}");
        }
    }

    public void PlayCurrentStageEliteMusic()
    {
        try
        {
            if (currentStage > maxStage + 1) return;

            var stageMusic = musicLibrary.GetStageMusic(currentStage - 1);
            if (stageMusic != null)
            {
                var clip = stageMusic.GetRandomEliteMusic();
                if (clip != null)
                    MusicManager.Instance?.PlayMusic(clip);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"PlayCurrentStageEliteMusic failed: {e.Message}");
        }
    }

    public void PlayCurrentStageBossMusic()
    {
        try
        {
            if (currentStage > maxStage)
            {
                var clip = musicLibrary.finalBossMusic;
                if (clip != null)
                    MusicManager.Instance?.PlayMusic(clip);
            }
            else
            {
                var stageMusic = musicLibrary.GetStageMusic(currentStage - 1);
                if (stageMusic != null)
                {
                    var clip = stageMusic.GetRandomBossMusic();
                    if (clip != null)
                        MusicManager.Instance?.PlayMusic(clip);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"PlayCurrentStageBossMusic failed: {e.Message}");
        }
    }

    #endregion


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ShopManager.Instance.ResetCoins();
    }
}
