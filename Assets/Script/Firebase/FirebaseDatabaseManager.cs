using Firebase.Database;
using Firebase;
using UnityEngine;
using Firebase.Extensions;
using System.Collections.Generic;

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private void Start()
    {
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        UpdateStageAndFloor(deviceId, "2", "2" );
        ReadDatabase(deviceId);
    }

    public void WriteDatabase(string id, string floor, string stage, int playTime, int enemiesKilled)
    {
        Dictionary<string, object> userData = new Dictionary<string, object>
    {
        { "floor", floor },
        { "stage", stage },
        { "playTime", playTime },  
        { "enemiesKilled", enemiesKilled } 
    };

        reference.Child("PlayerData").Child(id).SetValueAsync(userData).ContinueWithOnMainThread(task =>
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
        reference.Child("PlayerData").Child(id).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    Dictionary<string, object> userData = snapshot.Value as Dictionary<string, object>;

                    string floor = userData["floor"].ToString();
                    string stage = userData["stage"].ToString();
                    int playTime = int.Parse(userData["playTime"].ToString());
                    int enemiesKilled = int.Parse(userData["enemiesKilled"].ToString());
                    Debug.Log($"Player data:\n Man nguoi den: {stage}, {floor}\n Thoi gian choi: {playTime} giay\n So quai da giet: {enemiesKilled}");
                }
                else
                {
                    Debug.Log("ko tim thay du lieu!");
                }
            }
            else
            {
                Debug.Log("That bai: " + task.Exception);
            }
        });
    }


    public void UpdateMonsterKillCount(string id, int newKillCount)
    {
        reference.Child("PlayerData").Child(id).Child("enemiesKilled").SetValueAsync(newKillCount)
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

        reference.Child("PlayerData").Child(id).UpdateChildrenAsync(updateData)
        .ContinueWithOnMainThread(task =>
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
}
