using UnityEngine;

[RequireComponent(typeof(HomeMovement))]
public class CharacterBinder : MonoBehaviour
{
    private GameObject selectedCharacter;
    private GameObject interactComponent;
    private Vector2 originalPosition;
    private Vector3 originalScale;
    private int animationHash;
    
    private void Awake()
    {
        animationHash = Animator.StringToHash("isMoving");
    }

    private void Start()
    {
        selectedCharacter = GameObject.Find(GameManager.Instance.playerCharacter.home.name);
        GameManager.Instance.SetPlayerCharacter(GameManager.Instance.playerCharacter);
        BindCharacter(selectedCharacter);
    }

    public void BindCharacter(GameObject newCharacter)
    {
        originalPosition = newCharacter.transform.position;
        originalScale = newCharacter.transform.localScale;

        selectedCharacter = newCharacter;
        transform.SetParent(newCharacter.transform); transform.localPosition = Vector3.zero;
        GetComponent<HomeMovement>().BindObj(newCharacter);
        interactComponent = selectedCharacter.GetComponentInChildren<PlayableCharacter>().gameObject;
        interactComponent.SetActive(false);
    }

    public void ResetBindedCharacter()
    {
        selectedCharacter.transform.position = originalPosition;
        selectedCharacter.transform.localScale = originalScale;
        selectedCharacter.GetComponent<Animator>().SetBool(animationHash, false);
        selectedCharacter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        interactComponent.SetActive(true);
    }
}