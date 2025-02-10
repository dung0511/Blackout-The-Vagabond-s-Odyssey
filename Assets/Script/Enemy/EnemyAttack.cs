using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy dame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dame=GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            //isTouchPlayer = true;
            player.takeDame(dame.damage);
            Debug.Log("dealt:"+dame.damage);
        }
        //isTouchPlayer = false;
    }
}
