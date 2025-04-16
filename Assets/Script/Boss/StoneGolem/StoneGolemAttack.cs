using Assets.Script.Boss.StoneGolem;
using UnityEngine;

public class StoneGolemAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Player player = collision.GetComponent<Player>();
        if (collision.TryGetComponent<IDamageable>(out var player))
        {
            player.takeDame(GetComponentInParent<StoneGolem>().damage);

        }
    }
}
