using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{
    public GameObject border;
    public GameObject picture;
    public static ImageDisplay INSTANCE;
    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;      
    }
    public void SetImage(Sprite spr)
    {
        border.SetActive(true);
        picture.SetActive(true);
        picture.GetComponent<Image>().sprite = spr;
    }
}
