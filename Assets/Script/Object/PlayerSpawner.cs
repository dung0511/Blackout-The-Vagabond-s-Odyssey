using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach(var player in players)
        {
            player.transform.position = this.transform.position;
        }
    }
}
