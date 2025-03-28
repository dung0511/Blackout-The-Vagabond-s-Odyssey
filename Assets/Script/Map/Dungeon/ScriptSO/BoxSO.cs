using UnityEngine;

[CreateAssetMenu(fileName = "Box", menuName = "Scriptable Objects/Map/BoxSO")]
public class Box : ScriptableObject
{
    public int minWidth = 10; public int maxWidth = 20;
    public int minHeight = 10; public int maxHeight = 20;
}
