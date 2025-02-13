using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;
    public GameObject bullet;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    public float bulletForce;
    public int BulletDame;
    private float timeBtwFire = 0;
    public GameObject weapon;
    public SpriteRenderer currentCharacterSR;
    public CharacterManagement characterManagement;
    

    // Start is called before the first frame update
    void Start()
    {
        currentCharacterSR = characterManagement.currentCharacterSR;
    }
    //private void Awake()
    //{
    //    instance = this;
    //}
    // Update is called once per frame
    void Update()
    {
        RotateGun();
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }
    void RotateGun()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; 

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)(worldMousePos - transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        
        transform.rotation = Quaternion.Euler(0, 0, angle);

       
        bool isFlipped = angle > 90 || angle < -90;
        transform.localScale = new Vector3(0.2f, isFlipped ? -0.2f : 0.2f, 1);

        
        currentCharacterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
                                                                         currentCharacterSR.gameObject.transform.localScale.y, 1);
    }

    public void Fire()
    {

        timeBtwFire -= Time.deltaTime;

        if (timeBtwFire <= 0)
        {
            timeBtwFire = TimeBtwFire;

            //if ((isFacingRight|| isTargettingEnemy(player,listEnemy)) || !isFacingRight && isTargettingEnemy(player, listEnemy)) {
            GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
            //}
            //else if(!isFacingRight && !isTargettingEnemy(player, listEnemy))
            //{
            //    GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
            //    Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            //    rb.AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
            //}
        }

    }
    
}
