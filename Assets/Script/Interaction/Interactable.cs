using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    private Animator[] animator = new Animator[2];
    private GameObject outline;

    protected virtual void Awake()
    {
        animator[0] = GetComponent<Animator>(); // object animator
        outline = transform.GetChild(0).gameObject;
        animator[1] = outline.GetComponent<Animator>(); //outline animator
    }

    public virtual void Interact()
    {
        animator[0].SetTrigger("isOpen");
        animator[1].SetTrigger("isOpen");
    }

    public virtual void HighLightOn()
    {
        outline.SetActive(true);
    }

    public virtual void HighLightOff()
    {
        outline.SetActive(false);
    }
}
