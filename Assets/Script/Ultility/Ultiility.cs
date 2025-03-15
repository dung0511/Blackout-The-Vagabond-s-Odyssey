using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Object = System.Object;
using System.Linq;

public static class Utility
{    
    public static void UnseededShuffle<T>(List<T> list)
    {
        Random rng = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]); // Swap elements
        }
    }

    public static void SeededShuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]); // Swap elements
        }
    }

    public static int UnseededRng(int min, int max)
    {
        Random rng = new Random();
        return rng.Next(min, max);
    }
    public static bool Chance(int chance) => UnseededRng(0,100) < chance;

    public static List<Vector2Int> BFS(Vector2Int startPost, IEnumerable<Vector2Int> map, HashSet<Vector2Int> nodes)
    {
        var graph = new HashSet<Vector2Int>(map);
        List<Vector2Int> reachedNodes = new();
        Queue<Vector2Int> frontier = new();
        HashSet<Vector2Int> visited = new();
        
        frontier.Enqueue(startPost);

        while(frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();
            visited.Add(current);
            if(nodes.Contains(current))
            {
                reachedNodes.Add(current);
            }
            foreach (var direction in Direction2D.Directions)
            {
                Vector2Int neighbour = current + direction;
                if(graph.Contains(neighbour) && !visited.Contains(neighbour) && !frontier.Contains(neighbour))
                {
                    frontier.Enqueue(neighbour);
                }
            }
        }

        // Debug.Log("reached nodes order: "+String.Join(", ",reachedNodes));
        return reachedNodes;
    }

    public static List<Vector2Int> TraverseBFS(Vector2Int startPost, IEnumerable<Vector2Int> map)
    {
        var graph = new HashSet<Vector2Int>(map);
        List<Vector2Int> visitedNodes = new();
        Queue<Vector2Int> frontier = new();
        HashSet<Vector2Int> visited = new();
        
        frontier.Enqueue(startPost);
        visited.Add(startPost);

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();
            visitedNodes.Add(current);
            
            foreach (var direction in Direction2D.Directions)
            {
                Vector2Int neighbour = current + direction;
                if (graph.Contains(neighbour) && !visited.Contains(neighbour))
                {
                    frontier.Enqueue(neighbour);
                    visited.Add(neighbour);
                }
            }
        }

        return visitedNodes;
    }

    public static List<Vector2Int> TraverseRadiusBFS(Vector2Int startPost, IEnumerable<Vector2Int> map, int radius)
    {
        var graph = new HashSet<Vector2Int>(map);
        List<Vector2Int> visitedNodes = new();
        Queue<Vector2Int> frontier = new();
        HashSet<Vector2Int> visited = new();
        
        frontier.Enqueue(startPost);
        visited.Add(startPost);

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();
            if(Vector2Int.Distance(current,startPost) > radius) continue;
            visitedNodes.Add(current);
            
            foreach (var direction in Direction2D.Directions)
            {
                Vector2Int neighbour = current + direction;
                if (graph.Contains(neighbour) && !visited.Contains(neighbour))
                {
                    frontier.Enqueue(neighbour);
                    visited.Add(neighbour);
                }
            }
        }

        return visitedNodes;
    }
    

    /// <summary>
    /// Empty string debug check
    /// </summary>
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// null value debug check
    /// </summary>
    public static bool ValidateCheckNullValue(Object thisObject, string fieldName, UnityEngine.Object objectToCheck)
    {
        if (objectToCheck == null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// positive value debug check - if zero is allowed set isZeroAllowed to true. Returns true if there is an error
    /// </summary>
    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, float valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                error = true;
            }
        }

        return error;
    }
    /// <summary>
    /// positive range debug check - set isZeroAllowed to true if the min and max range values can both be zero. Returns true if there is an error
    /// </summary>
    public static bool ValidateCheckPositiveRange(Object thisObject, string fieldNameMinimum, float valueToCheckMinimum, string fieldNameMaximum, float valueToCheckMaximum, bool isZeroAllowed)
    {
        bool error = false;
        if (valueToCheckMinimum > valueToCheckMaximum)
        {
            Console.WriteLine("Error");
            error = true;
        }

        if (ValidateCheckPositiveValue(thisObject, fieldNameMinimum, valueToCheckMinimum, isZeroAllowed)) error = true;

        if (ValidateCheckPositiveValue(thisObject, fieldNameMaximum, valueToCheckMaximum, isZeroAllowed)) error = true;

        return error;
    }
}