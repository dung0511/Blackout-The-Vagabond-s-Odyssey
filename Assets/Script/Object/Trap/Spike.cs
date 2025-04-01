using UnityEngine;

public class Spike : MonoBehaviour, ITrap
{
    [SerializeField] private int damage = 2;
    private Animator animator;
    int activate;
    private void Start()
    {
        animator = GetComponent<Animator>();
        activate = Animator.StringToHash("activate");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Activate(other);
    }

    public void Activate(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if (collision.TryGetComponent<Damageable>(out var damageable))
        {
            animator.SetTrigger(activate);
            damageable.takeDame(damage);
        }
    }

    public void Reset()
    {
        
    }
}