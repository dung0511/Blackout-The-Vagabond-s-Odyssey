using UnityEngine;

[RequireComponent(typeof(HomeMovement))]
public class CharacterBinder : MonoBehaviour
{
    private GameObject selectedCharacter;
    private GameObject interactComponent;
    private Vector2 originalPosition;
    private Vector3 originalScale;
    private int animationHash;
    public static CharacterBinder Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        animationHash = Animator.StringToHash("isMoving");
    }

    private void Start()
    {
        selectedCharacter = GameObject.Find(GameManager.Instance.playerCharacter.home.name);
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