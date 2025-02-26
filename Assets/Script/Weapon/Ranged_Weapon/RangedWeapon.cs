using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    public float bulletForce;
    public int BulletDame;
    // private float timeBtwFire = 0;
    private float lastFireTime = 0;
    public SpriteRenderer currentCharacterSR;
    public RangedWeaponSO rangedDetail;

    void Awake()
    {
        TimeBtwFire = rangedDetail.TimeBtwFire;
        bulletForce = rangedDetail.bulletForce;
        BulletDame = rangedDetail.damageRangedWeapon;
        currentCharacterSR = GameObject.Find("Character").GetComponent<SpriteRenderer>();
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
        float angle = RotateToMousePos();

        transform.rotation = Quaternion.Euler(0, 0, angle);

        bool isFlipped = angle > 90 || angle < -90;

        currentCharacterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
            currentCharacterSR.gameObject.transform.localScale.y, 1);

        transform.localScale = new Vector3(0.6f, isFlipped ? -0.6f : 0.6f, 1);
    }

    public void Fire()
    {
        float angle = RotateToMousePos();
        float elapsedTime = Time.time - lastFireTime;

        if (elapsedTime >= TimeBtwFire)
        {
            lastFireTime = Time.time;
            GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.Euler(0, 0, angle - 90));
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        }
    }

    private float RotateToMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)(worldMousePos - transform.position);
        return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }
}
