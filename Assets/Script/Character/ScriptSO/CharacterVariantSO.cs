using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Variants", menuName = "Scriptable Objects/Character Variants")]
public class CharacterVariantSO : ScriptableObject
{
    public string id;
    public GameObject home;
    public GameObject dungeon;
    //skins ?

    [ContextMenu("Generate ID")]
    void GenerateId()
    {
        id = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)).ToString();
    }
}
