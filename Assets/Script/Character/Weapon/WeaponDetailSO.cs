using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetailSO", menuName = "Scriptable Objects/WeaponDetailSO")]
public class WeaponDetailSO : ScriptableObject
{
    #region Header Weapon DETAILS
    [Space(10)]
    [Header("WEAPON DETAILS")]
    #endregion

    #region Header Damage
    [Space(10)]
    [Header("DAMAGE")]
    #endregion

    #region Tooltip
    [Tooltip("Damage of weapon")]
    #endregion
    public int damageWeapon;

    #region Header Attack Cooldown
    [Space(10)]
    [Header("ATTACK COOLDOWN")]
    #endregion

    #region Tooltip
    [Tooltip("Number of attack each second")]
    #endregion
    public float attackCooldown;

    #region Tooltip
    [Tooltip("Force applied")]
    #endregion
    public float force;

    #region Validation
#if UNITY_EDITOR
    /// <summary>
    /// Validation method to check for null or empty values in the ScriptableObject.
    /// </summary>
    private void OnValidate()
    {
        Utility.ValidateCheckPositiveValue(this, nameof(damageWeapon), damageWeapon, false);
        Utility.ValidateCheckPositiveValue(this, nameof(attackCooldown), attackCooldown, false);
        Utility.ValidateCheckPositiveValue(this, nameof(force), force, false);
    }
#endif
    #endregion
}
