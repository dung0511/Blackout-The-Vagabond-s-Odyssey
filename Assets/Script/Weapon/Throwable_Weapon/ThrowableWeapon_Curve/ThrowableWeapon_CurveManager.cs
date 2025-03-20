using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;

public class ThrowableWeapon_CurveManager : MonoBehaviour
{
    //[SerializeField] public GameObject projectilePrefab;

    //[SerializeField] private float shootRate;
    //[SerializeField] private float projectileMaxMoveSpeed;
    //[SerializeField] private float projectileMaxHeight;

    //[SerializeField] private AnimationCurve trajectoryAnimationCurve;
    //[SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    //[SerializeField] private AnimationCurve projectileSpeedAnimationCurve;

    //private float shootTimer;
    //public SpriteRenderer currentCharacterSR;
    //private SpriteRenderer weaponSpriteRenderer;
    //private void Start()
    //{
    //    weaponSpriteRenderer=GetComponent<SpriteRenderer>();
    //    Transform player = transform.parent.parent;
    //    currentCharacterSR = player.GetComponentInChildren<SpriteRenderer>();
    //  //  inHand = true;
    //    projectilePrefab.GetComponent<ThrowableWeapon_Curve>().curveManager=GetComponent<ThrowableWeapon_CurveManager>();
    //}

    //private void Update()
    //{
    //    shootTimer -= Time.deltaTime;

    //    RotateGun();
    //    if (Input.GetMouseButtonDown(0) && shootTimer <= 0)
    //    {
    //        TurnOffSpriteRenderer();
    //        shootTimer = shootRate;
    //        //ThrowableWeapon_Curve projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<ThrowableWeapon_Curve>();


    //        //Vector3 mousePos = Input.mousePosition;

    //        //mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

    //        //Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
    //        GameObject bullet = PoolManagement.Instance.GetBullet(projectilePrefab);

    //        bullet.transform.position = transform.position;
    //        bullet.transform.rotation = Quaternion.identity;

    //        ThrowableWeapon_Curve projectile = bullet.GetComponent<ThrowableWeapon_Curve>();


    //        Vector3 mousePos = Input.mousePosition;
    //        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
    //        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

    //        projectile.InitializeProjectile(worldMousePos, projectileMaxMoveSpeed, projectileMaxHeight);
    //        projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
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

    //private float RotateToMousePos()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = Camera.main.nearClipPlane;

    //    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
    //    Vector2 lookDir = (Vector2)(worldMousePos - transform.position);
    //    return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    //}

    //public void TurnOffSpriteRenderer()
    //{
    //    weaponSpriteRenderer.enabled=false;
    //}

    //public void TurnOnSpriteRenderer()
    //{
    //    weaponSpriteRenderer.enabled = true;
    //}

    [SerializeField] public GameObject projectilePrefab;

    [SerializeField] private float shootRate;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private float projectileMaxHeight;

    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;

    private float shootTimer;
    public SpriteRenderer characterSR;
    private SpriteRenderer weaponSpriteRenderer;

    public ThrowableWeaponCurveService weaponCurveService;

    void Start()
    {
        characterSR = GetComponentInParent<Transform>().root.Find("Character").GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        projectilePrefab.GetComponent<ThrowableWeapon_Curve>().curveManager = GetComponent<ThrowableWeapon_CurveManager>();
        // bulletPrefab.GetComponent<Bullet>().dame =
        weaponCurveService = new ThrowableWeaponCurveService(
            new WeaponRepository(),
            true,
            characterSR,
            transform,
            shootTimer,
            weaponSpriteRenderer,
            projectilePrefab,
            projectileMaxMoveSpeed,
            projectileMaxHeight,
            trajectoryAnimationCurve,
            axisCorrectionAnimationCurve,
            projectileSpeedAnimationCurve
        );
    }

    void Update()
    {

        weaponCurveService.RotateWeapon();
        shootTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && shootTimer <= 0)
        {
            shootTimer = shootRate;
            weaponCurveService.Attack();
            //  Invoke(nameof(StopFiringAnimation), 0.2f);
        }
    }

    public void TurnOffSpriteRenderer()
    {
        weaponSpriteRenderer.enabled = false;
    }

    public void TurnOnSpriteRenderer()
    {
        weaponSpriteRenderer.enabled = true;
    }
}
