using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderboardTMP;

    void Awake()
    {
        //fill

        leaderboardTMP.SetText("Leaderboard\n" +
            "1. Player 1\n" +
            "2. Player 2\n" +
            "3. Player 3\n" +
            "4. Player 4\n" +
            "5. Player 5\n" +
            "6. Player 6\n" +
            "7. Player 7\n" +
            "8. Player 8\n" +
            "9. Player 9\n" +
            "10. Player 10");
    }
}
