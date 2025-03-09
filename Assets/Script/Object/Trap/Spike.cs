using UnityEngine;

public class Spike : MonoBehaviour
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
        if (other.TryGetComponent<PlayerHealthController>(out var playerHealthController))
        {
            animator.SetTrigger(activate);
            playerHealthController.takeDame(damage);
        }
    }

}