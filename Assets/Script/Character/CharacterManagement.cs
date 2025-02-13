using UnityEngine;

public class CharacterManagement : MonoBehaviour
{
    int characterTotal = 1;
    public int currentCharacterIndex;
    public GameObject[] characters;
    public GameObject characterContainer;
    public GameObject currentCharacter;
    public SpriteRenderer currentCharacterSR;
    void Start()
    {
        characterTotal = characterContainer.transform.childCount;
        characters = new GameObject[characterTotal];
        for (int i = 0; i < characterTotal; i++)
        {
            characters[i] = characterContainer.transform.GetChild(i).gameObject;
            characters[i].SetActive(false);
        }
        //if (GameManager.currentWeaponIndex > 0)
        //{
        //    weapons[GameManager.currentWeaponIndex].SetActive(true);
        //    currentWeapon = weapons[GameManager.currentWeaponIndex];
        //}
        //else
        //{
        characters[0].SetActive(true);
        currentCharacter = characters[0];
        currentCharacterSR = currentCharacter.GetComponent<SpriteRenderer>();
        //}
    }
}
