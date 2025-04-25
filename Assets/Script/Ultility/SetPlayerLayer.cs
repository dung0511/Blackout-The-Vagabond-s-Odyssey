using UnityEngine;

public class SetPlayerLayer : MonoBehaviour
{
    [SerializeField] private bool setCollideLayer = true;
    [SerializeField] private LayerMask collideLayer = 0;
    [SerializeField] private bool setSortingLayer = true;
    [SerializeField] private SortingLayer sortLayer;
    [SerializeField] private int sortOrder = 0;

    void Awake()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (setCollideLayer)
            {
                player.layer = collideLayer;
            }

            if (setSortingLayer)
            {
                var sortingGroup = player.GetComponentsInChildren<SpriteRenderer>();
                foreach (var spriteRenderer in sortingGroup)
                {
                    spriteRenderer.sortingLayerName = sortLayer.name;
                    spriteRenderer.sortingOrder = sortOrder;
                }
            }
        }
    }
}
