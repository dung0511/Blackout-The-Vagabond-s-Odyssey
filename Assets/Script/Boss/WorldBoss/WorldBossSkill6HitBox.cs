using UnityEngine;

public class WorldBossSkill6HitBox : MonoBehaviour
{
    private int skill6Damage;
    public BulletIdentifier BulletIdentifier;
    private void Start()
    {
        GameObject obj = GameObject.Find("WorldBoss(Clone)").GetComponent<WorldBoss>().gameObject;
        skill6Damage = obj.GetComponent<WorldBoss>().Skill6Damage;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var player))
        {
            player.takeDame(skill6Damage);
            PoolManagement.Instance.ReturnBullet(gameObject, BulletIdentifier.bulletPrefabReference);
        }
        PoolManagement.Instance.ReturnBullet(gameObject, BulletIdentifier.bulletPrefabReference);
    }
}
