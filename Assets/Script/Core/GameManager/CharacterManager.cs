
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterVariantSO> characterList;

    void Awake()
    {
        var charId = DataManager.playerData.characterId;
        var character = characterList.FirstOrDefault(c => c.id == charId); 
        if(character)
        {
            GameManager.Instance.playerCharacter = character;   //load prev played char
        }      
    }
}