using UnityEngine;
using System.Collections;

public class Shop : Interactable
{
    

    public override void Interact(Interactor interactPlayer)
    {
        ShopUI.Instance.Show();
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