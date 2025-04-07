using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void Awake()
    {
        var animator = GetComponent<Animator>();
        animator.SetFloat("Stage", GameManager.Instance.currentStage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = new Vector3Int(1000,0,0); //scuff temp hide player 
        GameManager.Instance.NextLevel();
    }

}