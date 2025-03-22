using Assets.Script.Service;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class ThrowableWeaponCurveService : WeaponService
{
    private GameObject _projectilePrefab;

    private float _throwCooldown;
    private float _lastThrowTime;
    private float _projectileMaxMoveSpeed;
    private float _projectileMaxHeight;
    private SpriteRenderer _weaponSpriteRenderer;
    private AnimationCurve _trajectoryCurve;
    private AnimationCurve _axisCorrectionCurve;
    private AnimationCurve _speedCurve;

    public ThrowableWeaponCurveService(IWeaponRepository weaponRepository, bool inHand, SpriteRenderer characterSR, Transform transform, float throwCooldown, SpriteRenderer weaponSpriteRenderer, GameObject projectilePrefab, float projectileMaxMoveSpeed, float projectileMaxHeight,
                                       AnimationCurve trajectoryCurve, AnimationCurve axisCorrectionCurve, AnimationCurve speedCurve)
        : base(weaponRepository, inHand, characterSR, transform)
    {
        _throwCooldown = throwCooldown;
        _trajectoryCurve = trajectoryCurve;
        _axisCorrectionCurve = axisCorrectionCurve;
        _speedCurve = speedCurve;
        _weaponSpriteRenderer = weaponSpriteRenderer;
        _projectilePrefab = projectilePrefab;
        _projectileMaxHeight = projectileMaxHeight;
        _trajectoryCurve = trajectoryCurve;
        _projectileMaxMoveSpeed = projectileMaxMoveSpeed;
    }

    public float GetThrowCooldown() => _throwCooldown;
    public void SetThrowCooldown(float value) => _throwCooldown = value;

    public GameObject GetProjectilePrefab() => _projectilePrefab;
    public void SetProjectilePrefab(GameObject value) => _projectilePrefab = value;

    public float GetProjectileMaxMoveSpeed() => _projectileMaxMoveSpeed;
    public void SetProjectileMaxMoveSpeed(float value) => _projectileMaxMoveSpeed = value;

    public float GetProjectileMaxHeight() => _projectileMaxHeight;
    public void SetProjectileMaxHeight(float value) => _projectileMaxHeight = value;

    public SpriteRenderer GetWeaponSpriteRenderer() => _weaponSpriteRenderer;
    public void SetWeaponSpriteRenderer(SpriteRenderer value) => _weaponSpriteRenderer = value;

    public AnimationCurve GetTrajectoryCurve() => _trajectoryCurve;
    public void SetTrajectoryCurve(AnimationCurve value) => _trajectoryCurve = value;

    public AnimationCurve GetAxisCorrectionCurve() => _axisCorrectionCurve;
    public void SetAxisCorrectionCurve(AnimationCurve value) => _axisCorrectionCurve = value;

    public AnimationCurve GetSpeedCurve() => _speedCurve;
    public void SetSpeedCurve(AnimationCurve value) => _speedCurve = value;

    public bool IsInHand() => _inHand;
    public void SetInHand(bool value) => _inHand = value;

    public Transform GetTransform() => _transform;
    public void SetTransform(Transform value) => _transform = value;

    public SpriteRenderer GetCharacterSpriteRenderer() => _characterSR;
    public void SetCharacterSpriteRenderer(SpriteRenderer value) => _characterSR = value;

    public override void Attack()
    {
        if (Time.time >= _lastThrowTime + _throwCooldown)
        {
            _lastThrowTime = _throwCooldown;

            //Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject bullet = PoolManagement.Instance.GetBullet(_projectilePrefab);

            bullet.transform.position = _transform.position;
            bullet.transform.rotation = Quaternion.identity;

            ThrowableWeapon_Curve projectile = bullet.GetComponent<ThrowableWeapon_Curve>();


            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            projectile.InitializeProjectile(worldMousePos, _projectileMaxMoveSpeed, _projectileMaxHeight);
            projectile.InitializeAnimationCurves(_trajectoryCurve, _axisCorrectionCurve, _speedCurve);
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
