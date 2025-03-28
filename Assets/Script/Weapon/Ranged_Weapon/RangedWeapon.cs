using Assets.Script.Service.IService;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RangedWeapon : MonoBehaviour, IPickService
{
    //public GameObject bullet;
    //public Transform firePos;
    //public float TimeBtwFire = 0.2f;
    //public float bulletForce;
    //public int BulletDame;
    //// private float timeBtwFire = 0;
    //private float lastFireTime = 0;
    //private bool isFiring = false;
    //public bool inHand;
    //public SpriteRenderer currentCharacterSR;
    //public RangedWeaponSO rangedDetail;

    //void Start()
    //{
    //    TimeBtwFire = rangedDetail.TimeBtwFire;
    //    bulletForce = rangedDetail.bulletForce;
    //    BulletDame = rangedDetail.damageRangedWeapon;
    //    if (GetComponentInParent<WeaponController>() != null )
    //    {
    //        Transform player = transform.parent.parent;
    //        currentCharacterSR = player.GetComponentInChildren<SpriteRenderer>();
    //        inHand = true;
    //        GetComponent<BoxCollider2D>().enabled = false;

    //    }
    //    else
    //    {
    //        inHand = false;
    //        InGround(gameObject);
    //    }

    //}

    //void Update()
    //{
    //    if (inHand)
    //    {
    //        GetComponent<BoxCollider2D>().enabled = false;
    //        RotateGun();
    //        if (Input.GetMouseButton(0))
    //        {
    //            Fire();
    //        }

    //    }

    //}

    //void RotateGun()
    //{
    //    float angle = RotateToMousePos();

    //    transform.rotation = Quaternion.Euler(0, 0, angle);

    //    bool isFlipped = angle > 90 || angle < -90;

    //    currentCharacterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
    //        currentCharacterSR.gameObject.transform.localScale.y, 1);

    //    transform.localScale = new Vector3(0.6f, isFlipped ? -0.6f : 0.6f, 1);
    //}

    //public void Fire()
    //{
    //    float angle = RotateToMousePos();
    //    float elapsedTime = Time.time - lastFireTime;

    //    if (elapsedTime >= TimeBtwFire)
    //    {
    //        lastFireTime = Time.time;
    //        GameObject bulletTmp = PoolManagement.Instance.GetBullet(bullet);

    //        if (bulletTmp == null) return; 

    //        bulletTmp.transform.position = firePos.position;
    //        bulletTmp.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    //        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

    //        rb.linearVelocity = Vector2.zero;
    //        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);


    //        if (!isFiring)
    //        {
    //            isFiring = true;
    //            firePos.gameObject.GetComponent<Animator>().SetBool("isFiring", true);
    //            Invoke(nameof(StopFiringAnimation), 0.2f);
    //        }
    //    }
    //}


    //private float RotateToMousePos()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = Camera.main.nearClipPlane;

    //    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
    //    Vector2 lookDir = (Vector2)(worldMousePos - transform.position);
    //    return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    //}
    //private void StopFiringAnimation()
    //{
    //    isFiring = false;
    //    firePos.gameObject.GetComponent<Animator>().SetBool("isFiring", false);
    //}

    //public void InGround(GameObject game)
    //{
    //    game.GetComponent<RangedWeapon>().currentCharacterSR = null;
    //    game.transform.rotation = Quaternion.identity;
    //    game.GetComponent<RangedWeapon>().inHand = false;
    //    game.transform.localScale = new Vector3(5,5,0);
    //    game.GetComponent<BoxCollider2D>().enabled = true;
    //}

    public SpriteRenderer characterSR;
    public Transform firePos;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float bulletForce = 20f;
    public int dam = 1;
    public RangedWeaponService rangedWeapon;
    //private bool isFiring=false;
    public bool inHand;
    public RangedWeaponSO RangedWeaponSO;


    public GameObject GetPickGameOject()
    {
        return gameObject;
    }

    public void Pick()
    {
        characterSR = transform.root.GetComponentInChildren<SpriteRenderer>();
        inHand = true;
        rangedWeapon.PickWeapon();
    }

    public void Drop()
    {
        characterSR = null;
        inHand = false;
        rangedWeapon.DropWeapon();
    }

    public bool IsPickingItemOrWeapon()
    {
        return true;
    }

    void Start()
    {
        fireRate = RangedWeaponSO.TimeBtwFire;
        bulletForce = RangedWeaponSO.bulletForce;
        dam = RangedWeaponSO.damageRangedWeapon;

        rangedWeapon = new RangedWeaponService(
                   new WeaponRepository(),
                   true,
                   characterSR,
                   transform,
                   fireRate,
                   bulletPrefab,
                   firePos,
                   bulletForce,
                   dam
               );

        if (!GetComponentInParent<Transform>().root.Find("Character").IsUnityNull())
        {
            characterSR = transform.root.GetComponentInChildren<SpriteRenderer>();
            inHand = true;
            rangedWeapon.SetCharacterSpriteRenderer(characterSR);
            rangedWeapon.SetInHand(inHand);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {

            inHand = false;
            rangedWeapon.SetInHand(inHand);
            rangedWeapon.DropWeapon();
        }
    }

    void Update()
    {
        if (!inHand) return;
        rangedWeapon.RotateWeapon();


        if (Input.GetMouseButton(0))
        {
            rangedWeapon.Attack();
            //  Invoke(nameof(StopFiringAnimation), 0.2f);
        }
    }

    //private void StopFiringAnimation()
    //{
    //    isFiring = false;
    //    firePos.gameObject.GetComponent<Animator>().SetBool("isFiring", isFiring);
    //}
}
