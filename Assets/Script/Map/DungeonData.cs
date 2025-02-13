using System.Collections.Generic;
using UnityEngine;

public static class DungeonData
{
    public static HashSet<RectangleRoom> rooms = new();
    public static HashSet<Vector2Int> path = new();
    public static HashSet<Vector2Int> corridorPath = new();

    public static void Reset()
    {
        rooms = new();
        path = new();
        corridorPath = new();
    }
}
