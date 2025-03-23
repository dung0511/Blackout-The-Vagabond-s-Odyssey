using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MinimapFOW : MonoBehaviour
{
    [SerializeField] private int visionRange = 5;
    private Tilemap minimap;
    private Vector2Int lastPlayerPos;

    void OnEnable()
    {
        Binding();
    }
    private void Binding()
    {
        minimap = GameObject.FindGameObjectWithTag("MinimapTilemap").GetComponent<Tilemap>();
    }

    void Update()
    {
        Vector2Int playerPos = Vector2Int.FloorToInt(transform.position);

        if (playerPos != lastPlayerPos) // Check if the player has moved to a new tile
        {
            lastPlayerPos = playerPos; 
            var vision = Utility.FloodFillRadius(playerPos, MinimapData.minimapTiles.Keys, visionRange);

            foreach (var pos in vision)
            {
                if (MinimapData.revealedTiles.Add(pos)) // Only update newly discovered tiles
                {
                    minimap.SetTile(new Vector3Int(pos.x, pos.y, 0), MinimapData.minimapTiles[pos]);
                }
            }
        }
    }
}
