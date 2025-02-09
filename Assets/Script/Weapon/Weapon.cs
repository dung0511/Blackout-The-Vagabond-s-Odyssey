using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;
    public GameObject bullet;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    public float bulletForce;
    public float BulletDame;
    private float timeBtwFire = 0;
    public GameObject weapon;
    public SpriteRenderer characterSR;

    // Start is called before the first frame update
    void Start()
    {
        characterSR = GetComponent<SpriteRenderer>();
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3((float)0.2, (float)-0.2, 0);
            characterSR.gameObject.transform.localScale = new Vector3(-1, characterSR.gameObject.transform.localScale.y, 0);
        }
        else
        {
            transform.localScale = new Vector3((float)0.2, (float)0.2, 0);
            characterSR.gameObject.transform.localScale = new Vector3(1, characterSR.gameObject.transform.localScale.y, 0);
        }

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
