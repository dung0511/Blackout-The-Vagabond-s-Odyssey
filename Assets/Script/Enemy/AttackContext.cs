using UnityEngine;

public enum AttackType
{
    Weapon,
    NormalSkill,
    UltimateSkill
}

public static class AttackContext
{
    public static AttackType CurrentAttackType { get; set; } = AttackType.Weapon;
}
