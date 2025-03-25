using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemActionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject btnPrefab;

    public void AddButton(string name, Action onClickAction)
    {
        GameObject btn = Instantiate(btnPrefab, transform);
        btn.GetComponent<Button>().onClick.AddListener(() => onClickAction());
        btn.GetComponentInChildren<TMPro.TMP_Text>().text = name;
    }

    public void Toggle(bool val)
    {
        if (val == true)
        {
            RemoveOldButtons();
        }
        gameObject.SetActive(val);
    }

    private void RemoveOldButtons()
    {
        foreach (Transform transformChilObjects in transform)
        {
            Destroy(transformChilObjects.gameObject);
        }
    }
}
