using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Variants", menuName = "Scriptable Objects/Character Variants")]
public class CharacterVariantSO : ScriptableObject
{
    public string id;
    public GameObject home;
    public GameObject dungeon;

    #region Header 
    [Space(10)]
    [Header("CHARACTER INFO")]
    #endregion

    public Sprite characterImage;
    public string characterName;
    public float maxHealth;
    public float maxArmor;
    public float moveSpeed;
    public Sprite characterSkillIcon1;
    public string characterSkillDescription1;
    public Sprite characterSkillIcon2;
    public string characterSkillDescription2;


    //skins ?
#if UNITY_EDITOR
    [ContextMenu("Generate ID")]
    void GenerateId()
    {
        id = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)).ToString();
    }
#endif
    
}
