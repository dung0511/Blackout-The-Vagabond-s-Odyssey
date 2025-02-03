using UnityEngine;

[CreateAssetMenu(fileName = "BoxSO", menuName = "Scriptable Objects/Map/BoxSO")]
public class BoxSO : ScriptableObject
{
    public int minWidth = 10; public int maxWidth = 20;
    public int minHeight = 10; public int maxHeight = 20;
}
