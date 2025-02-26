using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class StageOne : AbstractDungeonGenerator
{
    public PropPlacer propPlacer;
    public AgentPlacer agentPlacer;
 
    protected override void RunProceduralGeneration()
    {
        PopulateRooms();
    }

    private void PopulateRooms()
    {
        foreach (var room in DungeonData.rooms)
        {
            propPlacer.SetRoomEntrances(room);
            propPlacer.PlaceCornerProps(room);
            propPlacer.PlaceLights(room);
            switch (room.roomType)
            {
                case RoomType.Spawn:
                    propPlacer.PlaceSpawnRoomProps(room);
                    break;
                case RoomType.Normal:
                    propPlacer.PlaceNearWallProps(room);
                    propPlacer.PlaceInnerProps(room);
                    propPlacer.PlaceTraps(room);
                    agentPlacer.PlaceEnemies(room);
                    break;
                case RoomType.Elite:
                    propPlacer.PlaceNearWallProps(room);
                    propPlacer.PlaceInnerProps(room);
                    propPlacer.PlaceTraps(room);
                    agentPlacer.PlaceEnemies(room);
                    //trap door 
                    break;
                case RoomType.Treasure:
                    propPlacer.PlaceTopWallProps(room);
                    // propPlacer.PlaceTreasure(room);
                    break;
                case RoomType.Shop:
                    propPlacer.PlaceLeftWallProps(room);
                    propPlacer.PlaceRightWallProps(room);
                    propPlacer.PlaceShopRoomProps(room);
                    break;
                case RoomType.Exit:
                    propPlacer.PlaceExitPortal(room);
                    break;
                default:
                    Debug.LogError("Invalid room type: " + room.roomType);
                    break;
            }
        }
    }



    protected HashSet<Vector2Int> RunBoxGen(Vector2Int startPos, int width, int height)
    {
        var currentPos = startPos;
        HashSet<Vector2Int> path = new HashSet<Vector2Int> {};
        var box = ProceduralGeneration.BoxGenerator(currentPos, width, height);
        path.UnionWith(box);
        return path;
    }

    protected void Reset()
    {
        DungeonData.Reset();
        propPlacer.Reset();
        agentPlacer.Reset();
    }
}
