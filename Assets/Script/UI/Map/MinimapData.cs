using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MinimapData
{
    public static Dictionary<Vector2Int, TileBase> minimapTiles = new(); 
    public static HashSet<Vector2Int> revealedTiles = new();

    public static void Reset()
    {
        minimapTiles = new();
        revealedTiles = new();
    }
}

public class MinimapCell //
{
    public TileBase tile;
    public Vector2Int position;
}
