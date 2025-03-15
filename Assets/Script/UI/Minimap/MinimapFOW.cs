using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinimapFOW : MonoBehaviour
{
    [SerializeField] private int visionRange = 5;
    private Tilemap minimap;

    void Awake()
    {
        minimap = GameObject.FindGameObjectWithTag("MinimapTilemap").GetComponent<Tilemap>();    
    }

    void Update()
    {
        Vector2Int playerPos = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        var vision = Utility.TraverseRadiusBFS(playerPos ,MinimapData.minimapTiles.Keys, visionRange);
        foreach(var pos in vision)
        {
            var tile = MinimapData.minimapTiles[pos];
            minimap.SetTile(new Vector3Int(pos.x, pos.y, 0), tile);
        }
    }
}
