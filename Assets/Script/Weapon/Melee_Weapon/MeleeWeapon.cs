using Assets.Script.Service.IService;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IPickService
{
    private SpriteRenderer characterSR;
    private Animator animator;
    public MeleeWeaponService meleeWeapon;
    private float lastAttackTime=0f;
    public float attackCoolDown;
    private bool inHand;
    private int dam;
    public MeleeWeaponSO MeleeWeaponSO;

    void Start()
    {

        attackCoolDown = MeleeWeaponSO.attackCooldown;
        dam = MeleeWeaponSO.damageMeleeWeapon;
        animator = GetComponentInChildren<Animator>();
        meleeWeapon = new MeleeWeaponService(
            new WeaponRepository(),
            false,
            characterSR,
            animator,
            transform,
           attackCoolDown,
            dam
        );

        if (!GetComponentInParent<Transform>().root.Find("Character").IsUnityNull())
        {
            // characterSR = GetComponentInParent<Transform>().root.Find("Character").GetComponent<SpriteRenderer>();
            characterSR = transform.root.GetComponentInChildren<SpriteRenderer>();
            inHand = true;
            meleeWeapon.SetInHand(inHand);
            meleeWeapon.SetCharacterSpriteRenderer(characterSR);
        }
        else
        {
            inHand = false;
            meleeWeapon.SetInHand(inHand);
            meleeWeapon.DropWeapon();
        }
    }

    void Update()
    {
        if (!inHand) return;
        meleeWeapon.RotateWeapon();
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= lastAttackTime + attackCoolDown)
            {
                meleeWeapon.Attack();
                lastAttackTime = Time.time;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isMeleeAttack", false);
        }
    }
    //void AttackMelee()
    //{
    //    StartCoroutine(AttackCoroutine());
    //}

    //IEnumerator AttackCoroutine()
    //{
        

    //    yield return new WaitForSeconds(0.4f);
    //}
    public GameObject GetPickWeaponGameOject()
    {
        return gameObject;
    }

    public void Pick()
    {
        characterSR = transform.root.GetComponentInChildren<SpriteRenderer>();
        inHand = true;
        meleeWeapon.PickWeapon();
    }

    public void Drop()
    {
        characterSR = null;
        inHand = false;
        meleeWeapon.DropWeapon();
    }
}
