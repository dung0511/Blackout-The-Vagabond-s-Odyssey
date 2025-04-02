using UnityEngine;

public class FireEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator Animator;
    void Start()
    {
        Animator = GetComponent<Animator>();
        int randomFireEffect = Random.Range(0, 5);
        switch (randomFireEffect)
        {
            case 0:
                break;

            case 1:
                Animator.SetBool("isBurning1", true);
                break;
            case 2:
                Animator.SetBool("isBurning2", true);
                break;
            case 3:
                Animator.SetBool("isBurning3", true);
                break;
            case 4:
                Animator.SetBool("isBurning4", true);
                break;
        }
    }


}
