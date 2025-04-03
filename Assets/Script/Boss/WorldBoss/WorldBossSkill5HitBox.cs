using UnityEngine;

public class WorldBossSkill5HitBox : MonoBehaviour
{
    public int skill5Damage;

    private void Start()
    {
        skill5Damage=GetComponentInParent<WorldBoss>().Skill5Damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out var player))
        {
            Debug.Log("DMM" + GetComponentInParent<WorldBoss>().Skill3Damage);
            player.takeDame(skill5Damage);
        }
    }
}
