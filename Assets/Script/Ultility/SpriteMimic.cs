using UnityEngine;

public class SpriteMimic : MonoBehaviour
{
    [SerializeField] private SpriteRenderer target;
    private SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.sprite = target.sprite;
    }
}
