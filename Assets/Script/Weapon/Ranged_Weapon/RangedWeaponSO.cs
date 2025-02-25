using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetailSO", menuName = "Scriptable Objects/WeaponDetailSO/RangedWeaponDetailSO")]
public class RangedWeaponSO : ScriptableObject
{
    #region Header Weapon BASE DETAILS
    [Space(10)]
    [Header("RANGED WEAPON BASE DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip("Ranged weapon name.")]
    #endregion
    public string rangedWeaponName;

    #region Tooltip
    [Tooltip("Prefab gameobject for the ranged weapon")]
    #endregion
    public GameObject rangedWeaponPrefab;

    #region Tooltip
    [Tooltip("Ranged weapon runtime animator controller")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;

    #region Header Damage
    [Space(10)]
    [Header("DAMAGE")]
    #endregion
    #region Tooltip
    [Tooltip("Damage of the ranged weapon")]
    #endregion
    public int damageRangedWeapon;

    #region Validation
#if UNITY_EDITOR
    /// <summary>
    /// Validation method to check for null or empty values in the ScriptableObject.
    /// </summary>
    private void OnValidate()
    {
        Utility.ValidateCheckEmptyString(this, nameof(rangedWeaponName), rangedWeaponName);
        Utility.ValidateCheckNullValue(this, nameof(rangedWeaponPrefab), rangedWeaponPrefab);
        Utility.ValidateCheckPositiveValue(this, nameof(damageRangedWeapon), damageRangedWeapon, false);
        Utility.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
    }
#endif
    #endregion
}
