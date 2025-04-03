
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : BaseWeapon, IPick
{
    public SpriteRenderer characterSR;
    public Animator animator;
    public float lastAttackTime=0f;
    public float attackCoolDown;
    public bool inHand;
    public int damage;
    public MeleeWeaponSO MeleeWeaponSO;
    public WeaponDetailSO weaponDetailSO;

    private int attackIndex = 1;
    private float comboResetTime = 5f;

    void Start()
    {

        attackCoolDown = MeleeWeaponSO.attackCooldown;
        damage = MeleeWeaponSO.damageMeleeWeapon;
        animator = GetComponentInChildren<Animator>();

        if (!GetComponentInParent<Transform>().root.Find("Character").IsUnityNull())
        {
            characterSR = transform.root.GetComponentInChildren<SpriteRenderer>();
            inHand = true;
        }
        else
        {
            inHand = false;
            DropWeapon();
        }
    }

    public GameObject GetPickGameOject()
    {
        return gameObject;
    }

    public void Pick()
    {
        characterSR = transform.root.GetComponentInChildren<SpriteRenderer>();
        inHand = true;
       
    }

    public void Drop()
    {
        characterSR = null;
        inHand = false;
        
    }

    public bool IsPickingItemOrWeapon()
    {
        return true;
    }

    public override void Attack()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName($"Attack{attackIndex}"))
            return;


        if (Time.time - lastAttackTime > comboResetTime)
            attackIndex = 1;

        animator.ResetTrigger($"Attack{attackIndex}");
        animator.SetTrigger($"Attack{attackIndex}");

        lastAttackTime = Time.time;

        attackIndex++;
        if (attackIndex > 3) attackIndex = 1;
    }

    public override void RotateWeapon()
    {
        if (!inHand) return;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)(worldMousePos - transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        bool isFlipped = angle > 90 || angle < -90;
        characterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
        characterSR.gameObject.transform.localScale.y, 1);


        transform.localScale = new Vector3(0.7f, isFlipped ? -0.7f : 0.7f, 1);
    }

    public override void PickWeapon()
    {
        transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        characterSR = transform.root.GetComponentInChildren<SpriteRenderer>();
        transform.localScale = new Vector3(0.7f, 0.7f, 0);
        transform.localPosition = new Vector3(0, 0.04f, 0);
        inHand = true;
    }

    public override void DropWeapon()
    {
        characterSR = null;
        inHand = false;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(5, 5, 0);
        transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public override float RotateToMousePos()
    {
        
        return 0;
    }
    public override WeaponDetailSO GetWeaponDetailSO()
    {
        return weaponDetailSO;
    }
}
