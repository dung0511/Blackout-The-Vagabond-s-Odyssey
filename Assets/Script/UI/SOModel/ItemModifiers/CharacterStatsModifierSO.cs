using UnityEngine;

public abstract class CharacterStatsModifierSO : ScriptableObject
{
    public abstract void AffectCharcater(GameObject player, float value);
}

