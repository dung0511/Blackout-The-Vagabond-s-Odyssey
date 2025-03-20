using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentPlacer : MonoBehaviour
{
    [SerializeField] private Transform enemyParent;
    [SerializeField] private List<AgentPlacerSO> normalEnemies; //variants
    [SerializeField] private int minNormalEnemies = 4;
    [SerializeField] private int maxNormalEnemies = 6;
    [SerializeField] private List<AgentPlacerSO> eliteEnemies; //variants
    [Header("Elite enemies Modifier Multiplier")]
    [SerializeField] private float eliteHp = 1.5f;
    [SerializeField] private float eliteDamage = 1.5f;
    [SerializeField] private float eliteSize = 1;


    public void PlaceAgents(BoxRoom room)
    {
        int enemiesCount = Utility.UnseededRng(minNormalEnemies, maxNormalEnemies);
        HashSet<Vector2Int> spawnableTiles = GetTraversableTiles(room);
        var availableSpaces = spawnableTiles.ToList(); //for shuffle, checking area for agent placement
        Utility.UnseededShuffle(availableSpaces);

        for (int i = 0; i < enemiesCount; i++)
        {
            AgentPlacerSO enemy = normalEnemies[Random.Range(0, normalEnemies.Count)];
            TryPlacingAgentBruteForce(availableSpaces, enemy, room);

        }
        if(room.roomType == RoomType.Elite)
        {
            AgentPlacerSO elite = eliteEnemies[Random.Range(0, eliteEnemies.Count)];
            var e = TryPlacingAgentBruteForce(availableSpaces, elite, room);
            if(e != null)
            {
                room.eliteReference = e;
            }
            // elite.GetComponent<Enemy>().ModifyStats(eliteHp, eliteDamage, eliteSize);
        }
    }

    private GameObject TryPlacingAgentBruteForce(List<Vector2Int> availableSpaces, AgentPlacerSO enemy, BoxRoom room)
    {
        foreach(var position in availableSpaces)
        {
            var placementPos = GetAgentPlacementPosition(enemy, position);
            var gridPos = new Vector2Int(Mathf.FloorToInt(placementPos.x), Mathf.FloorToInt(placementPos.y-enemy.size.y/2f));
            if(TryFitAgent(gridPos, availableSpaces, enemy))
            {
                var enemyObject = Instantiate(enemy.agentPrefab, placementPos, Quaternion.identity, enemyParent);
                room.enemyCount++;
                var enemyScript = enemyObject.GetComponent<Enemy>();
                enemyScript.roomBelong = room;
                return enemyObject;
            }
        }
        return null;
    }

    private bool TryFitAgent(Vector2Int tryPosition, List<Vector2Int> availableSpaces, AgentPlacerSO enemy)
    {
        var left = Mathf.CeilToInt(-enemy.size.x/2f);
        var right = Mathf.FloorToInt(enemy.size.x/2f);
        var taken = new List<Vector2Int>();
        for(int x = left-1; x <= right+1; x++) //from center, check 2 sides avaiable
        {
            for(int y = 0-1; y < enemy.size.y+1; y++) //from bottom, check up
            {
                var position = new Vector2Int(tryPosition.x + x, tryPosition.y + y);
                if(!availableSpaces.Contains(position)) return false;
                else taken.Add(position);
            }
        }
        availableSpaces.RemoveAll(taken.Contains);
        return true;
    }

    private Vector2 GetAgentPlacementPosition(AgentPlacerSO enemy, Vector2Int gridPosition)
    {
        //bottom center (enemy feet at anchor point, two sides might be uneven)
        var centerY = (enemy.spriteHeight - enemy.size.y)/2f;
        var position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + centerY);
        return position;
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

    public void PlacePlayer(BoxRoom room)
    {
        var player = FindFirstObjectByType<Player>();
        var playerPos = room.center;
        player.transform.position = new Vector3(playerPos.x + 0.5f, playerPos.y + 0.5f, 0);
    }

    public void Reset()
    {
        while (enemyParent.childCount > 0)
        {
            DestroyImmediate(enemyParent.GetChild(0).gameObject);
        }
    }
}
