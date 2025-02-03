// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class Stage1Generator : StageOne
// {
//     [SerializeField] private int corridorLength = 10;
//     [SerializeField] private int corridorNums = 3;
//     [SerializeField] [Range(0, 1)]private float roomChance = 0.8f;

//     protected override void RunProceduralGeneration()
//     {
//         CorridorFirstGeneration();
//     }

//     private void CorridorFirstGeneration()
//     {
//         HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
//         HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

//         CreateCorridors(floorPositions, potentialRoomPositions);
//         CreateSpawnRoom(floorPositions);
//         HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions, roomChance);
//         List<Vector2Int> deadEnds = GetDeadEnds(floorPositions);
//         CreateDeadEndRoom(deadEnds, roomPositions);

//         floorPositions.UnionWith(roomPositions);

//         mapVisualizer.PlaceTiles(floorPositions);
//         WallGenerator.CreateWalls(floorPositions, mapVisualizer);
//     }

//     private void CreateSpawnRoom(HashSet<Vector2Int> floorPositions)
//     {
//         var spawnRoom = RunBoxGen(SpawnRoom, startPos);
//         //Placing shits here
        
//         //
//         floorPositions.UnionWith(spawnRoom);
//     }

//     private void CreateDeadEndRoom(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
//     { //ensure no deadend corridor
//         foreach (var deadEnd in deadEnds)
//         {
//             if (!roomPositions.Contains(deadEnd))
//             {
//                 var room = RunBoxGen(boxParams, deadEnd);
//                 roomPositions.UnionWith(room);
//             }
//         }
//     }

//     private List<Vector2Int> GetDeadEnds(HashSet<Vector2Int> floorPositions)
//     {
//         List<Vector2Int> deadEnds = new List<Vector2Int>();
//         List<Vector2Int> deadEndsTmp = new List<Vector2Int>();
//         foreach (var pos in floorPositions)
//         {
//             int neighborCount = 0;
//             foreach (var direction in Direction2D.Directions)
//             {
//                 if (floorPositions.Contains(pos + direction*2))
//                 {
//                     neighborCount++;
//                 }
//             }
//             if (neighborCount == 1)
//             {
//                 deadEndsTmp.Add(pos);
//             }
//         }
//         foreach(var deadEnd in deadEndsTmp)
//         {
//             foreach (var direction in Direction2D.Directions)
//             {
//                 if(!floorPositions.Contains(deadEnd + direction))
//                 {
//                     deadEnds.Add(deadEnd);
//                     break;
//                 }
//             }
//         }
//         Debug.Log("DeadEnds: " + deadEnds.Count);
//         Debug.Log("Deadend positions: " + string.Join(", ", deadEnds));
//         return deadEnds;
//     }

//     private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions, float roomChance)
//     {
//         HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
//         int roomNums = Mathf.RoundToInt(potentialRoomPositions.Count * roomChance);
//         List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => UnityEngine.Random.Range(0, int.MaxValue)).Take(roomNums).ToList();
//         // foreach (var roomPos in roomToCreate)
//         // {
//         //     var room = RunRandomWalk(randomWalkParams, roomPos);
//         //     roomPositions.UnionWith(room);
//         // }
//         foreach (var roomPos in roomToCreate)
//         {
//             var room = RunBoxGen(boxParams, roomPos);
//             roomPositions.UnionWith(room);
//         }
    
//         return roomPositions;
//     }

//     private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
//     {
//         var currentPos = startPos;
//         int safe = 0;
//         for (int i = 0; i < corridorNums; i++)
//         {
//             var direction = Direction2D.GetRandomDirection();
//             var corridorPath = ProceduralGeneration.DirectedCorridor(currentPos, corridorLength, direction);
//             bool hasOverlap = corridorPath.Any(pos => floorPositions.Contains(pos+direction*3));  
//             if (!hasOverlap)
//             {
//                 currentPos = corridorPath[corridorPath.Count - 1];
//                 potentialRoomPositions.Add(currentPos);
//                 floorPositions.UnionWith(corridorPath); // add corridor path to floorPositions
//             }
//             else
//             {
//                 currentPos = potentialRoomPositions.ElementAt(UnityEngine.Random.Range(0, potentialRoomPositions.Count));
//                 i--;
//             }
//             safe++;
//             if (safe > 100)
//             {
//                 Debug.LogError("Infinite loop");
//                 break;
//             }
//         }
//     }
// }
