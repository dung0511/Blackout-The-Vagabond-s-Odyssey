using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetailSO", menuName = "Scriptable Objects/WeaponDetailSO/ThrowableWeaponCurveDetailSO")]
public class ThrowableWeaponCurveSO : ScriptableObject
{
    #region Header Weapon BASE DETAILS
    [Space(10)]
    [Header("CURVE THROWABLE WEAPON BASE DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip("Curve throwable weapon name.")]
    #endregion
    public string throwableWeaponName;

    #region Tooltip
    [Tooltip("Prefab gameobject for the curve throwable weapon")]
    #endregion
    public GameObject throwableWeaponPrefab;

    #region Tooltip
    [Tooltip("Curve throwable weapon runtime animator controller")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;

    #region Header Damage
    [Space(10)]
    [Header("DAMAGE")]
    #endregion
    #region Tooltip
    [Tooltip("Damage of the throwable weapon")]
    #endregion
    public int damageThrowableWeapon;

    #region Header Fire Rate & Throwable Force
    [Space(10)]
    [Header("FIRE RATE & THROWABLE FORCE")]
    #endregion
    #region Tooltip
    [Tooltip("Time between each shot (lower value means faster throwing).")]
    #endregion
    public float TimeBtwFire;

    #region Tooltip
    [Tooltip("Force applied to the curve throwable weapon when threw.")]
    #endregion
    public float throwableForce;

    #region Validation
#if UNITY_EDITOR
    /// <summary>
    /// Validation method to check for null or empty values in the ScriptableObject.
    /// </summary>
    private void OnValidate()
    {
        Utility.ValidateCheckEmptyString(this, nameof(throwableWeaponName), throwableWeaponName);
        Utility.ValidateCheckNullValue(this, nameof(throwableWeaponPrefab), throwableWeaponPrefab);
        Utility.ValidateCheckPositiveValue(this, nameof(damageThrowableWeapon), damageThrowableWeapon, false);
        Utility.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
        Utility.ValidateCheckPositiveValue(this, nameof(TimeBtwFire), TimeBtwFire, false);
        Utility.ValidateCheckPositiveValue(this, nameof(throwableForce), throwableForce, false);
    }
#endif
    #endregion
}
