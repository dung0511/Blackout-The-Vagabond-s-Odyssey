using Firebase.Database;
using Firebase;
using UnityEngine;
using Firebase.Extensions;
using System.Collections.Generic;
using System;
using Microsoft.Win32;
using Zenject.SpaceFighter;
using UnityEngine.InputSystem;
using System.Collections;

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;
    public static FirebaseDatabaseManager Instance { get; private set; } //singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        // WriteDatabase(GetOrCreatePlayerId(),"1", 0, 0, 0, 0,0,false);
    }

    private const string PlayerIdKey = "playerId";

    public string GetOrCreatePlayerId()
    {
        string id = string.Empty;
        if (PlayerPrefs.HasKey(PlayerIdKey))
        {
            id = PlayerPrefs.GetString(PlayerIdKey);
        }
        else
        {
            id = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString(PlayerIdKey, id);
            PlayerPrefs.Save();
        }
        DataManager.gameData.playerData.playerId = id;
        Debug.Log("Player ID: " + id);
        return id;
    }

    //public void WriteDatabase(string playerId, string gameId, string characterPlayed, int floor, int stage, float playTime, int enemiesKilled, int bossesKilled, bool win, Dictionary<string, int> normalSkill, Dictionary<string, int> ultimateSkill, Dictionary<string,int> weapons)
    //{
    //    string today = DateTime.UtcNow.ToString("dd-MM-yyyy");
    //    var gamePath = reference.Child("PlayerData").Child(playerId).Child(today).Child(gameId);


    //    Dictionary<string, object> userData = new Dictionary<string, object>
    //{
    //    { "characterPlayed", characterPlayed },
    //    { "floor", floor },
    //    { "stage", stage },
    //    { "playTime", playTime },
    //    { "totalEnemiesKilled", enemiesKilled },
    //    { "bossesKilled", bossesKilled },
    //    { "win", win },
    //    //{ "normalSkillUsed", NormalSkillUsed },
    //   // { "ultimateSkillUsed", UltimateSkillUsed }

    //};
    //    // update weapon
    //    gamePath.SetValueAsync(userData).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log("Write data sucess");


    //            foreach (var kvp in weapons)
    //            {

    //                string weaponName = kvp.Key;
    //                int kills = kvp.Value;

    //                gamePath.Child("weaponsUsed").Child(weaponName).SetValueAsync(kills).ContinueWithOnMainThread(subTask =>
    //                {
    //                    if (subTask.IsCompleted)
    //                    {
    //                        Debug.Log($"Write weapon {weaponName} = {kills} done");
    //                    }
    //                    else
    //                    {
    //                        Debug.LogError($"Write weapon {weaponName} false: {subTask.Exception?.Flatten().Message}");
    //                    }
    //                });
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Ghi data sucess");
    //            if (task.Exception != null)
    //            {
    //                foreach (var ex in task.Exception.Flatten().InnerExceptions)
    //                {
    //                    Debug.LogError("Firebase exception: " + ex.Message);
    //                }
    //            }
    //        }
    //    });

    //    //update skill normal
    //    gamePath.SetValueAsync(userData).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log("Write data sucess");


    //            foreach (var kvp in normalSkill)
    //            {

    //                string key = kvp.Key;
    //                int value = kvp.Value;

    //                gamePath.Child("normalSkill").Child(key).SetValueAsync(value).ContinueWithOnMainThread(subTask =>
    //                {
    //                    if (subTask.IsCompleted)
    //                    {
    //                        Debug.Log($"Write normalSkill {key} = {value} done");
    //                    }
    //                    else
    //                    {
    //                        Debug.LogError($"Write normalSkill {key} false: {subTask.Exception?.Flatten().Message}");
    //                    }
    //                });
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Ghi data sucess");
    //            if (task.Exception != null)
    //            {
    //                foreach (var ex in task.Exception.Flatten().InnerExceptions)
    //                {
    //                    Debug.LogError("Firebase exception: " + ex.Message);
    //                }
    //            }
    //        }
    //    });

    //    //update skill ultimate
    //    gamePath.SetValueAsync(userData).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log("Write data sucess");


    //            foreach (var kvp in ultimateSkill)
    //            {

    //                string key = kvp.Key;
    //                int value = kvp.Value;

    //                gamePath.Child("ultimateSkill").Child(key).SetValueAsync(value).ContinueWithOnMainThread(subTask =>
    //                {
    //                    if (subTask.IsCompleted)
    //                    {
    //                        Debug.Log($"Write ultimateSkill {key} = {value} done");
    //                    }
    //                    else
    //                    {
    //                        Debug.LogError($"Write ultimateSkill {key} false: {subTask.Exception?.Flatten().Message}");
    //                    }
    //                });
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Ghi data sucess");
    //            if (task.Exception != null)
    //            {
    //                foreach (var ex in task.Exception.Flatten().InnerExceptions)
    //                {
    //                    Debug.LogError("Firebase exception: " + ex.Message);
    //                }
    //            }
    //        }
    //    });
    //}

    public void WriteDatabase(
    string playerId,
    string gameId,
    string characterPlayed,
    int floor,
    int stage,
    float playTime,
    int enemiesKilled,
    int bossesKilled,
    bool win,
    Dictionary<string, int> normalSkill,
    Dictionary<string, int> ultimateSkill,
    Dictionary<string, int> weapons)
    {
     
        string playDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

        var playerData = new Dictionary<string, object>
    {
        { "PlayerId", playerId }
    };
        reference.Child("Players").Child(playerId).GetValueAsync().ContinueWithOnMainThread(playerDataTask =>
        {
            if (!playerDataTask.Result.Exists)
            {
               
                reference.Child("Players").Child(playerId).SetValueAsync(playerData)
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            Debug.Log("Player data written successfully.");
                        }
                        else
                        {
                            Debug.LogError("Failed to write player data: " + task.Exception?.Flatten().Message);
                        }
                    });
            }
            else
            {
                Debug.Log("Player data already exists. Skipping write.");
            }
        });


        var gameData = new Dictionary<string, object>
    {
        { "PlayerId", playerId },
        { "PlayDate", playDate },
        { "PlayTime", playTime },
        { "CharacterPlayed", characterPlayed },
        { "Floor", floor },
        { "Stage", stage },
        { "BossesKilled", bossesKilled },
        { "TotalEnemiesKilled", enemiesKilled },
        { "Win", win }
    };
        reference.Child("Games").Child(gameId).SetValueAsync(gameData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Games data written successfully.");
                }
                else
                {
                    Debug.LogError("Failed to write Games data: " + task.Exception?.Flatten().Message);
                }
            });

        
        foreach (var kvp in normalSkill)
        {
           
            reference.Child("Skills").Child(gameId).Child("Normal").Child(kvp.Key).SetValueAsync(kvp.Value)
                .ContinueWithOnMainThread(subTask =>
                {
                    if (subTask.IsCompleted)
                    {
                        Debug.Log($"Normal skill '{kvp.Key}' with value {kvp.Value} written successfully.");
                    }
                    else
                    {
                        Debug.LogError($"Failed to write normal skill {kvp.Key}: " + subTask.Exception?.Flatten().Message);
                    }
                });
        }

        
        foreach (var kvp in ultimateSkill)
        {
            reference.Child("Skills").Child(gameId).Child("Ultimate").Child(kvp.Key).SetValueAsync(kvp.Value)
                .ContinueWithOnMainThread(subTask =>
                {
                    if (subTask.IsCompleted)
                    {
                        Debug.Log($"Ultimate skill '{kvp.Key}' with value {kvp.Value} written successfully.");
                    }
                    else
                    {
                        Debug.LogError($"Failed to write ultimate skill {kvp.Key}: " + subTask.Exception?.Flatten().Message);
                    }
                });
        }

        
        foreach (var kvp in weapons)
        {
            reference.Child("WeaponsUsed").Child(gameId).Child(kvp.Key).SetValueAsync(kvp.Value)
                .ContinueWithOnMainThread(subTask =>
                {
                    if (subTask.IsCompleted)
                    {
                        Debug.Log($"Weapon '{kvp.Key}' with usage {kvp.Value} written successfully.");
                    }
                    else
                    {
                        Debug.LogError($"Failed to write weapon {kvp.Key}: " + subTask.Exception?.Flatten().Message);
                    }
                });
        }
    }



    public void ReadDatabase(string id)
    {
        string today = DateTime.UtcNow.ToString("dd-MM-yyyy");


        reference.Child("PlayerData").Child(id).Child(today).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {

                    Dictionary<string, object> userData = snapshot.Value as Dictionary<string, object>;

                    int floor = int.Parse(userData["floor"].ToString());
                    int stage = int.Parse(userData["stage"].ToString());
                    float playTime = float.Parse(userData["playTime"].ToString());
                    int enemiesKilled = int.Parse(userData["enemiesKilled"].ToString());

                    Debug.Log($"Player data {id}- Day {today}:\n Man nguoi den: {stage}, {floor}\n Thoi gian choi: {playTime} giay\n So quai da giet: {enemiesKilled}");
                    //Debug.Log("123");
                }
                else
                {
                    Debug.Log($" CAnt find [{id}] on {today}!");
                }
            }
            else
            {
                Debug.LogError($" Error reading data: {task.Exception}");
            }
        });
    }

    public void ReadDatabaseByDate(string id, string date)
    {

        reference.Child("PlayerData").Child(id).Child(date).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {

                    Dictionary<string, object> userData = new Dictionary<string, object>();

                    foreach (var child in snapshot.Children)
                    {
                        userData[child.Key] = child.Value;
                    }


                    if (userData.ContainsKey("floor") && userData.ContainsKey("stage") &&
                        userData.ContainsKey("playTime") && userData.ContainsKey("enemiesKilled"))
                    {
                        int floor = int.Parse(userData["floor"].ToString());
                        int stage = int.Parse(userData["stage"].ToString());
                        float playTime = float.Parse(userData["playTime"].ToString());
                        int enemiesKilled = int.Parse(userData["enemiesKilled"].ToString());

                        Debug.Log($"Player data [{id}] - Day: {date}:\n" +
                                  $" Floor: {floor}, Stage: {stage}\n" +
                                  $" Time played: {playTime} sec\n" +
                                  $" Enemies killed: {enemiesKilled}");
                    }
                    else
                    {
                        Debug.Log($" [{id}] missing.");
                    }
                }
                else
                {
                    Debug.Log($" CAnt find [{id}] on {date}!");
                }
            }
            else
            {
                Debug.LogError($" Error reading data: {task.Exception}");
            }
        });
    }


    public void UpdateMonsterKillCount(string id, int newKillCount)
    {
        string today = DateTime.UtcNow.ToString("dd-MM-yyyy");
        reference.Child("PlayerData").Child(id).Child(today).Child("enemiesKilled").SetValueAsync(newKillCount)
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("update enemy kill success!");
            }
            else
            {
                Debug.Log("update enemy kill fail: " + task.Exception);
            }
        });
    }

    public void UpdateStageAndFloor(string id, string newFloor, string newStage)
    {
        Dictionary<string, object> updateData = new Dictionary<string, object>
    {
        { "floor", newFloor },
        { "stage", newStage }
    };

        string today = DateTime.UtcNow.ToString("dd-MM-yyyy");

        reference.Child("PlayerData").Child(id).Child(today).UpdateChildrenAsync(updateData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("update floor and stage success!");
            }
            else
            {
                Debug.Log("update floor and stage fail " + task.Exception);
            }
        });
    }

    //public void UpdatePlayTimeAndEnemiesKilled(string id, string gameId, string characterPlayed, int floor, int stage, float playTime, int enemiesKilled, int bossKilled, bool win, Dictionary<string, int> normalSkill, Dictionary<string, int> ultimateSkill, Dictionary<string, int> weapons)
    //{
    //    string today = DateTime.UtcNow.ToString("dd-MM-yyyy");
    //    var basePath = reference.Child("PlayerData").Child(id).Child(today).Child(gameId);

    //    reference.Child("PlayerData").Child(id).Child(today).Child(gameId).GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted && task.Result.Exists)
    //        {
    //            DataSnapshot snapshot = task.Result;

    //            float oldPlayTime = float.Parse(snapshot.Child("playTime").Value.ToString());


    //            Dictionary<string, object> updatedData = new Dictionary<string, object>
    //        {
    //            { "characterPlayed", characterPlayed },
    //            { "floor", floor },
    //            { "stage", stage },
    //            { "playTime", oldPlayTime + playTime },
    //            { "win", win },

    //        };


    //            basePath.UpdateChildrenAsync(updatedData).ContinueWithOnMainThread(updateTask =>
    //            {
    //                if (updateTask.IsCompleted)
    //                {
    //                    Debug.Log("update main data sucess");


    //                    foreach (var kvp in weapons)
    //                    {
    //                        string weaponName = kvp.Key;
    //                        int kills = kvp.Value;

    //                        basePath.Child("weaponsUsed").Child(weaponName).SetValueAsync(kills).ContinueWithOnMainThread(weaponTask =>
    //                        {
    //                            if (weaponTask.IsCompleted)
    //                            {
    //                                Debug.Log($"Updated {weaponName} = {kills}");
    //                            }
    //                            else
    //                            {
    //                                Debug.LogError($"Error write weapon {weaponName}: {weaponTask.Exception?.Flatten().Message}");
    //                            }
    //                        });
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.LogError("error update data");
    //                }
    //            });

    //            //update skill normal
    //            basePath.UpdateChildrenAsync(updatedData).ContinueWithOnMainThread(task =>
    //            {
    //                if (task.IsCompleted)
    //                {
    //                    Debug.Log("Write data sucess");


    //                    foreach (var kvp in normalSkill)
    //                    {

    //                        string key = kvp.Key;
    //                        int value = kvp.Value;

    //                        basePath.Child("normalSkill").Child(key).SetValueAsync(value).ContinueWithOnMainThread(subTask =>
    //                        {
    //                            if (subTask.IsCompleted)
    //                            {
    //                                Debug.Log($"Write normalSkill {key} = {value} done");
    //                            }
    //                            else
    //                            {
    //                                Debug.LogError($"Write normalSkill {key} false: {subTask.Exception?.Flatten().Message}");
    //                            }
    //                        });
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.LogError("Ghi data sucess");
    //                    if (task.Exception != null)
    //                    {
    //                        foreach (var ex in task.Exception.Flatten().InnerExceptions)
    //                        {
    //                            Debug.LogError("Firebase exception: " + ex.Message);
    //                        }
    //                    }
    //                }
    //            });

    //            //update skill ultimate
    //            basePath.UpdateChildrenAsync(updatedData).ContinueWithOnMainThread(task =>
    //            {
    //                if (task.IsCompleted)
    //                {
    //                    Debug.Log("Write data sucess");


    //                    foreach (var kvp in ultimateSkill)
    //                    {

    //                        string key = kvp.Key;
    //                        int value = kvp.Value;

    //                        basePath.Child("ultimateSkill").Child(key).SetValueAsync(value).ContinueWithOnMainThread(subTask =>
    //                        {
    //                            if (subTask.IsCompleted)
    //                            {
    //                                Debug.Log($"Write ultimateSkill {key} = {value} done");
    //                            }
    //                            else
    //                            {
    //                                Debug.LogError($"Write ultimateSkill {key} false: {subTask.Exception?.Flatten().Message}");
    //                            }
    //                        });
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.LogError("Ghi data sucess");
    //                    if (task.Exception != null)
    //                    {
    //                        foreach (var ex in task.Exception.Flatten().InnerExceptions)
    //                        {
    //                            Debug.LogError("Firebase exception: " + ex.Message);
    //                        }
    //                    }
    //                }
    //            });
    //        }
    //        else
    //        {

    //            WriteDatabase(id, gameId, characterPlayed, floor, stage, playTime, enemiesKilled, bossKilled, win, normalSkill, ultimateSkill, weapons);
    //        }
    //    });
    //}


    public void UpdatePlayTimeAndEnemiesKilled(
    string id,
    string gameId,
    string characterPlayed,
    int floor,
    int stage,
    float playTime,
    int enemiesKilled,
    int bossKilled,
    bool win,
    Dictionary<string, int> normalSkill,
    Dictionary<string, int> ultimateSkill,
    Dictionary<string, int> weapons)
    {
        
        string playDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

       
        var gameRef = reference.Child("Games").Child(gameId);

      
        gameRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && task.Result.Exists)
            {
                DataSnapshot snapshot = task.Result;

               
                float oldPlayTime = snapshot.Child("PlayTime").Exists ? float.Parse(snapshot.Child("PlayTime").Value.ToString()) : 0f;
                int oldEnemiesKilled = snapshot.Child("TotalEnemiesKilled").Exists ? int.Parse(snapshot.Child("TotalEnemiesKilled").Value.ToString()) : 0;

             
                Dictionary<string, object> updatedData = new Dictionary<string, object>
            {
                { "CharacterPlayed", characterPlayed },
                { "Floor", floor },
                { "Stage", stage },
                { "PlayTime", oldPlayTime + playTime },
                { "TotalEnemiesKilled", oldEnemiesKilled + enemiesKilled },
                { "BossesKilled", bossKilled },
                { "Win", win },
                
                   
            };

                gameRef.UpdateChildrenAsync(updatedData).ContinueWithOnMainThread(updateTask =>
                {
                    if (updateTask.IsCompleted)
                    {
                        Debug.Log("Main Games data updated successfully.");

                       
                        var weaponsRef = reference.Child("WeaponsUsed").Child(gameId);
                        foreach (var kvp in weapons)
                        {
                            weaponsRef.Child(kvp.Key).SetValueAsync(kvp.Value)
                                .ContinueWithOnMainThread(weaponTask =>
                                {
                                    if (weaponTask.IsCompleted)
                                    {
                                        Debug.Log($"Weapon '{kvp.Key}' updated successfully with value {kvp.Value}");
                                    }
                                    else
                                    {
                                        Debug.LogError($"Error updating weapon '{kvp.Key}': {weaponTask.Exception?.Flatten().Message}");
                                    }
                                });
                        }

                       
                        var normalSkillRef = reference.Child("Skills").Child(gameId).Child("Normal");
                        foreach (var kvp in normalSkill)
                        {
                            normalSkillRef.Child(kvp.Key).SetValueAsync(kvp.Value)
                                .ContinueWithOnMainThread(normalTask =>
                                {
                                    if (normalTask.IsCompleted)
                                    {
                                        Debug.Log($"Normal skill '{kvp.Key}' updated successfully with value {kvp.Value}");
                                    }
                                    else
                                    {
                                        Debug.LogError($"Error updating normal skill '{kvp.Key}': {normalTask.Exception?.Flatten().Message}");
                                    }
                                });
                        }

                       
                        var ultimateSkillRef = reference.Child("Skills").Child(gameId).Child("Ultimate");
                        foreach (var kvp in ultimateSkill)
                        {
                            ultimateSkillRef.Child(kvp.Key).SetValueAsync(kvp.Value)
                                .ContinueWithOnMainThread(ultimateTask =>
                                {
                                    if (ultimateTask.IsCompleted)
                                    {
                                        Debug.Log($"Ultimate skill '{kvp.Key}' updated successfully with value {kvp.Value}");
                                    }
                                    else
                                    {
                                        Debug.LogError($"Error updating ultimate skill '{kvp.Key}': {ultimateTask.Exception?.Flatten().Message}");
                                    }
                                });
                        }
                    }
                    else
                    {
                        Debug.LogError("Error updating Games main data: " + updateTask.Exception?.Flatten().Message);
                    }
                });
            }
            else
            {
              
                Debug.Log("Game data does not exist, calling WriteDatabase to create new entry.");
                WriteDatabase(id, gameId, characterPlayed, floor, stage, playTime, enemiesKilled, bossKilled, win, normalSkill, ultimateSkill, weapons);
            }
        });
    }



}
