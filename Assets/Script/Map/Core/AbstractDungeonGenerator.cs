using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected MapVisualizer mapVisualizer; 
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;
    [SerializeField] protected string seed;

    private void Start()
    {
        GenerateDungeon();
    }

    [ContextMenu("Generate Dungeon")]
    private void GenerateDungeon()
    {
        if(seed.Trim().Length == 0) seed = GenerateRandomSeed(10);
        UnityEngine.Random.InitState(seed.GetHashCode());
        mapVisualizer.ClearMap();
        RunProceduralGeneration();
    }

    private string GenerateRandomSeed(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[UnityEngine.Random.Range(0, chars.Length)];
        }
        return new string(result);
    }

    protected abstract void RunProceduralGeneration();
}
