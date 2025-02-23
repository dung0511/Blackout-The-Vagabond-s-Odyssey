using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailSO : ScriptableObject
{
    #region Header PLAYER BASE DETAILS
    [Space(10)]
    [Header("PLAYER BASE DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip("Player character name.")]
    #endregion
    public string playerCharacterName;

    #region Tooltip
    [Tooltip("Prefab gameobject for the player")]
    #endregion
    public GameObject playerPrefab;

    #region Tooltip
    [Tooltip("Player runtime animator controller")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;

    #region Header HEALTH
    [Space(10)]
    [Header("HEALTH")]
    #endregion
    #region Tooltip
    [Tooltip("Player starting health amount")]
    #endregion
    public int playerHealthAmount;

    #region Header ARMOR
    [Space(10)]
    [Header("ARMOR")]
    #endregion
    #region Tooltip
    [Tooltip("Player starting armor amount")]
    #endregion
    public int playerArmorAmount;

    #region Validation
#if UNITY_EDITOR
    /// <summary>
    /// Validation method to check for null or empty values in the ScriptableObject.
    /// </summary>
    private void OnValidate()
    {
        Utility.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        Utility.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        Utility.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount, false);
        Utility.ValidateCheckPositiveValue(this, nameof(playerArmorAmount), playerArmorAmount, false);
        Utility.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
    }
#endif
    #endregion
}
