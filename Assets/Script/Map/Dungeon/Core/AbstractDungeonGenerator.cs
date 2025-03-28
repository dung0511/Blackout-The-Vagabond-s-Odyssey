using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected MapVisualizer mapVisualizer; 
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;
    private string seed;

    private void Start()
    {
        GenerateDungeon();
    }

    [ContextMenu("Generate Dungeon")]
    private void GenerateDungeon()
    {
        seed = DungeonManager.Instance.currentSeed;
        Debug.Log($"Level seed: {seed}");
        UnityEngine.Random.InitState(seed.GetHashCode());
        mapVisualizer.ClearMap();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
