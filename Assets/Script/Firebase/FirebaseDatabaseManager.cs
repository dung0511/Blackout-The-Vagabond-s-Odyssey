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
       // WriteDatabase(deviceId, "Stage 1.1", 100, 100);
        ReadDatabase(deviceId);
    }

    public void WriteDatabase(string id, string stageCame, int playTime, int enemiesKilled)
    {
        Dictionary<string, object> userData = new Dictionary<string, object>
    {
        { "stageCame", stageCame },
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

                    string stageCame = userData["stageCame"].ToString();
                    int playTime = int.Parse(userData["playTime"].ToString());
                    int enemiesKilled = int.Parse(userData["enemiesKilled"].ToString());
                    Debug.Log($"Player data:\n Man nguoi den: {stageCame}\n Thoi gian choi: {playTime} giay\n So quai da giet: {enemiesKilled}");
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

}
