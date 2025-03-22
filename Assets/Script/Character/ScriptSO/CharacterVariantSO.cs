using UnityEngine;

[CreateAssetMenu(fileName = "Character Variants", menuName = "Scriptable Objects/Character Variants")]
public class CharacterVariantSO : ScriptableObject
{
    //name, id
    public GameObject home;
    public GameObject dungeon;
}
