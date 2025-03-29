using Assets.Script.Service;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;

public class MeleeWeaponService : WeaponService
{
    private int attackIndex = 1;
    private float comboResetTime = 5f;
    private float _attackCooldown;
    private float _lastAttackTime;
    private int _damage;
    private Animator _animator;

    public MeleeWeaponService(IWeaponRepository weaponRepository, bool inHand, SpriteRenderer characterSR, Animator animator, Transform transform, float attackCooldown, int damage)
        : base(weaponRepository, inHand, characterSR, transform)
    {
        _attackCooldown = attackCooldown;
        _damage = damage;
        _animator = animator;
    }


    public float GetAttackCooldown() => _attackCooldown;
    public void SetAttackCooldown(float value) => _attackCooldown = value;


    public int GetDamage() => _damage;
    public void SetDamage(int value) => _damage = value;


    public Animator GetAnimator() => _animator;
    public void SetAnimator(Animator value) => _animator = value;


    public bool IsInHand() => _inHand;
    public void SetInHand(bool value) => _inHand = value;


    public Transform GetTransform() => _transform;
    public void SetTransform(Transform value) => _transform = value;


    //public SpriteRenderer GetCharacterSpriteRenderer() => _characterSR;
    public SpriteRenderer GetCharacterSR()
    {
        return _characterSR;
    }
    public void SetCharacterSpriteRenderer(SpriteRenderer value) => _characterSR = value;

    public override void Attack()
    {        
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName($"Attack{attackIndex}"))
            return;

      
        if (Time.time - _lastAttackTime > comboResetTime)
            attackIndex = 1;

        _animator.ResetTrigger($"Attack{attackIndex}"); 
        _animator.SetTrigger($"Attack{attackIndex}");  

        _lastAttackTime = Time.time; 

        attackIndex++; 
        if (attackIndex > 3) attackIndex = 1; 

    }

    public override void RotateWeapon()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)(worldMousePos - _transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        _transform.rotation = Quaternion.Euler(0, 0, angle);
        bool isFlipped = angle > 90 || angle < -90;
        _characterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
        _characterSR.gameObject.transform.localScale.y, 1);


        _transform.localScale = new Vector3(0.7f, isFlipped ? -0.7f : 0.7f, 1);
    }

    public override void DropWeapon()
    {
        _characterSR = null;
        _inHand = false;
        _transform.rotation = Quaternion.identity;
        _transform.localScale = new Vector3(5, 5, 0);
        _transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void PickWeapon()
    {
        _transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _characterSR = _transform.root.GetComponentInChildren<SpriteRenderer>();
        _transform.localScale = new Vector3(0.7f, 0.7f, 0);
        _transform.localPosition = new Vector3(0, -0.03f, 0);
        _inHand = true;
    }
}
