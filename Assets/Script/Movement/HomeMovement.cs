using UnityEngine;

public class HomeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight;
    private int moveAnimation;
    private GameObject bindedObj;

    void Awake()
    {
        moveAnimation = Animator.StringToHash("isMoving");
    }

    public void BindObj(GameObject obj)
    {
        bindedObj = obj;
        rb = bindedObj.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = bindedObj.GetComponent<Animator>();
        isFacingRight = bindedObj.transform.localScale.x > 0;
    }

    private void Update()
    {
        if(bindedObj) animator.SetBool(moveAnimation, MoveControl());
    }

    private bool MoveControl()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(horizontal, vertical);
        rb.linearVelocity = moveInput.normalized * speed;

        if (horizontal > 0 && !isFacingRight)
            Flip();
        else if (horizontal < 0 && isFacingRight)
            Flip();

        return moveInput.sqrMagnitude > 0.01f;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = bindedObj.transform.localScale;
        scale.x *= -1;
        bindedObj.transform.localScale = scale;
        Debug.Log("flip " +bindedObj.transform.localScale);
    }
}
