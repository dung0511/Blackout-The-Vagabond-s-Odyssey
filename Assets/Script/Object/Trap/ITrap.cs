using UnityEngine;

public interface ITrap
{
    public void Activate(Collider2D collision);
    public void Reset();
}
