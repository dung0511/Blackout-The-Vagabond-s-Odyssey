using UnityEngine;

public class Chest : Interactable
{
    private Lootable lootable;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent<Lootable>(out lootable);
        animator = GetComponent<Animator>(); // object animator
    }

    public override void Interact()
    {
        base.Interact();
        animator.SetTrigger("Interact");
        if(lootable != null)
        {
            lootable.DropLoot();
        }
        this.enabled = false;
    }

    public override void HighLightOn()
    {
        base.HighLightOn();
    }

    public override void HighLightOff()
    {
        base.HighLightOff();
    }
}
