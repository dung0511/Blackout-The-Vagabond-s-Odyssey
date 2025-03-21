using Assets.Script.Boss.StoneGolem;
using UnityEngine;

public class RedZoneHitBox : MonoBehaviour
{
    private float damageTimer = 0f;
    private float damageInterval = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                enemy.takeDame(2);
                damageTimer = 0f;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                enemy.takeDame(2);
                damageTimer = 0f;
            }
        }
    }
}
