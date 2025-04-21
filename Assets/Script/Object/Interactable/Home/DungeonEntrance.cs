using System;
using System.Collections;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private GameObject animationPrefab;
    [SerializeField] private float delayStart = 1.5f;
    [SerializeField] private float delayHide = 0.2f;
    [SerializeField] private Vector3 animationOffset = new Vector3(0.25f,0.25f,0);

    private bool isSubscribed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSubscribed) return;
        InputManager.Instance.playerInput.Ingame.Interact.performed += EnterDungeon;
        isSubscribed = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!isSubscribed) return;
        InputManager.Instance.playerInput.Ingame.Interact.performed -= EnterDungeon;
        isSubscribed = false;
    }

    private void EnterDungeon(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!isSubscribed) return;
        InputManager.Instance.playerInput.Ingame.Interact.performed -= EnterDungeon;
        isSubscribed = false;
        var players = GameObject.FindObjectsByType<HomeMovement>(FindObjectsSortMode.None);
        
        foreach(var player in players)
        {
            player.enabled = false;
            Instantiate(animationPrefab, player.transform.position + animationOffset, Quaternion.identity);
            var sprite = player.transform.root.gameObject;
            StartCoroutine(HidePlayer(sprite));
        }
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene(){
        yield return new WaitForSeconds(delayStart);
        GameManager.Instance.StartDungeon();
    }

    private IEnumerator HidePlayer(GameObject obj)
    {
        yield return new WaitForSeconds(delayHide);
        obj.SetActive(false);
    }
}
