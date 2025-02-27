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
    private bool isFiring = false;
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
            GameObject bulletTmp = BulletPoolManagement.Instance.GetGameObject(bullet);

            if (bulletTmp == null) return; 

            bulletTmp.transform.position = firePos.position;
            bulletTmp.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

            
            if (!isFiring)
            {
                isFiring = true;
                firePos.gameObject.GetComponent<Animator>().SetBool("isFiring", true);
                Invoke(nameof(StopFiringAnimation), 0.2f);
            }
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
    private void StopFiringAnimation()
    {
        isFiring = false;
        firePos.gameObject.GetComponent<Animator>().SetBool("isFiring", false);
    }
}
