using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected MapVisualizer mapVisualizer; 
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;
    [SerializeField] private bool editorMode = false;
    [SerializeField] private string seed;

    private void Start()
    {
        GenerateDungeon();
    }

    [ContextMenu("Generate Dungeon")]
    private void GenerateDungeon()
    {
        if(editorMode)
        {
            if(seed == null) seed = Utility.GenerateRandomSeed(10);
        } else 
        {
            seed = DungeonManager.Instance.currentSeed;
        }
        Debug.Log($"Dungeon seed: {seed}");
        UnityEngine.Random.InitState(seed.GetHashCode());
        mapVisualizer.ClearMap();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
