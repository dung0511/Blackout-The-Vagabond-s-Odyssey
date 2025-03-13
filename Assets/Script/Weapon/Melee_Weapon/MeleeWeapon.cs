using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MeleeWeapon : MonoBehaviour
{
    public MeleeWeaponSO meleeDetail;
    public SpriteRenderer CharacterSR;
    public Animator anim;
    private float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;
    public int dame;
    public bool inHand;

    void Start()
    {
        attackCooldown=meleeDetail.attackCooldown;
        dame =meleeDetail.damageMeleeWeapon;
        anim = GetComponentInChildren<Animator>();
        if (GetComponentInParent<WeaponController>() != null)
        {
            Transform player = transform.parent.parent;
            CharacterSR = player.GetComponentInChildren<SpriteRenderer>();
            inHand = true;
            GetComponent<BoxCollider2D>().enabled = false;

        }
        else
        {
            inHand = false;
            InGround(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inHand)
        {
            RotateMeleeWeapon();
            GetComponent<BoxCollider2D>().enabled = false;
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

    public void InGround(GameObject game)
    {
        game.GetComponent<MeleeWeapon>().CharacterSR = null;
        game.transform.rotation = Quaternion.identity;
        game.GetComponent<MeleeWeapon>().inHand = false;
        game.transform.localScale = new Vector3(5, 5, 0);
        game.GetComponent<BoxCollider2D>().enabled = true;
    }


}
