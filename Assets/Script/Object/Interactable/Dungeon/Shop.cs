using UnityEngine;
using System.Collections;

public class Shop : Interactable
{
    private bool isUIOpen = false;
    private bool canOpenShop = true; 

    public override void Interact(Interactor interactPlayer)
    {
        if (!canOpenShop) return; 

        base.Interact(interactPlayer);

        isUIOpen = !isUIOpen;

        if (isUIOpen)
            ShopUI.Instance.Show();
        else
        {
            ShopUI.Instance.Hide();
            StartCoroutine(InteractionCooldown());
        }
    }

    public override void HighLightOn()
    {
        if (!canOpenShop) return;
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
            StartCoroutine(InteractionCooldown());
        }
    }

    private IEnumerator InteractionCooldown()
    {
        canOpenShop = false; 
        yield return new WaitForSeconds(7f); 
        canOpenShop = true; 
    }
}