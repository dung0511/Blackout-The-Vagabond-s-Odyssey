using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MeleeWeapon : MonoBehaviour
{
    public MeleeWeaponSO meleeDetail;
    public SpriteRenderer CharacterSR;
    public Animator anim;
    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;
    public int dame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dame=meleeDetail.damageMeleeWeapon;
        CharacterSR = GameObject.Find("Character").GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateMeleeWeapon();

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackMelee();
                lastAttackTime = Time.time;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isMeleeAttack", false);
        }

    }

    void RotateMeleeWeapon()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)(worldMousePos - transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        bool isFlipped = angle > 90 || angle < -90;

        CharacterSR.gameObject.transform.localScale = new Vector3(isFlipped ? -1 : 1,
                                                                     CharacterSR.gameObject.transform.localScale.y, 1);


        transform.localScale = new Vector3(0.7f, isFlipped ? -0.7f : 0.7f, 1);

    }
    void AttackMelee()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        anim.SetBool("isMeleeAttack", true);

        yield return new WaitForSeconds(0.4f);

        
    }


}
