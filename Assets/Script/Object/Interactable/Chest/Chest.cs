using UnityEngine;

public class Chest : Lootable, IInteractable
{
    private Animator[] chestAnimator = new Animator[2];
    private GameObject outline;

    private void Awake()
    {
        chestAnimator[0] = GetComponent<Animator>(); // Chest animator
        chestAnimator[1] = transform.GetChild(0).GetComponent<Animator>(); //outline animator
        outline = transform.GetChild(0).gameObject;
    }

    public void Interact()
    {
        chestAnimator[0].SetTrigger("isOpen");
        chestAnimator[1].SetTrigger("isOpen");
        DropLoot();
    }

    public void HighLightOn()
    {
        outline.SetActive(true);
    }

    public void HighLightOff()
    {
        outline.SetActive(false);
    }
}
