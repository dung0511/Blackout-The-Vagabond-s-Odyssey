using Assets.Script.Service;
using Assets.Script.Weapon.Throwable_Weapon;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class ThrowableWeaponStraightService : WeaponService
{
    private float _throwCooldown;
    private float _lastThrowTime;
    private GameObject _throwablePrefab;
    private Transform _firePos;
    private float _throwForce;
    private int _damage;
    private Animation _animation;
    public ThrowableWeaponStraightService(IWeaponRepository weaponRepository, bool inHand, SpriteRenderer characterSR, Transform transform, float throwCooldown, GameObject throwablePrefab, Transform firePos, float throwForce,int damage, Animation animation)
        : base(weaponRepository, inHand, characterSR, transform)
    {
        _throwCooldown = throwCooldown;
        _throwablePrefab = throwablePrefab;
        _firePos = firePos;
        _throwForce = throwForce;
        _damage = damage;
        _animation = animation;
    }

    public float GetThrowCooldown() => _throwCooldown;
    public void SetThrowCooldown(float value) => _throwCooldown = value;

    public float GetLastThrowTime() => _lastThrowTime;
    public void SetLastThrowTime(float value) => _lastThrowTime = value;

    public GameObject GetThrowablePrefab() => _throwablePrefab;
    public void SetThrowablePrefab(GameObject value) => _throwablePrefab = value;

    public Transform GetFirePos() => _firePos;
    public void SetFirePos(Transform value) => _firePos = value;

    public float GetThrowForce() => _throwForce;
    public void SetThrowForce(float value) => _throwForce = value;

    public int GetDamage() => _damage;
    public void SetDamage(int value) => _damage = value;

    public bool IsInHand() => _inHand;
    public void SetInHand(bool value) => _inHand = value;

    public Transform GetTransform() => _transform;
    public void SetTransform(Transform value) => _transform = value;

    public SpriteRenderer GetCharacterSpriteRenderer() => _characterSR;
    public void SetCharacterSpriteRenderer(SpriteRenderer value) => _characterSR = value;

    public override void Attack()
    {
        float angle = RotateToMousePos();
        float elapsedTime = Time.time - _lastThrowTime;

        if (elapsedTime >= _throwCooldown)
        {
            _lastThrowTime = Time.time;
            GameObject bulletTmp = PoolManagement.Instance.GetBullet(_throwablePrefab);

            if (bulletTmp == null) return;

            bulletTmp.transform.position = _firePos.position;
            bulletTmp.transform.rotation = Quaternion.Euler(0, 0, angle);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(_transform.right * _throwForce, ForceMode2D.Impulse);
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
