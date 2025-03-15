using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, MapVisualizer mapVisualizer, int bottomHeight)
    {
        var wallPositions = GetAroundFloor(floorPositions, Direction2D.AllDirections, bottomHeight); //get tiles around floor, offset bottom tiles by bottomHeight
        foreach (var pos in wallPositions)
        {
            mapVisualizer.PlaceWall(pos);
        }
    }

    private static HashSet<Vector2Int> GetAroundFloor(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions, int bottomHeight)
    {
        HashSet<Vector2Int> offsetBottomFloor = new HashSet<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            for(int i = 0; i < bottomHeight; i++)
            {
                if (floorPositions.Contains(pos + Vector2Int.up*(i+1))) //move entire floor up 
                {
                    offsetBottomFloor.Add(pos + Vector2Int.up*(i+1));
                }
            }

        }
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var pos in offsetBottomFloor) //get tiles around offseted floor
        {
            foreach (var direction in directions)
            {
                if (!offsetBottomFloor.Contains(pos + direction))
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
