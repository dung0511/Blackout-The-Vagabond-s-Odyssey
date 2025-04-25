using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitCamera : MonoBehaviour
{
    void Start()
    {
        Resize();
    }

    void Resize()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if (rend == null) return;

        Vector3 size = rend.bounds.size;

        float scaleX = width / size.x;
        float scaleY = height / size.y;

        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}
