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

    public override void DropWeapon(GameObject game)
    {
        // Drop logic
    }
    private float RotateToMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)(worldMousePos - _transform.position);
        return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }

   
}
