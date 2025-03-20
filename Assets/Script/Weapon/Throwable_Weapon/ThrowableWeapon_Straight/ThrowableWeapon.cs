using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Weapon.Throwable_Weapon
{
    public class ThrowableWeapon: MonoBehaviour
    {
        //    public GameObject bullet;
        //    public Transform firePos;

        //    public float TimeBtwFire = 0.2f;
        //    public float bulletForce;
        //    private float lastFireTime = 0;

        //    public int BulletDame;

        //    private bool isFiring = false;
        //    public bool inHand;

        //    public SpriteRenderer currentCharacterSR;
        //    public ThrowableWeaponSO throwableWeaponDetail;

        //    void Start()
        //    {

        //        TimeBtwFire = throwableWeaponDetail.TimeBtwFire;
        //        bulletForce = throwableWeaponDetail.throwableForce;
        //        BulletDame = throwableWeaponDetail.damageThrowableWeapon;
        //        if (GetComponentInParent<WeaponController>() != null)
        //        {
        //            Transform player = transform.parent.parent;
        //            currentCharacterSR = player.GetComponentInChildren<SpriteRenderer>();
        //            inHand = true;
        //            GetComponent<BoxCollider2D>().enabled = false;

        //        }
        //        else
        //        {
        //            inHand = false;
        //            InGround(gameObject);
        //        }

        //    }

        //    void Update()
        //    {
        //        if (inHand)
        //        {
        //            GetComponent<BoxCollider2D>().enabled = false;
        //            RotateGun();
        //            if (Input.GetMouseButton(0))
        //            {
        //                Fire();
        //            }

        //        }

        //    }

        //    void RotateGun()
        //    {
        //        float angle = RotateToMousePos();

        //        transform.rotation = Quaternion.Euler(0, 0, angle);

        //        bool isFlipped = angle > 90 || angle < -90;

        //        currentCharacterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
        //            currentCharacterSR.gameObject.transform.localScale.y, 1);

        //        transform.localScale = new Vector3(0.7f, isFlipped ? -0.7f : 0.7f, 1);
        //    }

        //    public void Fire()
        //    {
        //        float angle = RotateToMousePos();
        //        float elapsedTime = Time.time - lastFireTime;

        //        if (elapsedTime >= TimeBtwFire)
        //        {
        //            lastFireTime = Time.time;
        //            GameObject bulletTmp = PoolManagement.Instance.GetBullet(bullet);

        //            if (bulletTmp == null) return;

        //            bulletTmp.transform.position = firePos.position;
        //            bulletTmp.transform.rotation = Quaternion.Euler(0, 0, angle);
        //            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

        //            rb.linearVelocity = Vector2.zero;
        //            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);


        //            if (!isFiring)
        //            {
        //                isFiring = true;
        //            }
        //        }
        //    }


        //    private float RotateToMousePos()
        //    {
        //        Vector3 mousePos = Input.mousePosition;
        //        mousePos.z = Camera.main.nearClipPlane;

        //        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //        Vector2 lookDir = (Vector2)(worldMousePos - transform.position);
        //        return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //    }


        //    public void InGround(GameObject game)
        //    {
        //        game.GetComponent<ThrowableWeapon>().currentCharacterSR = null;
        //        game.transform.rotation = Quaternion.identity;
        //        game.GetComponent<ThrowableWeapon>().inHand = false;
        //        game.transform.localScale = new Vector3(5, 5, 0);
        //        game.GetComponent<BoxCollider2D>().enabled = true;
        //    }

        public SpriteRenderer characterSR;
        public Transform firePos;
        public GameObject throwablePrefab;
        public float throwCoolDown = 0.5f;
        public float bulletForce = 20f;
        public int dame = 1;
        public ThrowableWeaponStraightService throwStraightWeapon;
       // private bool isFiring = false;

        void Start()
        {
            // bulletPrefab.GetComponent<Bullet>().dame =
           // throwablePrefab.GetComponent<ThrowableWeaponCollider>().weapon.dame = dame;
            throwStraightWeapon = new ThrowableWeaponStraightService(
                new WeaponRepository(),
                true,
                characterSR,
                transform,
                throwCoolDown,
                throwablePrefab,
                firePos,
                bulletForce,
                dame
            );
        }

        void Update()
        {

            throwStraightWeapon.RotateWeapon();


            if (Input.GetMouseButton(0))
            {
                throwStraightWeapon.Attack();
                //  Invoke(nameof(StopFiringAnimation), 0.2f);
            }
        }

    }


}
