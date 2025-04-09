using UnityEngine;

public abstract class StatsModifierSO : ScriptableObject
{
    public abstract void AffectCharacter(GameObject player, float value);
}

