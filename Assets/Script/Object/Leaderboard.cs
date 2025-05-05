using System;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderboardTMP;

    void Awake()
    {
        leaderboardTMP.SetText("Loading...");

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            leaderboardTMP.SetText("No internet connection.");
            return;
        }   

        //fill
        if (FirebaseDatabaseManager.Instance != null)
        {
            string playerId = FirebaseDatabaseManager.Instance.GetOrCreatePlayerId();
            FirebaseDatabaseManager.Instance.GetFastestPlayTimes(10, leaderboard =>
            {
                string leaderboardText = "";

                for (int i = 0; i < leaderboard.Count; i++)
                {
                    var entry = leaderboard[i];
                    TimeSpan ts = TimeSpan.FromSeconds(entry.playTime);
                    string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                                         ts.Hours,
                                                         ts.Minutes,
                                                         ts.Seconds);
                    leaderboardText += $"{i + 1}. {(playerId == entry.playerId ? "You" : "Other")} - {formattedTime}\n";
                }

                leaderboardTMP.SetText(leaderboardText);
            });
        }


    }
}
