using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private void Awake()
    {
        var animator = GetComponent<Animator>();
        animator.SetFloat("Stage", GameManager.Instance.currentStage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.NextLevel();
    }

}