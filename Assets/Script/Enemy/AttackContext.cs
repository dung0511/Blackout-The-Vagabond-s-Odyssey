using UnityEngine;

public enum AttackType
{
    Weapon,
    Skill
}

public static class AttackContext
{
    public static AttackType CurrentAttackType { get; set; } = AttackType.Weapon;
}
