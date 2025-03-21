using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    protected GameObject outline;

    protected virtual void Awake()
    {
        outline = transform.GetChild(0).gameObject;
    }

    public virtual void Interact()
    {
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
