using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Prop", menuName = "Scriptable Objects/Object/PropSO")]
public class Prop : ScriptableObject
{
    public List<GameObject> prefabsVariant;
    public Vector2Int propSize = Vector2Int.one;

    [Header("Quantity: ")]
    [Min(0)] public int minQuantity = 1;
    [Min(1)] public int maxQuantity = 1;

    [Header("Group:")]
    public bool placeAsGroup = false;
    [Min(1)] public int minGroupSize = 1;
    [Min(1)] public int maxGroupSize = 1;

    [Header("Positions")]
    public bool corner = false;
    public bool nearTopWall = false;
    public bool nearBottomWall = false;
    public bool nearLeftWall = false;
    public bool nearRightWall = false;
    public bool inner = true;    
}