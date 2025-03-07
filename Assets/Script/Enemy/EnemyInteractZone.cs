using UnityEngine;

public class EnemyInteractZone : MonoBehaviour
{
    public bool isTouchPlayer = false;

    private void OnTriggerEnter2D()
    {
        isTouchPlayer = true;
    }

    private void OnTriggerStay2D()
    {
        isTouchPlayer = true;
    }

    private void OnTriggerExit2D()
    {
        isTouchPlayer = false;

    }
}
