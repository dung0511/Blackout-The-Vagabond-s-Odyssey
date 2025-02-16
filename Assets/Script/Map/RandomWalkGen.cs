// using System;
// using System.Collections.Generic;
// using System.Linq;
// using NUnit.Framework;
// using UnityEngine;
// using Random = UnityEngine.Random;

// public class RandomWalk : AbstractDungeonGenerator
// {
//     [SerializeField] protected RandomWalkSO randomWalkParams;

//     protected override void RunProceduralGeneration()
//     {
//         HashSet<Vector2Int> path = RunRandomWalk(randomWalkParams, startPos);
//         mapVisualizer.ClearMap();
//         mapVisualizer.PlaceTiles(path);
//     }

//     protected HashSet<Vector2Int> RunRandomWalk(RandomWalkSO randomWalkParams, Vector2Int startPos)
//     {
//         var currentPos = startPos;
//         HashSet<Vector2Int> path = new HashSet<Vector2Int> {};
//         for(int i = 0; i < randomWalkParams.iterations; i++)
//         {
//             var iterPath = ProceduralGeneration.RandomWalk(currentPos, randomWalkParams.numSteps, 10, 10);
//             path.UnionWith(iterPath);
//             if (randomWalkParams.startRandomEachIteration)
//             {
//                 currentPos = path.ElementAt(Random.Range(0, path.Count));
//             }
//         }
//         return path;
//     }

// }
