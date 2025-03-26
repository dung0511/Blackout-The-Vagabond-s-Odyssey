
using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterVariantSO> characterList;

    void Awake()
    {
        var charId = GameManager.Instance.playerCharacter.id;
        CharacterVariantSO character;
        try
        {
            charId = DataManager.playerData.characterId;
        } catch(Exception e)
        {
            Debug.LogError("Use default char, Load char id failed "+e.Message);
        }
        character = characterList.FirstOrDefault(c => c.id == charId); 
        if(character)
        {
            GameManager.Instance.playerCharacter = character;   //load prev played char
        }
            
    }
}