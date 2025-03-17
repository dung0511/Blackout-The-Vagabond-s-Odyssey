using UnityEngine;

public class Chest : Interactable
{
    private Lootable lootable;
    private bool isOpen = false;

    protected override void Awake()
    {
        base.Awake();
        lootable = GetComponent<Lootable>();
    }

    public override void Interact()
    {
        if(!isOpen)
         {
            isOpen = true;
            base.Interact();
            //lootable.DropLoot();
         }
    }

    public override void HighLightOn()
    {
         if(!isOpen) base.HighLightOn();
    }

    public override void HighLightOff()
    {
        if(isOpen)
         {
             Destroy(transform.GetChild(0).gameObject);
         }
         else base.HighLightOff();
    }
}
