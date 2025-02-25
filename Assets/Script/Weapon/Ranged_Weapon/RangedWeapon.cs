using UnityEngine;
using UnityEngine.TextCore.Text;

public class RangedWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    public float bulletForce;
    public int BulletDame;
    private float timeBtwFire = 0;
    public SpriteRenderer currentCharacterSR;
    public RangedWeaponSO rangedDetail;


    void Awake()
    {
        //rangedDetail = GetComponent<RangedWeaponSO>();
        BulletDame = rangedDetail.damageRangedWeapon;
        currentCharacterSR = GameObject.Find("Character").GetComponent<SpriteRenderer>();
        //anim = GetComponentInChildren<Animator>();
    }

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

        currentCharacterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
                                                                     currentCharacterSR.gameObject.transform.localScale.y, 1);


        transform.localScale = new Vector3(0.6f, isFlipped ? -0.6f : 0.6f, 1);
    }

    public void Fire()
    {

        timeBtwFire -= Time.deltaTime;

        if (timeBtwFire <= 0)
        {
            timeBtwFire = TimeBtwFire;
            GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        }
    }

}
