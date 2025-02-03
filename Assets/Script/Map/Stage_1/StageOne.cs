using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class StageOne : AbstractDungeonGenerator
{
    [SerializeField] protected BoxSO boxParams, smallRoom;
    protected HashSet<StageOneRoom> rooms = new();

    protected override void RunProceduralGeneration()
    {
        AddRoomObjects();
    }

    private void AddRoomObjects()
    {
        foreach (var room in rooms)
        {
            switch (room.roomType)
            {
                case RoomType.Normal:

                    break;
                case RoomType.Elite:

                    break;
                case RoomType.Treasure:

                    break;
                // case RoomType.Upgrade:

                //     break;
                // case RoomType.Shop:
                
                //     break;
                case RoomType.Exit:

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
}
