
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
        var defaultChar = GameManager.Instance.playerCharacter;
        var charId = defaultChar.id;
        try
        {
            charId = DataManager.gameData.playerData.characterId;
            CharacterVariantSO character = characterList.FirstOrDefault(c => c.id == charId); 
            if(character == null) throw new Exception("Character not found in list: "+charId);
            GameManager.Instance.playerCharacter = character;   //load prev played char
        } catch(Exception e)
        {
            GameManager.Instance.playerCharacter = defaultChar; //load default char
            Debug.LogError("Load char failed: "+e.Message);
        }
    }
}