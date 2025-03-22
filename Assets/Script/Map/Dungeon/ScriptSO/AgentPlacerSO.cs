using UnityEngine;

[CreateAssetMenu(fileName = "AgentPlacer", menuName = "Scriptable Objects/Enemy/AgentPlacer")]
public class AgentPlacerSO : ScriptableObject
{
    public GameObject agentPrefab;
    public int spriteHeight = 1;
    public Vector2Int size = Vector2Int.zero;
}
