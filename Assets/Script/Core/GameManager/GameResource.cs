using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource : MonoBehaviour
{
    private static GameResource instance;

    public static GameResource Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResource>("GameResources");
            }
            return instance;
        }
    }

    #region Header PLAYER
    [Space(10)]
    [Header("PLAYER")]
    #endregion

    #region Tooltip
    [Tooltip("The current player scriptable object - this is used to reference the current player between scenes")]
    #endregion
    public CurrentPlayerSO currentPlayer;

    
}
