using Firebase.Database;
using Firebase;
using UnityEngine;
using Firebase.Extensions;
using System.Collections.Generic;
using System;
using Microsoft.Win32;
using Zenject.SpaceFighter;

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

    public void WriteDatabase(string id, int floor, int stage, float playTime, int enemiesKilled)
    {
        string today = DateTime.UtcNow.ToString("dd-MM-yyyy");

        Dictionary<string, object> userData = new Dictionary<string, object>
    {
        { "floor", floor },
        { "stage", stage },
        { "playTime", playTime },
        { "enemiesKilled", enemiesKilled }
    };

        reference.Child("PlayerData").Child(id).Child(today).SetValueAsync(userData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Ghi du lieu thanh cong");
            }
            else
            {
                Debug.Log("Ghi du lieu that bai: " + task.Exception);
            }
        });
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

    public void UpdatePlayTimeAndEnemiesKilled(string id, int floor, int stage, float playTime, int enemiesKilled)
    {
        string today = DateTime.UtcNow.ToString("dd-MM-yyyy");

        reference.Child("PlayerData").Child(id).Child(today).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && task.Result.Exists)
            {
                DataSnapshot snapshot = task.Result;

                float oldPlayTime = float.Parse(snapshot.Child("playTime").Value.ToString());
                int oldEnemiesKilled = int.Parse(snapshot.Child("enemiesKilled").Value.ToString());
                int oldFloor = int.Parse(snapshot.Child("floor").Value.ToString());
                int oldStage = int.Parse(snapshot.Child("stage").Value.ToString());

               
                int updatedFloor = Mathf.Max(oldFloor, floor);
                int updatedStage = Mathf.Max(oldStage, stage);

                Dictionary<string, object> updatedData = new Dictionary<string, object>
            {
                { "floor", updatedFloor },
                { "stage", updatedStage },
                { "playTime", oldPlayTime + playTime },
                { "enemiesKilled", oldEnemiesKilled + enemiesKilled }
            };

                reference.Child("PlayerData").Child(id).Child(today).SetValueAsync(updatedData);
            }
            else
            {
               
                WriteDatabase(id, floor, stage, playTime, enemiesKilled);
            }
        });
    }

}
