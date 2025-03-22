using Unity.VisualScripting;
using UnityEngine;
 
 public class HomePlayerController : MonoBehaviour
 {
    public GameObject selectedCharacter;
    private GameObject previousCharacter;
    private Vector2 originalPosition;
    private Vector3 originalScale; 

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

    public void Start()
    {
        RebindCharacter();
    }

    public void RebindCharacter()
    {
        transform.SetParent(selectedCharacter.transform); transform.localPosition = new Vector2(0,0.069f);   //follow
        rd = selectedCharacter.GetComponent<Rigidbody2D>(); 
        animator = selectedCharacter.GetComponent<Animator>();
        moveAnimation = Animator.StringToHash("isMoving");
        isFacingRight = selectedCharacter.transform.localScale.x > 0;
        originalPosition = selectedCharacter.transform.position;
        originalScale = selectedCharacter.transform.localScale;
        
        selectedCharacter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //unlock physics
        previousCharacter = selectedCharacter.GetComponentInChildren<SelectableCharacter>().gameObject; 
        previousCharacter.SetActive(false); //disable for selection
    }

    public void ResetCurrentCharacter() //reset previous selected character to default 
    {
        selectedCharacter.transform.position = originalPosition;
        selectedCharacter.transform.localScale = originalScale;
        var prevRb = selectedCharacter.GetComponent<Rigidbody2D>();
        animator.SetBool(moveAnimation, false); //reset to idle
        prevRb.constraints = RigidbodyConstraints2D.FreezeAll; //lock physics
        previousCharacter.SetActive(true); //enable for selection
    }

    private void Update()
    {
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



