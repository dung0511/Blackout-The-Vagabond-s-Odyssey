using UnityEngine;

public class Chest : Interactable
{
    private Lootable lootable;
    private Animator animator;
    private bool isOpen = false;

    protected override void Awake()
    {
        base.Awake();
        lootable = GetComponent<Lootable>();
        animator = GetComponent<Animator>(); // object animator
    }

    public override void Interact(Interactor interactPlayer)
    {
        if(!isOpen)
        {
            base.Interact(interactPlayer);
            animator.SetTrigger("Interact");
            if(lootable != null)
            {
                lootable.DropLoot();
            }
            isOpen = true;
            Destroy(outline);
        }
    }

    public override void HighLightOn()
    {
        base.HighLightOn();
    }

    public override void HighLightOff()
    {
        if(outline) base.HighLightOff();
    }
}
