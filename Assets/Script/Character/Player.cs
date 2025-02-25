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

    public PlayerHealthController healthController;
    public PlayerArmorController armorController;
    public PlayerMoveController moveController;
    public WeaponController weaponController;
    private void Awake()
    {
        health = playerDetailSO.playerHealthAmount;
        armor = playerDetailSO.playerArmorAmount;

        
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        healthController = GetComponent<PlayerHealthController>();
        armorController = GetComponent<PlayerArmorController>();
        moveController = GetComponent<PlayerMoveController>();
        weaponController=GetComponentInChildren<WeaponController>();


    }
    //public Rigidbody2D rd;
    //public SpriteRenderer characterSR;
    //private Animator anim;
    //public bool isGamePaused = false;
    //public int health;
    //public int armor;
    //public bool isHurt;
    //public bool isDead;
    //public float speed = 5f;
    //private Vector3 moveInput;

    //private PlayerArmorController armorController;
    //private PlayerHealthController healthController;
    //private PlayerMoveController moveController;

    //public PlayerDetailSO playerDetailSO;
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //private void Awake()
    //{
    //    // Initialize components
    //    armorController = GetComponent<PlayerArmorController>();
    //    healthController = GetComponent<PlayerHealthController>();
    //    moveController = GetComponent<PlayerMoveController>();

    //    // Initialize stats from Scriptable Object
    //    health = playerDetailSO.playerHealthAmount;
    //    armor = playerDetailSO.playerArmorAmount;
    //}
    //void Start()
    //{
    //    rd = GetComponent<Rigidbody2D>();
    //    anim = GetComponentInChildren<Animator>();
    //    isGamePaused = false;
    //    isHurt = GetComponent<PlayerHealthController>().isHurt;
    //    isDead = GetComponent<PlayerHealthController>().isDead;

    //    characterSR = GetComponentInChildren<SpriteRenderer>();
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    if (!isDead)
    //    {
    //        Move();
    //        Health();
    //    }
    //    else
    //    {
    //        rd.linearVelocity = Vector2.zero;
    //        //GameObject.Find("Aim").gameObject.SetActive(false);
    //        //transform.Find("WeaponManagemnt").gameObject.SetActive(false);
    //        Cursor.visible = true;
    //        anim.SetBool("isDead", true);

    //    }
    //    if (isHurt && !isDead)
    //    {
    //        anim.SetBool("isHurt", true);
    //    }
    //    else anim.SetBool("isHurt", false);

    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponController.ChangeWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponController.ChangeWeapon(2);
        }
        if (!healthController.IsDead)
        {
            
            bool isMoving = moveController.MoveControl();
            anim.SetBool("isMoving", isMoving);
           
        }
        else
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            armorController.ArmorWhenPlayerDie();
            rd.linearVelocity = Vector2.zero;
            Cursor.visible = true;
            anim.SetBool("isDead", true);
        }

       
        if (healthController.IsHurt && !healthController.IsDead)
            anim.SetBool("isHurt", true);
        else
            anim.SetBool("isHurt", false);
    }

    public void Health()
    {
        //isHurt = GetComponent<PlayerHealthController>().isHurt;
        //isDead = GetComponent<PlayerHealthController>().isDead;
        //health = GetComponent<PlayerHealth>().health;

    }
    public void Move()
    {
        anim.SetBool("isMoving", moveController.MoveControl());
    }
}
