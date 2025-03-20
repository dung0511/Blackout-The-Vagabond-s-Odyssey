using Assets.Script.Service;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;

public class MeleeWeaponService : WeaponService
{
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

    public override void Attack()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            _animator.SetBool("isMeleeAttack", true);
            _lastAttackTime = Time.time;

        }
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

    public override void DropWeapon(GameObject game)
    {
        game.GetComponent<MeleeWeapon>().meleeWeapon._characterSR = null;
        game.transform.rotation = Quaternion.identity;
        game.GetComponent<MeleeWeapon>().meleeWeapon._inHand = false;
        game.transform.localScale = new Vector3(5, 5, 0);
        game.GetComponent<BoxCollider2D>().enabled = true;
    }
}
