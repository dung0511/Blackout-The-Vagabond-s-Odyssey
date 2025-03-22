using Assets.Script.Service;
using UnityEngine;

public class RangedWeaponService : WeaponService
{
    private float _fireRate;
    private float _lastFireTime;
    private GameObject _bulletPrefab;
    private Transform _firePos;
    private float _bulletForce;
    //  private bool _isFiring = false;
    private int _dame;
    public RangedWeaponService(IWeaponRepository weaponRepository, bool inHand, SpriteRenderer characterSR, Transform transform, float fireRate, GameObject bulletPrefab, Transform firePos, float bulletForce, int dame)
        : base(weaponRepository, inHand, characterSR, transform)
    {
        _fireRate = fireRate;
        _bulletPrefab = bulletPrefab;
        _firePos = firePos;
        _bulletForce = bulletForce;
        _dame = dame;
        // _isFiring = isFiring;
    }

    public float GetFireRate() => _fireRate;
    public void SetFireRate(float value) => _fireRate = value;

   
    public GameObject GetBulletPrefab() => _bulletPrefab;
    public void SetBulletPrefab(GameObject value) => _bulletPrefab = value;

   
    public Transform GetFirePos() => _firePos;
    public void SetFirePos(Transform value) => _firePos = value;

   
    public float GetBulletForce() => _bulletForce;
    public void SetBulletForce(float value) => _bulletForce = value;

   
    public int GetDamage() => _dame;
    public void SetDamage(int value) => _dame = value;

   
    public bool IsInHand() => _inHand;
    public void SetInHand(bool value) => _inHand = value;

   
    public Transform GetTransform() => _transform;
    public void SetTransform(Transform value) => _transform = value;

    
    public SpriteRenderer GetCharacterSpriteRenderer() => _characterSR;
    public void SetCharacterSpriteRenderer(SpriteRenderer value) => _characterSR = value;

    public override void Attack()
    {
        float angle = RotateToMousePos();
        float elapsedTime = Time.time - _lastFireTime;

        if (elapsedTime >= _fireRate)
        {
            _lastFireTime = Time.time;
            GameObject bulletTmp = PoolManagement.Instance.GetBullet(_bulletPrefab);

            if (bulletTmp == null) return;

            bulletTmp.transform.position = _firePos.position;
            bulletTmp.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(_transform.right * _bulletForce, ForceMode2D.Impulse);



            //_isFiring = true;
            //_firePos.gameObject.GetComponent<Animator>().SetBool("isFiring", _isFiring);
            //Invoke(nameof(StopFiringAnimation), 0.2f);

        }
    }

    public override void RotateWeapon()
    {
        float angle = RotateToMousePos();

        _transform.rotation = Quaternion.Euler(0, 0, angle);

        bool isFlipped = angle > 90 || angle < -90;

        _characterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
            _characterSR.gameObject.transform.localScale.y, 1);

        _transform.localScale = new Vector3(0.6f, isFlipped ? -0.6f : 0.6f, 1);
    }

    private float RotateToMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)(worldMousePos - _transform.position);
        return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }

    public override void DropWeapon()
    {
        _characterSR = null;
        _transform.rotation = Quaternion.identity;
        _transform.localScale = new Vector3(5, 5, 0);      
        _transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        _inHand = false;
    }
    public override void PickWeapon()
    {
        _transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _characterSR = _transform.root.GetComponentInChildren<SpriteRenderer>();
        _transform.localScale = new Vector3(0.6f, 0.6f, 0);
        _transform.localPosition = new Vector3(0, -0.04f, 0);
        _inHand = true;
    }

}
