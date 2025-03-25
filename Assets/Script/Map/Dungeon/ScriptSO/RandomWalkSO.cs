using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkParams", menuName = "Scriptable Objects/Map/RandomWalkSO")]
public class RandomWalkSO : ScriptableObject
{
    public int iterations = 10;
    public int numSteps = 10;
    public bool startRandomEachIteration = true;
}
