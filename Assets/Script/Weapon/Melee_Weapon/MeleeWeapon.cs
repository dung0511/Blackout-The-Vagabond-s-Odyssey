using Assets.Script.Service.IService;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IPickService
{
    public SpriteRenderer characterSR;
    public Animator animator;
    public MeleeWeaponService meleeWeapon;
    public float lastAttackTime=0f;
    public float attackCoolDown;
    public bool inHand;
    public int dam;
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

        if (Input.GetMouseButtonDown(0)) 
        {
            if (Time.time >= lastAttackTime + attackCoolDown)
            {
                meleeWeapon.Attack();
                lastAttackTime = Time.time;
            }
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
    public GameObject GetPickGameOject()
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

    public bool IsPickingItemOrWeapon()
    {
        return true;
    }
}
