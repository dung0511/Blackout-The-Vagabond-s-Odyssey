using UnityEngine;

public class Interactzone : MonoBehaviour
{
    private void OnTriggerEnter2D()
    {
        GetComponentInParent<WorldBoss>().isPlayerStandingClose = true;
    }
    private void OnTriggerStay2D()
    {
        GetComponentInParent<WorldBoss>().isPlayerStandingClose = true;
    }
    private void OnTriggerExit2D()
    {
        GetComponentInParent<WorldBoss>().isPlayerStandingClose = false;
    }
}
