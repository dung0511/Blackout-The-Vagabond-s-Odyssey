using UnityEngine;

public class Shop : Interactable
{
    private bool isUIOpen = false;


    public override void Interact(Interactor interactPlayer)
    {
        base.Interact(interactPlayer);

        isUIOpen = !isUIOpen;

        if (isUIOpen)
            ShopUI.Instance.Show();
        else
            ShopUI.Instance.Hide();
    }

    public override void HighLightOn()
    {
        base.HighLightOn();
    }

    public override void HighLightOff()
    {
        base.HighLightOff();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Interactor>() != null && isUIOpen)
        {
            isUIOpen = false;
            ShopUI.Instance.Hide();
        }
    }
}