using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damageable : MonoBehaviour, IDamageable
{
    public int health = 3;
    private Lootable lootable;

    private void Awake()
    {
        lootable = GetComponent<Lootable>();
    }

    public virtual void takeDame(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Break();
            
        }
    }

    private void Break()
    {
        //AstarPath.active.Scan();
        if (lootable != null)
        {
            lootable.DropLoot();
        }
        //Destroy(gameObject);
        StartCoroutine(UpdateGraphAndDestroy());
    }
    
    private IEnumerator UpdateGraphAndDestroy()
    {
        // Đợi 1 frame để đảm bảo collider đã bị vô hiệu hóa/hủy bỏ
        yield return null;

       
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;

            Bounds updateBounds = col.bounds;

            GraphUpdateObject guo = new GraphUpdateObject(updateBounds);
           
            // guo.modifyWalkability = true;
            // guo.setWalkability = true;

            AstarPath.active.UpdateGraphs(guo);
        }
        Destroy(gameObject);
    }
}
