
using UnityEngine;

public class UnlockAfterStage : MonoBehaviour
{
    [SerializeField] private int stage = 1;

    void OnEnable()
    {
        var data = DataManager.gameData.playerData;
        bool unlocked = data.furthestStage > stage;
        gameObject.SetActive(unlocked);
    }
}