public class Chest : Lootable, IInteractable
{
    public void Interact()
    {
        OpenChest();
    }

    public void OpenChest()
    {
        DropLoot();
    }

    public void HighLightOn()
    {
        throw new System.NotImplementedException();
    }

    public void HighLightOff()
    {
        throw new System.NotImplementedException();
    }
}