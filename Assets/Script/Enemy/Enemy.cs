using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public float speed;
    public int health;
    public Animator animator;
    public bool isHurt;

    void Start()
    {
        animator = GetComponent<Animator>();
        isHurt = false;
    }

    void Update()
    {
        animator.SetBool("isHurt", isHurt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            isHurt = true;
            Debug.Log("Is hurt is true");
            StartCoroutine(ResetHurt());

            
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.5f); 
        isHurt = false;
        Debug.Log("Is hurt is false");
    }
}
