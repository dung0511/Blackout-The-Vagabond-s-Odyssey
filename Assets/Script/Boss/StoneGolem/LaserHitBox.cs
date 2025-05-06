using Assets.Script.Boss.StoneGolem;
using UnityEngine;

public class LaserHitBox : MonoBehaviour
{
    public int dame;

    private float damageTimer = 0f;
    private float damageInterval = 0.5f;

    private void Start()
    {
        dame = GetComponentInParent<Laser>().stoneGolem.damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerHealthController playerHealthController = collision.GetComponent<PlayerHealthController>();
        if (collision.TryGetComponent<IDamageable>(out var player))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                player.takeDame(dame);
                damageTimer = 0f;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // PlayerHealthController playerHealthController = collision.GetComponent<PlayerHealthController>();
        if (collision.TryGetComponent<IDamageable>(out var player))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                player.takeDame(dame);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // PlayerHealthController playerHealthController = collision.GetComponent<PlayerHealthController>();
        if (collision.TryGetComponent<IDamageable>(out var player))
        {

            damageTimer = 0f;
        }
    }
}
