using System.Collections;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private float delaySpawn = 0.8f;
    [SerializeField] private float delayBoss = 0.2f;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject spawnEffect;
    private EdgeCollider2D trigger;

    void Awake()
    {
        var seed = DungeonManager.Instance.currentSeed;
        Random.InitState(seed.GetHashCode());
        trigger = GetComponent<EdgeCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        trigger.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        if(spawnEffect)
        {
            StartCoroutine(DelaySpawn());
        } else {
            StartCoroutine(DelayBoss());
        }
        
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(delaySpawn);
        var anime = Instantiate(spawnEffect, spawnPos.position + new Vector3(0,-0.25f,0), Quaternion.identity);
        Destroy(anime, 5);
        StartCoroutine(DelayBoss());
    }

    private IEnumerator DelayBoss()
    {
        yield return new WaitForSeconds(delayBoss);
        var bossIndex = Random.Range(0, BossManager.Instance.bossList.Count);
        Instantiate(BossManager.Instance.bossList[bossIndex], spawnPos.position, Quaternion.identity);
    }
}
