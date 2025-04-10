using UnityEngine;

public class Shop : Interactable
{
    private bool isUIOpen = false;

    public override void Interact(Interactor interactPlayer)
    {
        base.Interact(interactPlayer);

        isUIOpen = !isUIOpen;

        if (isUIOpen)
            ShopUI.instance.Show();
        else
            ShopUI.instance.Hide();
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