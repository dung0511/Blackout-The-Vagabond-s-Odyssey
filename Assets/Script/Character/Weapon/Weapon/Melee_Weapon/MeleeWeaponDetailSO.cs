using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetailSO", menuName = "Scriptable Objects/WeaponDetailSO/MeleeWeaponDetailSO")]
public class MeleeWeaponSO : ScriptableObject
{
    #region Header Weapon BASE DETAILS
    [Space(10)]
    [Header("MELEE WEAPON BASE DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip("Melee weapon name.")]
    #endregion
    public string meleeWeaponName;

    #region Tooltip
    [Tooltip("Prefab gameobject for the melee weapon")]
    #endregion
    public GameObject meleeWeaponPrefab;

    #region Tooltip
    [Tooltip("Melee weapon runtime animator controller")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;

    #region Header Damage
    [Space(10)]
    [Header("DAMAGE")]
    #endregion

    #region Tooltip
    [Tooltip("Damage of the melee weapon")]
    #endregion
    public int damageMeleeWeapon;

    #region Header Attack Cooldown
    [Space(10)]
    [Header("ATTACK COOLDOWN")]
    #endregion

    #region Tooltip
    [Tooltip("Time between each melee attack (in seconds).")]
    #endregion
    public float attackCooldown;

    #region Validation
#if UNITY_EDITOR
    /// <summary>
    /// Validation method to check for null or empty values in the ScriptableObject.
    /// </summary>
    private void OnValidate()
    {
        Utility.ValidateCheckEmptyString(this, nameof(meleeWeaponName), meleeWeaponName);
        Utility.ValidateCheckNullValue(this, nameof(meleeWeaponPrefab), meleeWeaponPrefab);
        Utility.ValidateCheckPositiveValue(this, nameof(damageMeleeWeapon), damageMeleeWeapon, false);
        Utility.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
        Utility.ValidateCheckPositiveValue(this, nameof(attackCooldown), attackCooldown, false);
    }
#endif
    #endregion
}
