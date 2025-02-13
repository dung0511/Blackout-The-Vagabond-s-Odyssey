using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, MapVisualizer mapVisualizer)
    {
        var wallPositions = FindWallsDirection(floorPositions, Direction2D.AllDirections);
        foreach (var pos in wallPositions)
        {
            mapVisualizer.PlaceWall(pos);
        }
    }

    private static HashSet<Vector2Int> FindWallsDirection(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            foreach (var direction in directions)
            {
                if (!floorPositions.Contains(pos + direction))
                {
                    if(direction.Equals(Vector2Int.right) || direction.Equals(Vector2Int.down))
                    {
                        wallPositions.Add(pos + direction*2);
                    } else {
                        wallPositions.Add(pos + direction);
                    }
                }
            }
        }

        return wallPositions;
    }
}
