using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public abstract void Attack();
    public abstract void RotateWeapon();
    public abstract void PickWeapon();
    public abstract void DropWeapon();
    public abstract float RotateToMousePos();
    public abstract WeaponDetailSO GetWeaponDetailSO();
   
}
