
using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

public class UnlockAfterStage : MonoBehaviour
{
    [SerializeField] private int stage = 1;

    void Awake()
    {
        var data = DataManager.gameData.playerData;
        bool unlocked = data.furthestStage > stage;

        gameObject.SetActive(unlocked);
    }
}