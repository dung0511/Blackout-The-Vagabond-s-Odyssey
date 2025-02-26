using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentPlacer : MonoBehaviour
{
    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> normalEnemies; //variants
    [SerializeField] private int minNormalEnemies = 4;
    [SerializeField] private int maxNormalEnemies = 6;
    [SerializeField] private List<GameObject> eliteEnemies; //variants
    [Header("Elite enemies Modifier Multiplier")]
    [SerializeField] private float eliteHp = 1.5f;
    [SerializeField] private float eliteDamage = 1.5f;
    [SerializeField] private float eliteSize = 1;

    public void PlaceEnemies(BoxRoom room)
    {
        int enemiesCount = Utility.UnseededRng(minNormalEnemies, maxNormalEnemies);
        HashSet<Vector2Int> spawnableTiles = GetTraversableTiles(room);

        for (int i = 0; i < enemiesCount; i++)
        {
            Vector2Int spawnPoint = spawnableTiles.Skip(Random.Range(0, spawnableTiles.Count)).First();
            GameObject variant = normalEnemies[Random.Range(0, normalEnemies.Count)];
            PlaceAgent(room, variant, spawnPoint);
            spawnableTiles.Remove(spawnPoint);
        }
        if(room.roomType == RoomType.Elite)
        {
            Vector2Int spawnPoint = spawnableTiles.Skip(Random.Range(0, spawnableTiles.Count)).First();
            GameObject variant = eliteEnemies[Random.Range(0, eliteEnemies.Count)];
            var elite = PlaceAgent(room, variant, spawnPoint);
            // elite.GetComponent<Enemy>().ModifyStats(eliteHp, eliteDamage, eliteSize);
            spawnableTiles.Remove(spawnPoint);
        }
    }

    private HashSet<Vector2Int> GetTraversableTiles(BoxRoom room) //bfs from all entrances and center incase prop objects block
    {
        HashSet<Vector2Int> traversableTiles = new HashSet<Vector2Int>();
        HashSet<Vector2Int> openTiles = new HashSet<Vector2Int>(room.roomTiles); openTiles.ExceptWith(room.propPositions);

        var entrances = GetRoomEntrances(room);
        foreach(var entrance in entrances)
        {
            if(!traversableTiles.Contains(entrance)) traversableTiles.UnionWith(Utility.TraverseBFS(entrance, openTiles)); //bfs from all entrances
        }
        if(!traversableTiles.Contains(room.center) || !openTiles.Contains(room.center))
        {
            traversableTiles.UnionWith(Utility.TraverseBFS(room.center, openTiles)); //bfs from center
        }
        return traversableTiles;
    }

    private List<Vector2Int> GetRoomEntrances(BoxRoom room)
    {
        var entrances = new List<Vector2Int>();
        if(room.topEntrance != Vector2Int.zero) entrances.Add(room.topEntrance+Vector2Int.down);
        if(room.bottomEntrance != Vector2Int.zero) entrances.Add(room.bottomEntrance+Vector2Int.up);
        if(room.leftEntrance != Vector2Int.zero) entrances.Add(room.leftEntrance+Vector2Int.right);
        if(room.rightEntrance != Vector2Int.zero) entrances.Add(room.rightEntrance+Vector2Int.left);
        return entrances;
    }

    private GameObject PlaceAgent(BoxRoom room ,GameObject agent, Vector2Int position)
    {
        Vector3 spawnPos = new Vector3(position.x +0.5f, position.y +0.5f, 0);
        var enemy = Instantiate(agent, spawnPos, Quaternion.identity, enemyParent);
        room.enemyObjectReferences.Add(enemy);
        return enemy;
    }

    public void Reset()
    {
        while (enemyParent.childCount > 0)
        {
            DestroyImmediate(enemyParent.GetChild(0).gameObject);
        }
    }
}
