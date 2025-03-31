using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonController : AbstractDungeonGenerator
{
    public PropPlacer propPlacer;
    public AgentPlacer agentPlacer;
    public EliteRoomSetup eliteRoomSetup;
 
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
                    agentPlacer.PlaceAgents(room);
                    break;

                case RoomType.Elite:
                    propPlacer.PlaceNearWallProps(room);
                    propPlacer.PlaceInnerProps(room);
                    propPlacer.PlaceTraps(room);
                    agentPlacer.PlaceAgents(room);

                    eliteRoomSetup.SetupEliteRoom(room); //elite room setup
                    break;

                case RoomType.Treasure:
                    propPlacer.PlaceTopWallProps(room);
                    propPlacer.PlaceTreasure(room);
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

    protected void Reset()
    {
        DungeonData.Reset();
        propPlacer.Reset();
        agentPlacer.Reset();
        eliteRoomSetup.Reset();
        MinimapData.Reset();
    }
}
