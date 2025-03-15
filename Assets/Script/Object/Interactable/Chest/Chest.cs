using UnityEngine;

public class Chest : Lootable, IInteractable
{
    private Animator[] chestAnimator = new Animator[2];
    private GameObject outline;
    private bool isOpen = false;

    private void Awake()
    {
        chestAnimator[0] = GetComponent<Animator>(); // Chest animator
        outline = transform.GetChild(0).gameObject;
        chestAnimator[1] = outline.GetComponent<Animator>(); //outline animator
    }

    public void Interact()
    {
        if(!isOpen)
        {
            isOpen = true;
            chestAnimator[0].SetTrigger("isOpen");
            chestAnimator[1].SetTrigger("isOpen");
            //DropLoot();
        }
    }

    public void HighLightOn()
    {
        if(outline != null) outline.SetActive(true);
    }

    public void HighLightOff()
    {
        if(isOpen)
        {
            Destroy(outline);
        }
        else outline.SetActive(false);
    }
}
