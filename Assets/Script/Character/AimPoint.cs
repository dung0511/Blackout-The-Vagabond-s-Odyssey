using UnityEngine;

public class AimPoint : MonoBehaviour
{
    public static AimPoint Instance;

    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotspot;

    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void DisableAim()
    {
        gameObject.SetActive(false);
    }

    public void EnableAim()
    {
        gameObject.SetActive(true);
    }
}
