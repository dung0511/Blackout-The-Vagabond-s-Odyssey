using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rd;
    public SpriteRenderer characterSR;
    private Animator anim;
    public bool isGamePaused = false;
    public int health;
    public int armor;
    public bool isHurt;
    public bool isDead;
    public PlayerDetailSO playerDetailSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        isGamePaused = false;
        isHurt = GetComponent<PlayerHealthController>().isHurt;
        isDead = GetComponent<PlayerHealthController>().isDead;
        health = playerDetailSO.playerHealthAmount;
        armor = playerDetailSO.playerArmorAmount;
        characterSR = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            Health();
        }
        else
        {
            rd.linearVelocity = Vector2.zero;
            //GameObject.Find("Aim").gameObject.SetActive(false);
            //transform.Find("WeaponManagemnt").gameObject.SetActive(false);
            Cursor.visible = true;
            anim.SetBool("isDead", true);
            
        }
        if (isHurt && !isDead)
        {
            anim.SetBool("isHurt", true);
        }
        else anim.SetBool("isHurt", false);
        
    }

    public void Health()
    {
        isHurt = GetComponent<PlayerHealthController>().isHurt;
        isDead = GetComponent<PlayerHealthController>().isDead;
        //health = GetComponent<PlayerHealth>().health;

    }
    public void Move()
    {
        anim.SetBool("isMoving", GetComponent<PlayerMoveController>().MoveControl());
    }
}
