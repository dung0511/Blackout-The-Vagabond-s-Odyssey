using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageOneDungeonGenerator : StageOne
{
    [SerializeField] private int corridorLength = 10;
    [SerializeField] private int corridorNums = 3;
    [SerializeField] [Range(0, 1)]private float roomChance = 0.8f, doorChance = 0.5f;
    // [SerializeField] private int eliteRoomNums = 1;

    protected override void RunProceduralGeneration()
    {
        rooms = new();
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
        PlaceDoor();
        
        // Debug.Log("Rooms: " + rooms.Count);
        // Debug.Log("Rooms: " + string.Join(", ", rooms.OfType<StageOneRoom>().Select(x => new {x.center, x.topDoor, x.bottomDoor,x.leftDoor,x.rightDoor})));
    }


    private void PlaceDoor()
    {
        foreach (var room in rooms)
        {
            if (room.hasTopDoor)
            {
                mapVisualizer.PlaceDoor(room.topEntrance, 0);
            }
            if (room.hasBottomDoor)
            {
                mapVisualizer.PlaceDoor(room.bottomEntrance+Vector2Int.down, 0);
            }
            if (room.hasLeftDoor)
            {
                mapVisualizer.PlaceDoor(room.leftEntrance, 1);
            }
            if (room.hasRightDoor)
            {
                mapVisualizer.PlaceDoor(room.rightEntrance, 2);
            }
        }
    }

    private void SetRoomType(HashSet<Vector2Int> deadEnds)
    {
        //set room with elite monster
        var eliteRoomNum = Mathf.CeilToInt(rooms.Count / 10f);
        var eliteRooms = rooms.Where(x => x.roomType != RoomType.Spawn)
                      .OrderBy(x => UnityEngine.Random.Range(0, int.MaxValue))
                      .Take(eliteRoomNum);
        foreach (var room in eliteRooms)
        {
            room.roomType = RoomType.Elite;
            Debug.Log("Elite room: " + room.center);
        }
        //Set exit room the furthest
        if(deadEnds.Count == 1){
            rooms.SingleOrDefault(x => x.center == deadEnds.First()).roomType = RoomType.Exit;
            return;
        } else {
            float maxDistance = 0f, secondMaxDistance = 0f;
            var exitPos = deadEnds.First();
            var treasurePos = deadEnds.First();
            foreach (var deadEnd in deadEnds)
            {
                var distance = Vector2Int.Distance(startPos, deadEnd);
                if (distance > maxDistance)
                {
                    secondMaxDistance = maxDistance;
                    treasurePos = exitPos;

                    maxDistance = distance;
                    exitPos = deadEnd;
                }
                else if (distance > secondMaxDistance)
                {
                    secondMaxDistance = distance;
                    treasurePos = deadEnd;
                }
            }
            rooms.SingleOrDefault(x => x.center == exitPos).roomType = RoomType.Exit;
            Debug.Log("Exit room: " + exitPos);
            //set treasure room 2nd furthest
            rooms.SingleOrDefault(x => x.center == treasurePos).roomType = RoomType.Treasure;
            Debug.Log("Treasure room: " + treasurePos);
        }  
    }

    private void FindRoomEntrances(HashSet<Vector2Int> floorPositions)
    {
        foreach (var room in rooms)
        {
            var center = room.center;
            var size = room.size;

            var mt = new Vector2Int(center.x, center.y+size[1]+1); //mid top
            var mb = new Vector2Int(center.x, center.y-size[1]-1); //mid bottom
            var ml = new Vector2Int(center.x-size[0]-1, center.y); //mid left
            var mr = new Vector2Int(center.x+size[0]+1, center.y); //mid right
            if(floorPositions.Contains(mt)) 
            {
                room.topEntrance = mt;
                room.hasTopDoor = UnityEngine.Random.Range(0,100) <= doorChance*100;
            }
            if(floorPositions.Contains(mb)) 
            {
                room.bottomEntrance = mb;
                room.hasBottomDoor = UnityEngine.Random.Range(0,100) <= doorChance*100;
            }
            if(floorPositions.Contains(ml)) 
            {
                room.leftEntrance = ml;
                room.hasLeftDoor = UnityEngine.Random.Range(0,100) <= doorChance*100;
            }
            if(floorPositions.Contains(mr)) 
            {
                room.rightEntrance = mr;
                room.hasRightDoor = UnityEngine.Random.Range(0,100) <= doorChance*100;
            }
        }

        //set full entrance for elite rooms
        foreach (var room in rooms.Where(x => x.roomType == RoomType.Elite))
        {
            room.hasTopDoor = room.topEntrance != Vector2Int.zero;
            room.hasBottomDoor = room.bottomEntrance != Vector2Int.zero;
            room.hasLeftDoor = room.leftEntrance != Vector2Int.zero;
            room.hasRightDoor = room.rightEntrance != Vector2Int.zero;
        }
    }

    private void CreateSpawnRoom(HashSet<Vector2Int> floorPositions)
    {
        var spawnRoom = RunBoxGen(startPos, smallRoom.minWidth, smallRoom.minHeight);
        rooms.Add( new StageOneRoom
        {
            center = startPos,
            size = new int[] {smallRoom.minWidth, smallRoom.minHeight},
            roomType = RoomType.Spawn
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
        Debug.Log("Deadend positions: " + string.Join(", ", deadEnds));
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
            rooms.Add( new StageOneRoom
            {
                center = roomCenter,
                roomType = RoomType.Normal //default
            });

        }
    }

    private HashSet<Vector2Int> CreateRooms()
    {
        var roomPositions = new HashSet<Vector2Int>();
        foreach (var r in rooms)
        {
            if(r.roomType == RoomType.Spawn) continue;
            else if(r.roomType == RoomType.Exit || r.roomType == RoomType.Treasure)
            {
                int width = UnityEngine.Random.Range(smallRoom.minWidth, smallRoom.maxWidth);
                int height = UnityEngine.Random.Range(smallRoom.minHeight, smallRoom.maxHeight);
                var roomBound = RunBoxGen(r.center, width, height);
                r.size = new int[] {width, height};
                roomPositions.UnionWith(roomBound);
            }
            else
            {
                int width = UnityEngine.Random.Range(boxParams.minWidth, boxParams.maxWidth);
                int height = UnityEngine.Random.Range(boxParams.minHeight, boxParams.maxHeight);
                var roomBound = RunBoxGen(r.center, width, height);
                r.size = new int[] {width, height};
                roomPositions.UnionWith(roomBound);
            }
            
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPos = startPos;
        potentialRoomPositions.Add(currentPos);
        int safe = 0;
        for (int i = 0; i < corridorNums; i++)
        {
            var direction = Direction2D.GetRandomDirection();
            var corridorPath = ProceduralGeneration.DirectedCorridor(currentPos, corridorLength, direction);
            bool hasOverlap = corridorPath.Any(pos => floorPositions.Contains(pos+direction*3));  
            if (!hasOverlap)
            {
                currentPos = corridorPath[corridorPath.Count - 1];
                potentialRoomPositions.Add(currentPos);
                floorPositions.UnionWith(corridorPath); // add corridor path to floorPositions
            }
            else
            {
                currentPos = potentialRoomPositions.ElementAt(UnityEngine.Random.Range(0, potentialRoomPositions.Count));
                i--;
            }
            safe++;
            if (safe > 1000)
            {
                Debug.LogError("Infinite loop");
                break;
            }
        }

        
    }
}
