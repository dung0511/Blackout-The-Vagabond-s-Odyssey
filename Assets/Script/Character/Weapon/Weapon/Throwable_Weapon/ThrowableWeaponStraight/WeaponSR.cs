using Assets.Script.Weapon.Throwable_Weapon;
using UnityEngine;

public class WeaponSR : MonoBehaviour
{
    public void AttackEvent()
    {
        GetComponentInParent<ThrowableWeaponStraight>().AttackEvent();
    }
}
