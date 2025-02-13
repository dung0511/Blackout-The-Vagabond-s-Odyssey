using System;
using System.Collections.Generic;

public static class Utility
{
    private static Random rng = new Random();    
    
    public static void UnseededShuffle<T>(List<T> list)
    {
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
        return rng.Next(min, max);
    }
}