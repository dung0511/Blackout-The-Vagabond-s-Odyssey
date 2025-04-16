using UnityEngine;

public class OrcWarlockSkill1HitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out var gameobj))
        {
            Debug.Log("dealt:" + transform.root.GetComponent<OrcWarlock>().damage);
            gameobj.takeDame(transform.root.GetComponent<OrcWarlock>().damage);
            gameObject.SetActive(false);
        }
    }
}
