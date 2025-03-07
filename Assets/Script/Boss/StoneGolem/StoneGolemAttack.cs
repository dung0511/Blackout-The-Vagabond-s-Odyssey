using Assets.Script.Boss.StoneGolem;
using UnityEngine;

public class StoneGolemAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.healthController.takeDame(GetComponentInParent<StoneGolem>().damage);

        }
    }
}
