using UnityEngine;

public enum RangedWeaponFireType
{
    SingleShot,
    BurstShot,
    MultiShot
}


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

    #region Header Type
    [Space(10)]
    [Header("TYPE")]
    #endregion
    #region Tooltip
    [Tooltip("Type of shot")]
    #endregion
    public RangedWeaponFireType type;


    #region Header Damage
    [Space(10)]
    [Header("DAMAGE")]
    #endregion
    #region Tooltip
    [Tooltip("Damage of the ranged weapon")]
    #endregion
    public int damageRangedWeapon;

    #region Header Fire Rate & Bullet Force
    [Space(10)]
    [Header("FIRE RATE & BULLET FORCE")]
    #endregion
    #region Tooltip
    [Tooltip("Time between each shot (lower value means faster shooting).")]
    #endregion
    public float TimeBtwFire;

    #region Tooltip
    [Tooltip("Force applied to the bullet when fired.")]
    #endregion
    public float bulletForce;


    #region Header Bullet
    [Space(10)]
    [Header("BURST SHOT(ban tuan tu)")]
    #endregion
    #region Tooltip
    [Tooltip("BULLET OF BURST SHOT")]
    #endregion
    public float bulletSequentialShot;
    #region Tooltip
    [Tooltip("TIME BETWWEN EACH BULLET")]
    #endregion
    public float timeBtwEachBullet;

    
    [Space(10)]
    [Header(" MULTI-SHOT(ban cung luc)")]
    #region Tooltip
    [Tooltip("BULLET OF MULTI-SHOT")]
    #endregion
    public float bulletMultiShot;
    #region Tooltip
    [Tooltip("SPREAD ANGLE")]
    #endregion
    public float spreadAngle;

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
        Utility.ValidateCheckPositiveValue(this, nameof(TimeBtwFire), TimeBtwFire, false);
        Utility.ValidateCheckPositiveValue(this, nameof(bulletForce), bulletForce, false);
        Utility.ValidateCheckPositiveValue(this, nameof(bulletSequentialShot), bulletSequentialShot, false);
        Utility.ValidateCheckPositiveValue(this, nameof(timeBtwEachBullet), timeBtwEachBullet, false);
        Utility.ValidateCheckPositiveValue(this, nameof(bulletMultiShot), bulletMultiShot, false);
        Utility.ValidateCheckPositiveValue(this, nameof(spreadAngle), spreadAngle, false);
    }
#endif
    #endregion
}
