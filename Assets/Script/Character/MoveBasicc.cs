using UnityEngine;

public class MoveBasic : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    private Rigidbody2D rd;
    [SerializeField] public bool isFacingRight; 
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animator.SetBool("isMoving", MoveControl());
    }

    public bool MoveControl()
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
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }

}
