using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected MapVisualizer mapVisualizer; 
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;
    [SerializeField] private string seed;

    private void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        seed = DungeonManager.Instance.currentSeed;
        Debug.Log($"Level seed: {seed}");
        UnityEngine.Random.InitState(seed.GetHashCode());
        mapVisualizer.ClearMap();
        RunProceduralGeneration();
    }

    [ContextMenu("Generate Dungeon")]
    public void GenerateDungeonEditor()
    {
        if(string.IsNullOrWhiteSpace(seed))
        {
            seed = Utility.GenerateRandomString(10);
        }
        Debug.Log($"Level seed: {seed}");
        UnityEngine.Random.InitState(seed.GetHashCode());
        mapVisualizer.ClearMap();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
