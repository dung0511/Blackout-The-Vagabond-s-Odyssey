using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Dictionary<int,CharacterVariantSO> characters;

    public static CharacterManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


}