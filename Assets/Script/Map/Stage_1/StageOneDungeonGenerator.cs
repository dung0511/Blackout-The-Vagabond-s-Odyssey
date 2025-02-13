using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StageOneDungeonGenerator : StageOne
{
    [SerializeField] protected Box mediumRoom, smallRoom;
    [SerializeField] private int corridorLength = 10;
    [SerializeField] private int corridorNums = 3;
    [SerializeField] [Range(0, 1)]private float roomChance = 0.8f, doorChance = 0.5f;
    // [SerializeField] private int eliteRoomNums = 1;

    protected override void RunProceduralGeneration()
    {
        DungeonData.Reset();
        CorridorFirstGeneration();
        base.RunProceduralGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        
        CreateCorridors(floorPositions, potentialRoomPositions);
        CreateSpawnRoom(floorPositions);
        HashSet<Vector2Int> deadEnds = GetDeadEnds(floorPositions);
        SetRandomRoom(potentialRoomPositions, roomChance, deadEnds);
        SetRoomType(deadEnds);
        var roomPositions = CreateRooms();
        FindRoomEntrances(floorPositions);
        
        floorPositions.UnionWith(roomPositions);
        mapVisualizer.PlaceTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, mapVisualizer);
        SetRoomTileData();
        // Debug.Log("DungeonData.Rooms: " + DungeonData.rooms.Count);
        // Debug.Log("DungeonData.Rooms: " + string.Join(", ", DungeonData.rooms.OfType<StageOneRoom>().Select(x => new {x.center, x.topDoor, x.bottomDoor,x.leftDoor,x.rightDoor})));
    }

    private void SetRoomTileData()
    {
        foreach (var room in DungeonData.rooms)
        {
            int left = room.center.x - room.size[0];
            int right = room.center.x + room.size[0];
            int top = room.center.y + room.size[1];
            int bottom = room.center.y - room.size[1];

            room.corners = new HashSet<Vector2Int>
            {
                new Vector2Int(left, top),    // top left
                new Vector2Int(right, top),   // top right
                new Vector2Int(left, bottom), // bottom left
                new Vector2Int(right, bottom) // bottom right
            };

            for (int x = left + 1; x < right; x++)
            {
                room.nearTopWall.Add(new Vector2Int(x, top));
                room.nearBottomWall.Add(new Vector2Int(x, bottom));
                for (int y = bottom + 1; y < top; y++)
                {
                    room.inners.Add(new Vector2Int(x, y));
                }
            }

            for (int y = bottom + 1; y < top; y++)
            {
                room.nearLeftWall.Add(new Vector2Int(left, y));
                room.nearRightWall.Add(new Vector2Int(right, y));
            }
        }


        // Debug.Log("DungeonData.Rooms: " + string.Join(", ", DungeonData.rooms.Take(3).Select(x => 
        //     $"center: {x.center}, corners: [{string.Join(", ", x.corners)}], nearTopWall: {string.Join(", ", x.nearTopWall)}, nearBottomWall: {string.Join(", ", x.nearBottomWall)}, nearLeftWall: {string.Join(", ", x.nearLeftWall)}, nearRightWall: {string.Join(", ", x.nearRightWall)}"
        // )));    
    }

    private void SetRoomType(HashSet<Vector2Int> deadEnds)
    {
        var tmp = DungeonData.rooms.ToList();
        //Set exit (furthest), treasure room (2nd furthest)
        if(deadEnds.Count == 1){
            DungeonData.rooms.FirstOrDefault(x => x.center == deadEnds.First()).roomType = RoomType.Exit;
            return;
        } else {
             var exitPos = FindFurthestRoomByWorld(deadEnds); // Furthest room
            deadEnds.Remove(exitPos);
            var treasurePos = FindFurthestRoomByWorld(deadEnds); // 2nd furthest room
            deadEnds.Remove(treasurePos);

            var exitRoom = DungeonData.rooms.FirstOrDefault(x => x.center == exitPos);
            var treasureRoom = DungeonData.rooms.FirstOrDefault(x => x.center == treasurePos);

            exitRoom.roomType = RoomType.Exit;
            treasureRoom.roomType = RoomType.Treasure;

            Debug.Log("Exit room: " + exitPos);
            Debug.Log("Treasure room: " + treasurePos);

            tmp.Remove(exitRoom);
            tmp.Remove(treasureRoom); //remove exit and treasure room from tmp, prepare tmp for elite room
        }  

        //set elite room (random)
        var eliteRoomNum = Mathf.CeilToInt(DungeonData.rooms.Count / 10f);
        
        Utility.SeededShuffle(tmp);
        for(int i=0; i<eliteRoomNum; i++)
        {
            var index = UnityEngine.Random.Range(0, tmp.Count);
            var r = DungeonData.rooms.ElementAt(index);
            r.roomType = RoomType.Elite;
            Debug.Log("Elite: "+r.center);
            tmp.RemoveAt(index); //remove elite room from tmp, prepare tmp for shop room
        }

        //set shop room (random)
        var shopIndex = UnityEngine.Random.Range(0, tmp.Count);
        var sr = DungeonData.rooms.ElementAt(shopIndex);
        sr.roomType = RoomType.Shop;
        Debug.Log("Shop: "+sr.center);
    }

    private Vector2Int FindFurthestRoomByWorld(HashSet<Vector2Int> deadEnds)
    {
        float maxDistance = 0f;
        var furthest = deadEnds.First();
        foreach (var deadEnd in deadEnds)
        {
            var distance = Vector2Int.Distance(startPos, deadEnd);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthest = deadEnd;
            }
        }
        return furthest;
    }

    private void FindRoomEntrances(HashSet<Vector2Int> floorPositions)
    {
        foreach (var room in DungeonData.rooms)
        {
            var center = room.center;
            var size = room.size;

            var entrances = new Dictionary<string, Vector2Int>
            {
                { "top", new Vector2Int(center.x, center.y + size[1] + 1) },    //mid top
                { "bottom", new Vector2Int(center.x, center.y - size[1] - 1) }, //mid bottom
                { "left", new Vector2Int(center.x - size[0] - 1, center.y) },   //mid left
                { "right", new Vector2Int(center.x + size[0] + 1, center.y) }   //mid right
            };
            foreach (var entrance in entrances)
            {
                if (floorPositions.Contains(entrance.Value))
                {
                    var beforeEntrance = entrance.Value;
                    switch (entrance.Key)
                    {
                        case "top":
                            room.topEntrance = entrance.Value;
                            beforeEntrance = entrance.Value+Vector2Int.down;
                            DungeonData.path.Add(beforeEntrance);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.left);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.right);
                            if (room.roomType != RoomType.Elite && UnityEngine.Random.value <= doorChance)
                            {
                                mapVisualizer.PlaceDoor(room.topEntrance, 0);
                            }
                            break;
                        case "bottom":
                            room.bottomEntrance = entrance.Value;
                            beforeEntrance = entrance.Value+Vector2Int.up;
                            DungeonData.path.Add(beforeEntrance);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.left);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.right);

                            if (room.roomType != RoomType.Elite && UnityEngine.Random.value <= doorChance)
                            {
                                mapVisualizer.PlaceDoor(room.bottomEntrance + Vector2Int.down, 0);
                            }
                            break;
                        case "left":
                            room.leftEntrance = entrance.Value;
                            beforeEntrance = entrance.Value+Vector2Int.right;
                            DungeonData.path.Add(beforeEntrance);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.up);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.down);
                            if (room.roomType != RoomType.Elite && UnityEngine.Random.value <= doorChance)
                            {
                                mapVisualizer.PlaceDoor(room.leftEntrance, 1);
                            }
                            break;
                        case "right":
                            room.rightEntrance = entrance.Value;
                            beforeEntrance = entrance.Value+Vector2Int.left;
                            DungeonData.path.Add(beforeEntrance);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.up);
                            DungeonData.path.Add(beforeEntrance+Vector2Int.down);
                            if (room.roomType != RoomType.Elite && UnityEngine.Random.value <= doorChance)
                            {
                                mapVisualizer.PlaceDoor(room.rightEntrance, 1);
                            }
                            break;
                    }
                }
            }
        }

        //set full entrance for elite DungeonData.rooms
        foreach (var room in DungeonData.rooms.Where(x => x.roomType == RoomType.Elite))
        {
            if(room.topEntrance != Vector2Int.zero) mapVisualizer.PlaceDoor(room.topEntrance, 0);
            if(room.bottomEntrance != Vector2Int.zero) mapVisualizer.PlaceDoor(room.bottomEntrance+Vector2Int.down, 0);
            if(room.leftEntrance != Vector2Int.zero) mapVisualizer.PlaceDoor(room.leftEntrance, 1);
            if(room.rightEntrance != Vector2Int.zero) mapVisualizer.PlaceDoor(room.rightEntrance, 1);
        }
    }

    private void CreateSpawnRoom(HashSet<Vector2Int> floorPositions)
    {
        var spawnRoom = RunBoxGen(startPos, smallRoom.minWidth, smallRoom.minHeight);
        DungeonData.rooms.Add( new RectangleRoom
        {
            center = startPos,
            size = new int[] {smallRoom.minWidth, smallRoom.minHeight},
            roomType = RoomType.Spawn,
            roomTiles = spawnRoom
        });
        //Placing shits here
        
        //
        floorPositions.UnionWith(spawnRoom);
    }
    
    private HashSet<Vector2Int> GetDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> deadEnds = new();
        HashSet<Vector2Int> deadEndsTmp = new();
        foreach (var pos in floorPositions)
        {
            int neighborCount = 0;
            foreach (var direction in Direction2D.Directions)
            {
                if (floorPositions.Contains(pos + direction*2))
                {
                    neighborCount++;
                }
            }
            if (neighborCount == 1)
            {
                deadEndsTmp.Add(pos);
            }
        }
        foreach(var deadEnd in deadEndsTmp)
        {
            foreach (var direction in Direction2D.Directions)
            {
                if(!floorPositions.Contains(deadEnd + direction))
                {
                    deadEnds.Add(deadEnd);
                    break;
                }
            }
        }
        // Debug.Log("DeadEnds: " + deadEnds.Count);
        // Debug.Log("Deadend positions: " + string.Join(", ", deadEnds));
        return deadEnds;
    }

    private void SetRandomRoom(HashSet<Vector2Int> potentialRoomPositions, float roomChance, HashSet<Vector2Int> deadEnds)
    {
        potentialRoomPositions.Remove(startPos); //exclude spawn room
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomNums = Mathf.RoundToInt(potentialRoomPositions.Count * roomChance);
        var roomToCreate = potentialRoomPositions.OrderBy(x => UnityEngine.Random.Range(0, int.MaxValue)).Take(roomNums).ToHashSet();
        roomToCreate.UnionWith(deadEnds);  //ensure no deadend corridor
        foreach (var roomCenter in roomToCreate)
        {
            DungeonData.rooms.Add( new RectangleRoom
            {
                center = roomCenter,
                roomType = RoomType.Normal //default
            });
        }
    }

    private HashSet<Vector2Int> CreateRooms()
    {
        var roomPositions = new HashSet<Vector2Int>();
        var roomsToProcess = DungeonData.rooms.Where(r => r.roomType != RoomType.Spawn); //tmp to exclude spawn room, reference to real rooms list

        foreach (var r in roomsToProcess)
        {
            int width, height;
            HashSet<Vector2Int> roomBound;

            (width, height) = GetRoomDimensions(r.roomType);
            roomBound = RunBoxGen(r.center, width, height);
            r.size = new int[] { width, height };
            r.roomTiles = roomBound;
            roomPositions.UnionWith(roomBound);
        }

        DungeonData.corridorPath.ExceptWith(roomPositions);
        return roomPositions;
    }

    private (int width, int height) GetRoomDimensions(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.Elite: //use Normal room case
            case RoomType.Normal:
            return (
                    UnityEngine.Random.Range(mediumRoom.minWidth, mediumRoom.maxWidth),
                    UnityEngine.Random.Range(mediumRoom.minHeight, mediumRoom.maxHeight)
                );
            case RoomType.Treasure: //use Exit room case
            case RoomType.Exit:
                return (
                    UnityEngine.Random.Range(smallRoom.minWidth, smallRoom.maxWidth),
                    UnityEngine.Random.Range(smallRoom.minHeight, smallRoom.maxHeight)
                );
            case RoomType.Shop:
                return (
                    UnityEngine.Random.Range(smallRoom.maxWidth, smallRoom.maxWidth),
                    UnityEngine.Random.Range(smallRoom.minHeight, smallRoom.minHeight)
                );
            default:
                Debug.LogError("Invalid room type: " + roomType);
                return (0, 0);
        }
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {

        var currentPos = startPos;
        potentialRoomPositions.Add(currentPos);
        for (int i = 0; i < corridorNums; i++)
        {
            var direction = Direction2D.GetRandomDirection();
            var (corridorPath, minPath) = ProceduralGeneration.DirectedCorridor(currentPos, corridorLength, direction);
            bool hasOverlap = corridorPath.Any(pos => floorPositions.Contains(pos+direction*3));  
            if (!hasOverlap)
            {
                currentPos = corridorPath[corridorPath.Count - 1];
                potentialRoomPositions.Add(currentPos);
                DungeonData.path.UnionWith(minPath);
                DungeonData.corridorPath.Add(currentPos);
                floorPositions.UnionWith(corridorPath); // add corridor path to floorPositions
            }
            else
            {
                currentPos = potentialRoomPositions.ElementAt(UnityEngine.Random.Range(0, potentialRoomPositions.Count));
                i--;
            }
        }

    }
}
