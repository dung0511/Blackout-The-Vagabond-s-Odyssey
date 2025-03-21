using Unity.VisualScripting;
using UnityEngine;
 
 public class HomePlayerController : MonoBehaviour
 {
    public GameObject selectedCharacter;
    private Vector2 originalPosition;

    [SerializeField] private float speed = 5f;
    private bool isFacingRight; 
    private Rigidbody2D rd;
    private Animator animator;
    private int moveAnimation;

    public static HomePlayerController Instance {get; private set;} //singleton
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private GameObject r;
    public void Start()
    {
        rd = selectedCharacter.GetComponent<Rigidbody2D>(); //bind player
        animator = selectedCharacter.GetComponent<Animator>();
        moveAnimation = Animator.StringToHash("isMoving");
        isFacingRight = selectedCharacter.transform.localScale.x > 0;
        originalPosition = selectedCharacter.transform.position;
        
        selectedCharacter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //unlock physics
        r = selectedCharacter.GetComponentInChildren<SelectCharacter>().gameObject; r.SetActive(false); //disable for selectionr
        // selectedCharacter.GetComponentInChildren<SelectCharacter>().enabled = false;
    }

    public void ResetCurrentCharacter() //reset character to default position
    {
        selectedCharacter.transform.position = originalPosition;
        selectedCharacter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; //lock physics
        r.SetActive(true); //enable for selection
        // selectedCharacter.GetComponentInChildren<SelectCharacter>().enabled = true;
    }

    private void Update()
    {
        transform.position = selectedCharacter.transform.position;
        animator.SetBool(moveAnimation, MoveControl());
    }

    private bool MoveControl()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(horizontal, vertical);
        rd.linearVelocity = moveInput.normalized * speed;

        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        if (horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            Flip();
        }
        return isMoving;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = selectedCharacter.transform.localScale;
        scale.x *= -1; 
        selectedCharacter.transform.localScale = scale;
    }
 }