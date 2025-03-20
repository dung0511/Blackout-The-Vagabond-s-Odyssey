using UnityEngine;

public interface IWeaponRepository
{
    void SaveWeaponState(GameObject weapon);
    GameObject LoadWeaponState(string weaponId);
}
