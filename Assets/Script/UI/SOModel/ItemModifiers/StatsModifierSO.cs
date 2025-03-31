using UnityEngine;

public abstract class StatsModifierSO : ScriptableObject
{
    public abstract void AffectCharcater(GameObject player, float value);
}

