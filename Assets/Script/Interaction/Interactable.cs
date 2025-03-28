using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    protected GameObject outline;
    protected Interactor interactPlayer;

    protected virtual void Awake()
    {
        outline = transform.GetChild(0).gameObject;
    }

    public virtual void Interact(Interactor interactPlayer)
    {
        this.interactPlayer = interactPlayer;
        Debug.Log("Interacted");

    }

    public virtual void HighLightOn()
    {
        outline.SetActive(true);
    }

    public virtual void HighLightOff()
    {
        outline.SetActive(false);
    }
}
