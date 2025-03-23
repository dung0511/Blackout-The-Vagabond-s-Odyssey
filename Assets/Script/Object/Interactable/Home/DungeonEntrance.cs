using System;
using System.Collections;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private GameObject animationPrefab;
    [SerializeField] private float delayStart = 1.5f;
    [SerializeField] private float delayHide = 0.2f;
    [SerializeField] private Vector3 animationOffset = new Vector3(0.25f,0.25f,0);

    void OnTriggerEnter2D(Collider2D collision)
    {
        InputManager.Instance.playerInput.Ingame.Interact.performed += EnterDungeon;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        InputManager.Instance.playerInput.Ingame.Interact.performed -= EnterDungeon;
    }

    private void EnterDungeon(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        InputManager.Instance.playerInput.Ingame.Interact.performed -= EnterDungeon;
        var players = GameObject.FindObjectsByType<HomeMovement>(FindObjectsSortMode.None);
        
        foreach(var found in players)
        {
            found.enabled = false;
            Instantiate(animationPrefab, found.transform.position + animationOffset, Quaternion.identity);
            var sprite = found.transform.root.gameObject;
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
