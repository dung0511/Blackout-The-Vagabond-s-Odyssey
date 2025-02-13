using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool isHurt;
    public bool isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isHurt = false;
        isDead=false;
}

    
    public void takeDame(int dame)
    {
        health -= dame;
        if (health <= 0)
        {
            isDead = true;
            health = 0;
            //animator.SetBool("isDead",true);
        }
        else isHurt=true;
        StartCoroutine(ResetHurt());
    }
    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
}
