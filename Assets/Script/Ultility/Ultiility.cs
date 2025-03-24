using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Object = System.Object;

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
    
    //BFS with given map and targets
    public static List<Vector2Int> BFS(Vector2Int startPost, HashSet<Vector2Int> map, HashSet<Vector2Int> nodes)
    {
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
                if(map.Contains(neighbour) && !visited.Contains(neighbour) && !frontier.Contains(neighbour))
                {
                    frontier.Enqueue(neighbour);
                }
            }
        }

        // Debug.Log("reached nodes order: "+String.Join(", ",reachedNodes));
        return reachedNodes;
    }

    //BFS entire given map
    public static List<Vector2Int> FloodFill(Vector2Int startPost, HashSet<Vector2Int> map)
    {
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
                if (map.Contains(neighbour) && !visited.Contains(neighbour))
                {
                    frontier.Enqueue(neighbour);
                    visited.Add(neighbour);
                }
            }
        }

        return visitedNodes;
    }

    //BFS entire given map in a radius
    public static List<Vector2Int> FloodFillRadius(Vector2Int startPost, IEnumerable<Vector2Int> traverseMap, int radius)
    {
        var map = new HashSet<Vector2Int>(traverseMap);
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
                if (map.Contains(neighbour) && !visited.Contains(neighbour) && Vector2Int.Distance(startPost, neighbour) <= radius)
                {
                    frontier.Enqueue(neighbour);
                    visited.Add(neighbour);
                }
            }
        }

        return visitedNodes;
    }

    public static Vector2 GetRandomPositionInCircle(Vector2 center, float radius)
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
        float randomRadius = Mathf.Sqrt(UnityEngine.Random.Range(0f, 1f)) * radius;
        
        float x = center.x + randomRadius * Mathf.Cos(angle);
        float y = center.y + randomRadius * Mathf.Sin(angle);
        
        return new Vector2(x, y);
    }

    public static string GenerateRandomSeed(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[UnityEngine.Random.Range(0, chars.Length)];
        }
        return new string(result);
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