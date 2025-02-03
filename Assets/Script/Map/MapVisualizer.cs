using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase floorTile; 
    [SerializeField] private TileBase wallTile;
    [SerializeField] private GameObject doorParent; // up/down, left, right
    [SerializeField] private GameObject[] doors; // up/down, left, right

    public void PlaceTiles(IEnumerable<Vector2Int> path)
    {
        foreach (var pos in path)
        {
            PlaceTile(floorTilemap, floorTile, pos);
        }
    }

    private void PlaceTile(Tilemap floorTilemap, TileBase tile, Vector2Int pos)
    {
        var tilePos = floorTilemap.WorldToCell((Vector3Int)pos);
        floorTilemap.SetTile(new Vector3Int(tilePos.x, tilePos.y, 0), tile);
    }

    public void ClearMap()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        //clear doors
        while (doorParent.transform.childCount > 0) {
            DestroyImmediate(doorParent.transform.GetChild(0).gameObject);
        }
    }

    public void PlaceWall(Vector2Int pos)
    {
        PlaceTile(wallTilemap, wallTile, pos);
    }

    public void PlaceDoor(Vector2Int pos, int index)
    {
        var doorPos = floorTilemap.GetCellCenterWorld((Vector3Int)pos);
        var door = Instantiate(doors[index], doorPos, Quaternion.identity);
        door.transform.SetParent(doorParent.transform);
    }
}
